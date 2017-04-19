using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;

namespace WebApplicationTemplate.Web
{
	public class Email
	{
		public void SendEmail(List<string> lstCamposCorreo)
		{
			try
			{
				MailMessage mail = new MailMessage();
				/*********************************************
                 * With MailTrap               
                 *********************************************/
				mail.From = new MailAddress("02b4763913-63cf25@inbox.mailtrap.io");
				mail.To.Add("humberto1_sergio@hotmail.com");
				mail.Subject = "Test Mail";
				mail.IsBodyHtml = true;

				string body = string.Empty;

				body = @"
				<table border='1'>
					<tr><td>Selecciona una modalidad:</td><td></td>{0}</tr>
					<tr><td>Nombe:</td><td></td>{1}</tr>
					<tr><td>Fecha de Nacimiento:</td><td></td>{2}</tr>
					<tr><td>Email:</td><td></td>{3}</tr>
					<tr><td>Club:</td><td></td>{4}</tr>
					<tr><td>Teléfono Personal:</td><td></td>{5}</tr>
					<tr><td>Teléfono de Contacto de Emergencia:</td><td></td>{6}</tr>
					<tr><td>Dirección:</td><td></td>{7}</tr>
					<tr><td>El comité organizador y la empresa que lo organiza, así como todos los patrocinadores involucrados en el evento, no se hacen responsables de los corredores, al momento de aceptar estos términos, el Atleta que está llenando este formulario de inscripción con sus datos, está consciente de que tiene la salud apta para participar en un evento de este tipo, en cualquier de sus ediciones, 6 Kilómetros, 11 Kilómetros o 21 Kilómetros.</td><td></td>{8}</tr>
					<tr><td>Total:</td><td></td>{9}</tr>
					<tr><td>Status:</td><td></td>{10}</tr>
					<tr><td>Payment ID:</td><td></td>{11}</tr>
					<tr><td>Payment Date:</td><td></td>{12}</tr>
					<tr><td>Full Name:</td><td></td>{13}</tr>
				</table>
				";

				body = string.Format(body
					, lstCamposCorreo[0]
					, lstCamposCorreo[1]
					, lstCamposCorreo[2]
					, lstCamposCorreo[3]
					, lstCamposCorreo[4]
					, lstCamposCorreo[5]
					, lstCamposCorreo[6]
					, lstCamposCorreo[7]
					, lstCamposCorreo[8]
					, lstCamposCorreo[9]
					, lstCamposCorreo[10]
					, lstCamposCorreo[11]
					, lstCamposCorreo[12]
					, lstCamposCorreo[13]);

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