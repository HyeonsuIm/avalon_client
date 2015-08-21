﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace Avalron
{
    
    public partial class Lobby : Form
    {
        // Panel을 이용한 창 옮기기에 필요한 것들
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        
        public readonly int WM_NLBUTTONDOWN = 0xA1;
        public readonly int HT_CAPTION = 0x2;

        // 변수 선언
        enum LobbyOpcode { CHAT = 100, WISPER, ROOM_REFRESH, USER_REFRESH, ROOM_MAKE, ROOM_JOIN };
        enum PlayerOpcode { USER_INFO_REQUEST = 801, HOST_IP_REQUEST, USER_SCORE_REQUEST }
        enum GlobalOpcode { Nomal_CONNECTION = 900, Nomal_EXIT, Keep_Alive }
        delegate void SetTextBoxCallback(string nick, string chating);
        delegate void SetRoomCallback();
        string[] roomDefault = new string[6];
        char delimeter = '\u0001';
        Room[] room;
        AvalonServer.RoomListInfo roomListInfo;
        int indexPage, MaxPage; // 로비 방 페이지
        Task reciveDataThread, keepAliveThread;
        public bool isClosing = false;
        LobbyRoomPassword lobbyRoomPassword;

        public Lobby(UserInfo userInfo)
        {
            Program.userInfo = userInfo;
            Program.lobbyLoading = new LobbyLoading();
            Program.lobbyLoading.Show();

            InitializeComponent();

            Program.tcpAllocation();
            Shown += new EventHandler(Lobby_Shown);

            // room 할당
            RoomAllocation();
            
            LoadLobby();

        }

        private void Lobby_Shown(Object sender, EventArgs e)
        {
            Program.lobbyLoading.Close();
            keepAliveThread.Start();
            reciveDataThread.Start();
        }

        private void LoadLobby()
        {
            roomDefault[5] = "null";

            keepAliveThread = new Task(KeepAlive);
            reciveDataThread = new Task(resiveData);

            // 유저 정보 요청
            Program.tcp.DataSend((int)PlayerOpcode.USER_INFO_REQUEST, Program.userInfo.index.ToString());
            waitData((int)PlayerOpcode.USER_INFO_REQUEST);

            // 유저 점수 요청
            Program.tcp.DataSend((int)PlayerOpcode.USER_SCORE_REQUEST, Program.userInfo.index.ToString());
            waitData((int)PlayerOpcode.USER_SCORE_REQUEST);

            // 방 정보 불러오기
            Program.tcp.DataSend((int)LobbyOpcode.ROOM_REFRESH, "");
            waitData((int)LobbyOpcode.ROOM_REFRESH);

            // 유저 목록 요청
            Program.tcp.DataSend((int)LobbyOpcode.USER_REFRESH, "");
            //waitData((int)LobbyOpcode.USER_REFRESH);

            // 접속 성공 메세지
            ChatingLog.Text = "---------------------------접속에 성공하셨습니다----------------------------";

            UserNICK.Text = Program.userInfo.nick;
            UserSCORE.Text = Program.userInfo.win + " 승 " + Program.userInfo.lose + " 패 " + Program.userInfo.draw + " 무";
        }
        
        private void KeepAlive()
        {
            while (true)
            {
                Program.tcp.DataSend((int)GlobalOpcode.Keep_Alive,"");
                Thread.Sleep(5000);
            }
        }

        private void waitData(int opCode)
        {
            int dataleng;
            string data, parameterNum;
            byte[] bData;
            string[] parameter;

            Program.tcp.ReciveBData(out bData, out dataleng);

            data = Encoding.UTF8.GetString(bData);

            if(opCode != Convert.ToInt16(data.Substring(0, 3)))
            {
                MessageBox.Show("접속에 실패하였습니다. ErrorCode : " + opCode);
                Application.Exit();
                return;
            }

            try
            {
                parameter = data.Substring(5).Split('\u0001');
                parameterNum = data.Substring(3, 2);
            }
            catch
            {
                parameter = new string[0];
                parameterNum = "99";
                MessageBox.Show("통신오류");
                Application.Exit();
            }
            switch (opCode)
            {
                case (int)LobbyOpcode.ROOM_REFRESH: // 방목록 갱신
                    indexPage = 1;
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                    MemoryStream ms = new MemoryStream();
                    ms.Write(bData, 5, dataleng - 5);
                    ms.Position = 0;
                    roomListInfo = (AvalonServer.RoomListInfo)bf.Deserialize(ms);
                    MaxPage = ((roomListInfo.getRoomCount() - 1) / 6) + 1;
                    SetRooms();
                    break;
                case (int)LobbyOpcode.USER_REFRESH: // 유저목록 갱신 ( 수정중

                    break;
                case (int)PlayerOpcode.USER_INFO_REQUEST: // 유저정보 요청
                    Program.userInfo = new UserInfo(parameter[0], Convert.ToInt32(parameter[1]));
                    break;
                case (int)PlayerOpcode.USER_SCORE_REQUEST: // 유저전적 요청
                    Program.userInfo.setScore(Convert.ToInt16(parameter[0]), Convert.ToInt16(parameter[1]), Convert.ToInt16(parameter[2]));
                    break;
                default:
                    break;
            }
        }

        public void resiveData()
        {
            while (true)
            {
                int dataleng, opcode;
                string data, parameterNum;
                byte[] bData;
                string[] parameter;

                Program.tcp.ReciveBData(out bData, out dataleng);
                
                data = Encoding.UTF8.GetString(bData);

                try
                {
                    parameter = data.Substring(5).Split('\u0001');
                    opcode = Convert.ToInt16(data.Substring(0, 3));
                    parameterNum = data.Substring(3, 2);
                }
                catch
                {
                    parameter = new string[0];
                    opcode = 999;
                    parameterNum = "99";
                    MessageBox.Show("통신오류");
                    Application.Exit();
                }
                
                switch (opcode)
                {
                    case (int)LobbyOpcode.CHAT: // 채팅
                        if (parameter[1] == "") { break; }
                        SetChatingLog(parameter[0], parameter[1]);
                        break;
                    case (int)LobbyOpcode.WISPER: // 귓속말
                        break;
                    case (int)LobbyOpcode.ROOM_REFRESH: // 방목록 갱신
                        indexPage = 1;
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                        MemoryStream ms = new MemoryStream();
                        ms.Write(bData, 5, dataleng - 5);
                        ms.Position = 0;
                        roomListInfo = (AvalonServer.RoomListInfo)bf.Deserialize(ms);
                        //MessageBox.Show("방목록갱신");
                        MaxPage = ((roomListInfo.getRoomCount() - 1) / 6) + 1;
                        SetRooms();
                        break;
                    case (int)LobbyOpcode.USER_REFRESH: // 유저목록 갱신 ( 수정중

                        break;
                    case (int)LobbyOpcode.ROOM_MAKE: // 방 만들기
                        break;
                    case (int)LobbyOpcode.ROOM_JOIN: // 방 들어가기
                        break;
                    case (int)PlayerOpcode.USER_SCORE_REQUEST: // 유저전적 요청
                        Program.userInfo.setScore(Convert.ToInt16(parameter[0]), Convert.ToInt16(parameter[1]), Convert.ToInt16(parameter[2]));
                        break;
                    case (int)GlobalOpcode.Nomal_EXIT: // 정상접속종료
                        Application.Exit();
                        break;
                    default:
                        break;
                }
                if(opcode == (int)GlobalOpcode.Nomal_EXIT) { break; }
            }
        }

        private void SetChatingLog(string nick, string chating)
        {
            if (ChatingLog.InvokeRequired)
            {
                SetTextBoxCallback setTextBoxCallback = new SetTextBoxCallback(SetChatingLog);
                Invoke(setTextBoxCallback, new object[] { nick, chating });
            }
            else
            {
                ChatingLog.Text += Environment.NewLine + nick + " : " + chating;
            }
        }

        private void SetRooms()
        {
            if (ChatingLog.InvokeRequired)
            {
                SetRoomCallback setRoomCallback = new SetRoomCallback(SetRooms);
                Invoke(setRoomCallback);
            }
            else
            {
                int page = (indexPage - 1) * 6;
                for (int i = 0; i < 6; i++)
                {
                    if (roomListInfo.getRoomCount() <= i + page) { room[i].setRoomInfo(roomDefault); }
                    else { room[i].setRoomInfo(roomListInfo.roomInfo[i + page].getRoomInfo()); }
                }
                RoomListIndex.Text = indexPage + " / " + MaxPage;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 다른 컨트롤에 묶여있을 수 있을 수 있으므로 마우스캡쳐 해제
                ReleaseCapture();

                // 타이틀 바의 다운 이벤트처럼 보냄
                SendMessage(Handle, WM_NLBUTTONDOWN, HT_CAPTION, 0);
            }
            base.OnMouseDown(e);
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)GlobalOpcode.Nomal_EXIT, "");
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)GlobalOpcode.Nomal_EXIT, "");
        }

        private void Minimalize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SendMass_Click(object sender, EventArgs e)
        {
            if(ChatingBar.Text == ""){ return; }
            Program.tcp.DataSend((int)LobbyOpcode.CHAT, Program.userInfo.nick + delimeter + ChatingBar.Text);
            ChatingBar.Text = "";
        }

        private void ChatingBar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                // 메세지 보내기
                SendMass_Click(sender, e);

                // 엔터키 소리제거
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // 채팅로그
        private void ChatingLog_TextChanged(object sender, EventArgs e)
        {
            ChatingLog.SelectionStart = ChatingLog.Text.Length;
            ChatingLog.ScrollToCaret();
            ChatingBar.Focus();
        }

        // 방 새로고침
        private void Refresh_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)LobbyOpcode.ROOM_REFRESH, "");
            
            Refresh.Enabled = false;
            Delay(3000);
            Refresh.Enabled = true;
        }

        // 방만들기 클릭
        private void RoomMake_Click(object sender, EventArgs e)
        {
            LobbyRoomMake lobbyRoomMake = new LobbyRoomMake(Program.tcp);
            lobbyRoomMake.ShowDialog(this);
        }

        // 방목록 오른쪽 화살표
        private void RoomListRight_Click(object sender, EventArgs e)
        {
            if(indexPage < MaxPage)
            {
                indexPage++;
                RoomListIndex.Text = indexPage + " / " + MaxPage;
                SetRooms();
            }
        }

        // 방목록 왼쪽 화살표
        private void RoomListLeft_Click(object sender, EventArgs e)
        {
            if(indexPage > 1)
            {
                indexPage--;
                RoomListIndex.Text = indexPage + " / " + MaxPage;
                SetRooms();
            }
        }

        // 딜레이 함수
        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);
            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        // 방 비밀번호 체크
        public void cheakRoomPassword(string password)
        {
            if (password.Equals(""))
            {
                MessageBox.Show("입장합니다.");
            }
            else
            {
                lobbyRoomPassword = new LobbyRoomPassword(password);
                lobbyRoomPassword.ShowDialog();
            }
        }

        // 방 할당함수
        private void RoomAllocation()
        {
            room = new Room[6];
            for (int i = 0; i < 6; i++)
            {
                room[i] = new Room(i);
                room[i].setRoom(this);
            }
        }
    }

    // 직, 병렬화 버전문제 해결 클래스
    sealed class AllowAllAssemblyVersionsDeserializationBinder : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            String currentAssembly = Assembly.GetExecutingAssembly().FullName;

            // In this case we are always using the current assembly
            assemblyName = currentAssembly;

            // Get the type using the typeName and assemblyName
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
                typeName, assemblyName));

            return typeToDeserialize;
        }
    }
}
