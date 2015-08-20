using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    class Chatting
    {
        System.Windows.Forms.RichTextBox chattingBox = new RichTextBox();
        ComboBox chatOption = new ComboBox();
        string[] chatOptionStr = {"전체", "귓속말" };
        TextBox chatText = new TextBox();
        bool chatFirst = true;

        public Chatting(Control.ControlCollection Controls)
        {
            chattingBox.Location = new System.Drawing.Point(15, 250);
            chattingBox.Name = "채팅";
            chattingBox.ReadOnly = true;
            chattingBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            chattingBox.Size = new System.Drawing.Size(500, 250);
            chattingBox.TabIndex = 2;
            chattingBox.Text = "";
            
            chatOption.FormattingEnabled = true;
            chatOption.Location = new System.Drawing.Point(14, 522);
            chatOption.Items.AddRange(chatOptionStr);
            chatOption.SelectedIndex = 0;
            chatOption.Name = "옵션";
            chatOption.Size = new System.Drawing.Size(60, 20);
            chatOption.TabIndex = 2;
            chatOption.SelectedIndexChanged += new System.EventHandler(this.chatOption_SelectedIndexChanged);

            chatText.Location = new System.Drawing.Point(80, 522);
            chatText.Name = "채팅내용";
            chatText.Text = "채팅내용";
            chatText.Size = new System.Drawing.Size(430, 21);
            chatText.TabIndex = 3;
            chatText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChatKeyDown);
            chatText.Enter += new System.EventHandler(this.chatTextEnter);

            Controls.Add(chattingBox);
            Controls.Add(chatOption);
            Controls.Add(chatText);
            chattingBox.ResumeLayout(false);
            chattingBox.PerformLayout();
        }

        public delegate void Delegate(string text);

        public void addText(string text)
        {
            try
            {
                if (this.chattingBox.InvokeRequired)
                {
                    chattingBox.Invoke(new Delegate(addText), new object[] { text });
                }
                else
                {
                    this.chattingBox.Text += text;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public void RunGetChat()
        {
            string getString = "";
            while(Avalron.gameClient.IsClosing() == false)
            //while(true)
            {
                try {
                    getString = Avalron.gameClient.ReciveData() + "\n";
                }
                catch(System.Net.Sockets.SocketException e)
                {
                    //MessageBoxEx.Show(this, e.Message);
                }
                 
                if (getString == "")
                    continue;
                addText(getString);

                getString = "";

                // 채팅 금지시
                if(false)
                {
                    chattingBox.Enabled = false;
                }

                // 채팅 금지 해지시
                if(false)
                {
                    chattingBox.Enabled = true;
                }
            }
        }

        private void ChatKeyDown(object sender, KeyEventArgs e)

        {
            // 엔터키만 처리합니다.
            if (e.KeyCode !=  Keys.Enter)
                return;

            // 아무 내용 없을시 넘깁니다.
            if (chatText.Text == "")
                return;

            switch(Program.cmd.Splite(chatText.Text)) // 전체 채팅
            {
                case Command.Option.All:
                Avalron.gameClient.ChatSend(Program.userInfo.nick, chatText.Text);
                    break;

                case Command.Option.Wisper:
                    Avalron.gameClient.WisperSend(Program.userInfo.nick, Program.cmd.GetNick(chatText.Text), Program.cmd.GetText(chatText.Text));
                    break;

                case Command.Option.Err:
                    return;

                default:
                    throw new Exception("먼값을 선택한거여");
            }

            //chatText.Text += "\n";
            //chattingBox.Text += chatText.Text;
            chatText.Text = "";
        }

        private void chatOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((string)chatOption.SelectedItem == "전체")
            {
                chatText.Text = "";
            }
            else if((string)chatOption.SelectedItem == "귓속말")
            {
                chatText.Text = "/w";
            }
        }

        private void chatTextEnter(object sender, EventArgs e)
        {
            if (false == chatFirst)
                return;

            chatText.Text = "";
            chatFirst = false;
        }
    }
}
