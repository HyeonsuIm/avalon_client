using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public class Chatting
    {
        Panel panel = new Panel();
        System.Windows.Forms.RichTextBox chattingBox = new RichTextBox();
        TextBox chatText = new TextBox();
        protected int formNum = (int)TCPClient.FormNum.LOBBY;
        bool chatFirst = true;

        public bool ChatBoxEnabled
        {
            get
            {
                return chattingBox.Enabled;
            }
            set
            {
                chattingBox.Enabled = value;
            }
        }
        public Chatting(Control.ControlCollection Controls)
        {
            chattingBox.Location = new System.Drawing.Point(0, 0);
            chattingBox.Name = "채팅";
            chattingBox.ReadOnly = true;
            chattingBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            chattingBox.Size = new System.Drawing.Size(650, 370);
            chattingBox.TabIndex = 2;
            chattingBox.Text = "";
            chattingBox.TextChanged += new System.EventHandler(this.chattingBox_TextChanged);

            chatText.Location = new System.Drawing.Point(0, chattingBox.Size.Height + 10);
            chatText.Name = "채팅내용";
            chatText.Text = "채팅내용";
            chatText.Size = new System.Drawing.Size(chattingBox.Width, 20);
            chatText.TabIndex = 3;
            chatText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChatKeyDown);
            chatText.Enter += new System.EventHandler(this.chatTextEnter);

            panel.Controls.Add(chattingBox);
            panel.Controls.Add(chatText);
            panel.Location = new System.Drawing.Point(20, 300);
            panel.Size = new System.Drawing.Size(chattingBox.Size.Width, chatText.Location.Y + chatText.Size.Height);
            panel.BackColor = System.Drawing.Color.Transparent;
            
            Controls.Add(panel);
            chattingBox.ResumeLayout(false);
            chattingBox.PerformLayout();
        }

        public System.Drawing.Point Location
        {
            get; set;
        }
        private delegate void Delegate(string text, string color);

        public void addText(string text, string color = "#000000")  // Black
        {
            try
            {
                if (this.chattingBox.InvokeRequired)
                {
                    chattingBox.Invoke(new Delegate(addText), new object[] { text, color});
                }
                else
                {
                    int startLength = chattingBox.TextLength;
                    this.chattingBox.AppendText(text + '\n');
                    this.chattingBox.SelectionStart = startLength;
                    this.chattingBox.SelectionLength = text.Length;
                    this.chattingBox.SelectionColor = System.Drawing.ColorTranslator.FromHtml(color);
                    chatText.Focus();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public void addSystemText(string text)
        {
            string line = "[System]" + text;
            addText(line, "#FF0000");   // Red

        }

        private void ChatKeyDown(object sender, KeyEventArgs e)

        {
            // 엔터키만 처리합니다.
            if (e.KeyCode !=  Keys.Enter)
                return;

            // 아무 내용 없거나 금지상태이면 넘깁니다.
            if (chatText.Text == "" || chatText.Enabled == false)
                return;

            switch (Program.cmd.Splite(chatText.Text)) // 전체 채팅
            {
                case Command.Option.All:
                    Program.avalron.gameClient.DataSend((int)AvalronClient.ChattingOpCode.CHATSEND, Program.userInfo.nick + TCPClient.delimiter + chatText.Text);
                    break;

                case Command.Option.Wisper:
                    Program.avalron.gameClient.DataSend((int)AvalronClient.ChattingOpCode.CHATSEND, Program.userInfo.nick + TCPClient.delimiter + Program.cmd.GetNick(chatText.Text) + TCPClient.delimiter + Program.cmd.GetText(chatText.Text));
                    break;

                case Command.Option.Err:
                    return;

                default:
                    throw new Exception("먼값을 선택한거여");
            }

            chatText.Text = "";
        }

        private void chattingBox_TextChanged(object sender, EventArgs e)
        {
            chattingBox.SelectionStart = chattingBox.TextLength;
            chattingBox.ScrollToCaret();
        }

        private void chatTextEnter(object sender, EventArgs e)
        {
            if (false == chatFirst)
                return;

            chatText.Text = "";
            chatFirst = false;
        }
        
        private delegate void ChattingOnOffCallBack(bool state);

        public void chattingOnOff(bool state)
        {
            if (chattingBox.InvokeRequired)
            {
                ChattingOnOffCallBack chattingOnOffCallBack = new ChattingOnOffCallBack(chattingOnOff);
                chattingBox.Invoke(chattingOnOffCallBack, new object[] { state });
            }
            else
            {
                chatText.Enabled = state;
                if (true == state)
                {
                    chatText.Text = "";
                }
                else
                {
                    chatText.Text = "채팅 금지상태입니다.";
                }
            }
        }
    }
}
