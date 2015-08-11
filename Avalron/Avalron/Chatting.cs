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
        System.Windows.Forms.RichTextBox chattingBox;

        public Chatting(Control.ControlCollection Controls)
        {
            chattingBox = new RichTextBox();
            chattingBox.Location = new System.Drawing.Point(15, 300);
            chattingBox.Name = "채팅";
            chattingBox.ReadOnly = true;
            chattingBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            chattingBox.Size = new System.Drawing.Size(971, 199);
            chattingBox.TabIndex = 2;
            chattingBox.Text = "";

            Controls.Add(chattingBox);
            chattingBox.ResumeLayout(false);
            chattingBox.PerformLayout();
        }

        public void RunChat()
        {
            while(Avalron.gameClient.IsClosed())
            {
                
            }
        }
    }
}
