using System;
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
        // 변수 선언
        public enum LobbyOpcode { CHAT = 100, WISPER, ROOM_REFRESH, USER_REFRESH, ROOM_MAKE, ROOM_JOIN };
        enum PlayerOpcode { USER_INFO_REQUEST = 801, HOST_IP_REQUEST, USER_SCORE_REQUEST }
        public enum GlobalOpcode { Nomal_CONNECTION = 900, Nomal_EXIT, Keep_Alive }
        delegate void SetTextBoxCallback(string nick, string chating);
        delegate void SetRoomCallback();
        delegate void SetUserListCallback(string[] users);
        string[] roomDefault = new string[6];
        char delimeter = '\u0001';
        Room[] room;
        public AvalonServer.RoomListInfo roomListInfo;
        int indexPage, MaxPage; // 로비 방 페이지
        //public Task reciveDataThread, keepAliveThread;
        static public Task keepAliveThread;
        static public Task reciveDataThread;
        static public Task userListThread;
        public bool isClosing = false;
        LobbyRoomPassword lobbyRoomPassword;

        LobbyRoomMake lobbyRoomMake;

        public Lobby(UserInfo userInfo)
        {
            Program.userInfo = userInfo;

            InitializeComponent();

            Shown += new EventHandler(Lobby_Shown);

            // room 할당
            RoomAllocation();
            
            LoadLobby();

            TitleBar titlebar = new TitleBar(this);
            keepAliveThread.Start();
        }

        private void Lobby_Shown(Object sender, EventArgs e)
        {
            reciveDataThread.Start();
            userListThread.Start();
            ChatingBar.Focus();
        }

        private void LoadLobby()
        {
            roomDefault[5] = "null";

            keepAliveThread = new Task(KeepAlive);
            reciveDataThread = new Task(resiveData);
            userListThread = new Task(userListCheck);

            // 유저 정보 요청
            Program.tcp.DataSend((int)PlayerOpcode.USER_INFO_REQUEST, Program.userInfo.index.ToString());
            waitData((int)PlayerOpcode.USER_INFO_REQUEST);

            // 유저 점수 요청
            Program.tcp.DataSend((int)PlayerOpcode.USER_SCORE_REQUEST, Program.userInfo.index.ToString());
            waitData((int)PlayerOpcode.USER_SCORE_REQUEST);

            // 방 정보 불러오기
            Program.tcp.DataSend((int)LobbyOpcode.ROOM_REFRESH, "");
            waitData((int)LobbyOpcode.ROOM_REFRESH);
            
            // 접속 성공 메세지
            ChatingLog.Text = "---------------------------접속에 성공하셨습니다----------------------------";

            UserNICK.Text = Program.userInfo.nick;
            UserSCORE.Text = Program.userInfo.win + " 승 " + Program.userInfo.lose + " 패 " + Program.userInfo.draw + " 무";
        }

        // 유저 목록을 체크
        private void userListCheck()
        {
            while ((Program.state%10) == 1)
            {
                Program.tcp.DataSend((int)LobbyOpcode.USER_REFRESH, "");
                Thread.Sleep(2999);
            }
        }

        // 서버와의 연결을 체크
        private void KeepAlive()
        {
            while (Program.state != 0)
            {
                Thread.Sleep(7999);
                Program.tcp.DataSend((int)GlobalOpcode.Keep_Alive,"");
            }
        }

        // 로비 로딩에 필요한 데이터를 순차적으로 받는 함수
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
                waitData(opCode);
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

        // 데이터를 받는 쓰레드를 위한 함수
        public void resiveData()
        {
            while ((Program.state % 10) == 1)
            {
                int dataleng, opcode, parameterNum;
                string data;
                byte[] bData;
                string[] parameter;

                Program.tcp.ReciveBData(out bData, out dataleng);
                
                data = Encoding.UTF8.GetString(bData);

                try
                {
                    parameter = data.Substring(5).Split('\u0001');
                    opcode = Convert.ToInt16(data.Substring(0, 3));
                    parameterNum = Convert.ToInt16(data.Substring(3, 2));
                }
                catch
                {
                    parameter = new string[0];
                    opcode = 999;
                    parameterNum = 99;
                    MessageBox.Show("통신오류");
                    Application.Exit();
                }

                BinaryFormatter bf = new BinaryFormatter();
                bf.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                MemoryStream ms = new MemoryStream();


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
                        ms.Write(bData, 5, dataleng - 5);
                        ms.Position = 0;
                        roomListInfo = (AvalonServer.RoomListInfo)bf.Deserialize(ms);
                        MaxPage = ((roomListInfo.getRoomCount() - 1) / 6) + 1;
                        SetRooms();
                        break;
                    case (int)LobbyOpcode.USER_REFRESH: // 유저목록 갱신
                        SetUserList(parameter);
                        break;
                    case (int)LobbyOpcode.ROOM_MAKE: // 방 만들기
                        Program.state = 12;

                        if (null == lobbyRoomMake)
                            MessageBox.Show("lobbyRoomMake 가 null 입니다.");
                        if (-1 == Convert.ToInt32(parameter[0]))
                            MessageBox.Show("방생성에서 받은 데이터 : " + parameter[0]);

                        Room room = new Room(0);
                        room.RoomName = lobbyRoomMake.name;
                        room.RoomType = lobbyRoomMake.type;
                        room.RoomPassword = lobbyRoomMake.pass;
                        room.RoomMaxMember = lobbyRoomMake.maxNumber;
                        room.RoomNumber = parameter[0];
                        //Program.lobby.reciveDataThread.Wait();

                        Program.room = new WaitingRoom(room);
                        lobbyRoomMake.LobbyRoomMakeClose();
                        LobbyClose();
                        break;
                    case (int)LobbyOpcode.ROOM_JOIN: // 방 들어가기
                        ms.Write(bData, 5, dataleng - 5);
                        ms.Position = 0;
                        
                        if (parameter[0] != "0")
                        {
                            Program.state = 12;
                            AvalonServer.RoomInfo roomInfo = (AvalonServer.RoomInfo)bf.Deserialize(ms);
                            Program.room = new WaitingRoom(roomInfo);
                            LobbyClose();
                        }
                        else
                        {
                            MessageBox.Show("방 들어가기 에러 : " + data);
                        }
                        break;
                    case (int)PlayerOpcode.USER_SCORE_REQUEST: // 유저전적 요청
                        Program.userInfo.setScore(Convert.ToInt16(parameter[0]), Convert.ToInt16(parameter[1]), Convert.ToInt16(parameter[2]));
                        break;
                    case (int)GlobalOpcode.Nomal_EXIT: // 정상접속종료
                        Program.state = 0;
                        Application.Exit();
                        break;
                    default:
                        break;
                }
                if(opcode == (int)GlobalOpcode.Nomal_EXIT || opcode == (int)LobbyOpcode.ROOM_MAKE || ((opcode == (int)LobbyOpcode.ROOM_JOIN) && (parameter[0] != "0"))) { break; }
            }
        }

        // 유저 리스트 세팅 크로스 스레드
        private void SetUserList(string[] users)
        {
            if (ChatingLog.InvokeRequired)
            {
                SetUserListCallback setUserListCallback = new SetUserListCallback(SetUserList);
                Invoke(setUserListCallback, new object[] { users });
            }
            else
            {
                UserList.Items.Clear();
                UserList.Items.AddRange(users);
            }
        }

        // 채팅로그 세팅 크로스 스레드
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

        // 방 세팅 크로스 스레드
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
                    if ((roomListInfo.getRoomCount() <= i + page) || roomListInfo.roomInfo[i+page] == null) { room[i].setRoomInfo(roomDefault); }
                    else {
                        room[i].setRoomInfo(roomListInfo.roomInfo[i + page].getRoomInfo());
                        if (roomListInfo.roomInfo[i + page].state == 1) { room[i].RoomRock(); }
                    }
                }
                RoomListIndex.Text = indexPage + " / " + MaxPage;
            }
        }

        // 종료 버튼
        private void Logout_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)GlobalOpcode.Nomal_EXIT, "");
        }

        // 종료 버튼
        private void Exit_Click(object sender, EventArgs e)
        {
            Program.tcp.DataSend((int)GlobalOpcode.Nomal_EXIT, "");
        }

        // 창 내림
        private void Minimalize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // 메세지 보내기 버튼
        private void SendMass_Click(object sender, EventArgs e)
        {
            if(ChatingBar.Text == ""){ return; }
            Program.tcp.DataSend((int)LobbyOpcode.CHAT, Program.userInfo.nick + delimeter + ChatingBar.Text);
            ChatingBar.Text = "";
        }

        // 채팅 엔터키 처리
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

        // 채팅로그 자동 줄바꿈
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
            lobbyRoomMake = new LobbyRoomMake(Program.tcp);
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
        public static DateTime Delay(int MS)
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
        public void cheakRoomPassword(string RoomNumber)
        {
            lobbyRoomPassword = new LobbyRoomPassword(RoomNumber);
            lobbyRoomPassword.ShowDialog();
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

        // 로비를 닫는 크로스 스레드
        private void LobbyClose()
        {
            if(InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    Close();
                });
            }
            else
            {
                Close();
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
