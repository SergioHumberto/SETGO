using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;

namespace WebApplicationTemplate.Web
{
	public class Email
	{
		public void SendEmail(string body, string correo)
		{
			try
			{
				MailMessage mail = new MailMessage();
				/*********************************************
                 * With MailTrap               
                 *********************************************/
				mail.From = new MailAddress("02b4763913-63cf25@inbox.mailtrap.io");
				mail.To.Add(correo);
				mail.To.Add("info@setgo.mx");//Correo en duro para carrera del 22 de abril
				mail.Subject = "Test Mail";
				mail.IsBodyHtml = true;
				
				mail.Body = body;

				SmtpClient SmtpServer = new SmtpClient("smtp.mailtrap.io");
				SmtpServer.Port = 2525;
				SmtpServer.Credentials = new System.Net.NetworkCredential("7a177f25b5da9a", "a7258f87908b8b");
				SmtpServer.EnableSsl = false;

				SmtpServer.Send(mail);
				//lblMessage.Text = ">>> Correo enviado!!!";
			}
			catch (Exception ex)
			{
				//lblMessage.Text = ex.ToString();
			}
		}
	}
}