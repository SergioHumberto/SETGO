using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web
{
    public class Email
    {
        public void SendEmail(string body, string correo, string CC, string BCC)
        {
            SMTPConfigBLL SMTPConfigBLL = new SMTPConfigBLL();
            SMTPConfigOBJ SMTPConfigOBJ = SMTPConfigBLL.SelectSMTPConfig();

            if ( SMTPConfigOBJ != null)
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(SMTPConfigOBJ.User);
                mail.To.Add(correo);
                if (CC != string.Empty) mail.CC.Add(CC);
                if(BCC != string.Empty) mail.Bcc.Add(BCC);
                mail.Subject = "Confirmación de Registro";
                mail.IsBodyHtml = true;

                mail.Body = body;

                SmtpClient SmtpServer = new SmtpClient(SMTPConfigOBJ.Server);
                SmtpServer.Port = SMTPConfigOBJ.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SMTPConfigOBJ.User, SMTPConfigOBJ.Password);
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
            }           
        }
    }
}