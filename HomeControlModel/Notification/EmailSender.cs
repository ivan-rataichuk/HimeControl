using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HomeControlModel.Notification
{
    class EmailSender
    {
        public void Send(string message)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("rataichuktestapp@gmail.com");
            mail.To.Add("rockarolla6666@gmail.com");
            mail.Subject = "HomeControl Security";
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("rataichuktestapp@gmail.com", "air051088");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
