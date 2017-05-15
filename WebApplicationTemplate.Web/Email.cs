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
        public void SendEmail(string body, string correo, string CC, string BCC, int folio)
        {
            SMTPConfigBLL SMTPConfigBLL = new SMTPConfigBLL();
            SMTPConfigOBJ SMTPConfigOBJ = SMTPConfigBLL.SelectSMTPConfig();

            if (SMTPConfigOBJ != null)
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(SMTPConfigOBJ.User);
                if (correo != null && correo != string.Empty) mail.To.Add(correo);
                if (CC != null && CC != string.Empty)
                {
                    CC = CC.Replace(" ","");
                    string[] ccs = CC.Split(',');

                    foreach(string cc in ccs)
                    {
                        mail.CC.Add(cc);
                    }                    
                }
                
                if (BCC != null && BCC != string.Empty)
                {
                    BCC = BCC.Replace(" ", "");
                    string[] bccs = BCC.Split(',');

                    foreach (string bcc in bccs)
                    {
                        mail.Bcc.Add(bcc);
                    }
                }
                
                mail.Subject = @"Confirmación de Registro [#" + folio + "]";
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