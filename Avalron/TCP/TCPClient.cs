﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Avalron
{
    public class TCPClient
    {
        public enum FormNum { LOGIN , LOBBY, GAME };
        enum OpCode { LOGIN_REQUEST = 10, ID_CHECK, NICK_CHECK, EMAIL_CHECK, REGISTER, FIND_ID, FIND_PW};
        static public string delimiter = "\u0001";
        byte[] data = new byte[1024];
        string output;
        string input, stringData;
        string [] ArrData;
        //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("203.255.3.92"), 9050);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public int recv = 0;
        private bool disposed = false;
        Spriter sp;

        public TCPClient()
        {
            Initalize();
        }

        public TCPClient(string address)
        {
            try
            {
                IsValAddress(address);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            ipep = new IPEndPoint(IPAddress.Parse(address), 9050);

            Initalize();
        }
        ~TCPClient()
        {
            //MessageBox.Show("서버와 연결을 끊습니다.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void Initalize()
        {
            Cursor.Current = Cursors.WaitCursor;
            MessageBox.Show("서버와 연결을 시작합니다.");
            try
            {
                //server.SendTimeout = 1000;
                //server.ReceiveTimeout = 1000;
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("서버와 연결하는데 실패하였습니다." + e.Message);
                return;
            }
            recv = server.Receive(data);
            output = Encoding.UTF8.GetString(data, 0, recv);
            Cursor.Current = Cursors.Default;
        }

        public void IsValAddress(string ipAddress)
        {
            IPAddress address = IPAddress.Parse(ipAddress);
        }

        public void Close()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
                ((IDisposable)this).Dispose();
                //safeHandle.Dispose();
            disposed = true;
        }

        public string[] Send(string line)
        {
            if (0 == recv)
            {
                MessageBox.Show("서버와 연결이 되어있지 않습니다.");
                ArrData = new string[1];
                ArrData[0] = "-2";
                return ArrData;
            }

            server.Send(Encoding.UTF8.GetBytes(line));
            data = new byte[1024];
            recv = server.Receive(data);

            if (recv == 0 || recv == -1)
                MessageBox.Show("연결끊겼다" + recv);

            output = Encoding.UTF8.GetString(data, 0, recv);
            if (output == "")
            {
                ArrData = new string[1];
                ArrData[0] = "-1";
                return ArrData;
            }

            sp = new Spriter(output);
            string [] splited = sp.getSplit();

            //return ArrData;
            return splited;
        }
        
        // 로그인 성공시 일련번호, 실패시 -1
        public int Login(string ID, string PW)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.LOGIN_REQUEST + "02" + delimiter + ID + delimiter + PW);
            //바이트 수 계산용
            Encoding.Default.GetByteCount("asd");

            IsValidOp((int)OpCode.LOGIN_REQUEST);

            return Convert.ToInt32(ArrData[1]);
        }
        // 회원가입 될시 0, 실패시 에러코드 반환
        public int Register(string ID, string PW, string Nick, string Email)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.REGISTER + "04" + delimiter + ID + delimiter + PW + delimiter + Nick + delimiter + Email);

            IsValidOp(14);

            return Convert.ToInt32(ArrData[1]);
        }
        // 중복 있을시 true, 없을시 false
        public bool IDCheck(string ID)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.ID_CHECK + "01" + delimiter + ID);

            IsValidOp(11);

            return Convert.ToBoolean(ArrData[1]);
        }
        // 중복 있을시 true, 없을시 false
        public bool NickCheck(string Nick)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.NICK_CHECK + "01" + delimiter + Nick);

            IsValidOp(12);

            return Convert.ToBoolean(ArrData[1]);
        }
        // 찾을시 true, 못찾을시 false
        public bool EMailCheck(string Email)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.EMAIL_CHECK + "01" + delimiter + Email);

            IsValidOp(13);

            return Convert.ToBoolean(ArrData[1]);
        }

        // 찾은 ID 반환, 못찾을 시 NULL
        public string FindID(string Email)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.FIND_ID + "01" + delimiter + Email);

            IsValidOp(15);

            return ArrData[1];
        }
        // 찾으면 true, 못찾을시 false
        public bool FindPW(string ID, string Email)
        {
            ArrData = Send((int)FormNum.LOGIN + "" + (int)OpCode.FIND_PW + "02" + delimiter + ID + delimiter + Email);

            IsValidOp(16);

            return Convert.ToBoolean(ArrData[1]);
        }
        public void loadingBar()
        {

        }

        protected bool IsValidOp(int opName)
        {
            if (sp.getCnt() != 1 && sp.getForm() != 0 && sp.getOpCode() != opName)
            {
                throw new Exception("예상한 op 코드가 아닙니다.");
                return false;
            }

            return true;
        }


        // 아래로 사용 함수
        public void Login_Send(string line)
        {
            input = line;
            server.Send(Encoding.UTF8.GetBytes(input));
            data = new byte[1024];
            recv = server.Receive(data);
            stringData = Encoding.UTF8.GetString(data, 0, recv);
            MessageBox.Show(stringData);
        }

        public void DataSend(string opcode, string line)
        {
            string message = opcode + line;
            data = Encoding.UTF8.GetBytes(message);
            server.Send(data);
        }

        public string ReciveData()
        {
            data = new byte[1024];
            recv = server.Receive(data);
            stringData = Encoding.UTF8.GetString(data, 0, recv);
            return stringData;
        }

        public void Room_Refresh(string opcode)
        {
            server.Send(Encoding.UTF8.GetBytes(opcode));
        }
    }
}