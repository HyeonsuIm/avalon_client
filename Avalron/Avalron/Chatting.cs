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
        TextBox chatText = new TextBox();

        public Chatting(Control.ControlCollection Controls)
        {
            chattingBox.Location = new System.Drawing.Point(15, 300);
            chattingBox.Name = "채팅";
            chattingBox.ReadOnly = true;
            chattingBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            chattingBox.Size = new System.Drawing.Size(500, 200);
            chattingBox.TabIndex = 2;
            chattingBox.Text = "";
            
            chatOption.FormattingEnabled = true;
            chatOption.Location = new System.Drawing.Point(14, 522);
            chatOption.Items.AddRange(new object[] {
            "전체",
            "귀속말"});
            chatOption.SelectedIndex = 0;
            chatOption.Name = "옵션";
            chatOption.Size = new System.Drawing.Size(60, 20);
            chatOption.TabIndex = 2;

            chatText.Location = new System.Drawing.Point(80, 522);
            chatText.Name = "채팅내용";
            chatText.Size = new System.Drawing.Size(430, 21);
            chatText.TabIndex = 3;
            chatText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChatKeyDown);

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
            while(Avalron.gameClient.IsClosed() == false)
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

            Avalron.gameClient.ChatSend("가나다:", chatText.Text);

            //chatText.Text += "\n";
            //chattingBox.Text += chatText.Text;
            chatText.Text = "";
        }
    }
}
