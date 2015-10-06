using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron
{
    public partial class Confirm : Form
    {
        private int confirmNum; // 인증 번호 저장
        string Email;           // 인증 보낼 주소
        string EmailHead;       // 이메일 제목
        string EmailBody;       // 이메일 내용1
        string EmailBody2;      // 이메일 내용2      -> 내용1 + code + 내용2

        bool authorized = false;

        string SmtpID = "rikuo777@gmail.com";
        string SmtpPW = "hayate123";

        Label EmailAddress;
        Label abc;
        Label warning;

        public Confirm(string Email)
        {
            this.Email = Email;
            EmailHead = "Avalron에 가입해주셔서 감사합니다.";
            EmailBody = "회원가입해 주셔서 감사합니다. </br> 가입을 위한 인증번호는 다음과 같습니다. </br>";
            EmailBody2 = "</br>본 이메일은 발신 전용 메일입니다. ";

            EmailAddress = new Label();
            EmailAddress.Text = Email + "로 인증메일을 전송하였습니다.";
            EmailAddress.Location = new Point(21, 26);
            EmailAddress.AutoSize = true;
            this.Controls.Add(EmailAddress);

            abc = new Label();
            abc.Text = confirmNum.ToString();
            abc.Location = new Point(0, 0);
            abc.AutoSize = true;
            this.Controls.Add(abc);

            InitializeComponent();

            Send();
        }

        private void ReSent_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void Send()
        {

            Random rand = new Random();
            confirmNum = rand.Next(100000000, 999999999);   // 9자리의 난수입니다.

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(SmtpID, SmtpPW),
                EnableSsl = true,
                Timeout = 20000,
            };
            MailMessage mail = new MailMessage();
            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;                         // 메일 본문 내용을 html로 설정.
            mail.From = new MailAddress(SmtpID);            // 보내는 사람
            mail.To.Add(new MailAddress(Email));            // 받는 사람
            mail.Subject = EmailHead;
            mail.Body = EmailBody + confirmNum.ToString() + EmailBody2;
            
            //smtp.Port = SmtpPort;
            //smtp.UseDefaultCredentials = true;              // 기본 자격증명 사용여부
            //smtp.EnableSsl = true;                          // ssl 사용
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.Credentials = new NetworkCredential(SmtpID, SmtpPW);
            //smtp.Credentials = new System.Net.NetworkCredential(@"rikuo777@gmail.com", "hayate123");
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ep)
            {
                MessageBoxEx.Show(this, "전송에 실패하였습니다." + ep.Message);
                return;
            }
            finally
            {
                mail.Dispose();
                MessageBoxEx.Show(this, "전송되었습니다.");
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if(ConfirmBox.Text == confirmNum.ToString())
            {
                ConfirmBox.BackColor = Color.White;
                authorized = true;
                Close();
                return;
            }

            printWarning("일치하지 않습니다.");
        }

        private void printWarning(string str)
        {
            ConfirmBox.BackColor = Color.Red;
            warning = new Label();
            warning.ForeColor = Color.Red;
            warning.Text = str;
            warning.AutoSize = true;
            warning.Location = new Point(20, 120);
            this.Controls.Add(warning);
        }

        public bool IsAuthorized()
        {
            return authorized;
        }
    }
}
