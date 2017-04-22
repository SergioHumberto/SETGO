using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;
using WebApplicationTemplate.Web.Tools;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class PaymentProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["paymentId"] != null)
                {
                    ProcesaURL();
					// CallWSPayPal();
				}
            }
        }

        private void ProcesaURL()
        {
            string numeroTransaccion = Request.QueryString["paymentId"];
            string statusPaypal = Request.QueryString["st"] != null ? Request.QueryString["st"] : "";
            string strIdParticipante = Request.QueryString["IdParticipante"];
			string strPrecio = Request.QueryString["amt"] != null ? Request.QueryString["amt"] : "1";
			string strIdCarrera = Request.QueryString["IdCarrera"];

			lblStatus.Text = statusPaypal;

            if (!string.IsNullOrEmpty(strIdParticipante))
            {
                int IdParticipante;
                if (int.TryParse(strIdParticipante, out IdParticipante))
                {
                    ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(HttpSecurity.CurrentSession);
                    ParticipantesOBJ objParticipante = objParticipanteBLL.SelectParticipanteObject(IdParticipante);

					CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
					CarreraOBJ carreraOBJ = new CarreraOBJ();

					int IdCarrera = 0;
					if(strIdCarrera != null && int.TryParse(strIdCarrera, out IdCarrera))
					{
						carreraOBJ = carreraBLL.SelectCarreraObject(IdCarrera);
					}

                    if (objParticipante != null)
                    {
                        objParticipante.StatusPaypal = statusPaypal;
                        objParticipante.TransactionNumber = numeroTransaccion;

                        objParticipanteBLL.UpdateParticipante(objParticipante);
                    }

					//Cadena para enviar el correo.
					string body = @"
					<table>
						<tr><td width=" + "50%"+ @">Selecciona una modalidad:</td><td width=" + "50%" + @">{0}</td></tr>
						<tr><td style="+"background:#F3F7FB;"+ @">Nombe:</td><td style=" + "background:#F3F7FB;" + @">{1}</td></tr>
						<tr><td>Fecha de Nacimiento:</td><td>{2}</td></tr>
						<tr><td style=" + "background:#F3F7FB;" + @">Email:</td><td style=" + "background:#F3F7FB;" + @">{3}</td></tr>
						<tr><td>Teléfono Personal:</td><td>{4}</td></tr>
						<tr><td style=" + "background:#F3F7FB;" + @">Teléfono de Contacto de Emergencia:</td><td style=" + "background:#F3F7FB;" + @">{5}</ td></tr>
						<tr><td>Dirección:</td><td>{6}</td></tr>
						<tr><td style=" + "background:#F3F7FB;" + @">" + carreraOBJ.DescripcionPoliticas + @"</td><td style=" + "background:#F3F7FB;" + @">{7}</td></tr>
						<tr><td>Total:</td><td>{8}</td></tr>
						<tr><td style=" + "background:#F3F7FB;" + @">Status:</td><td style=" + "background:#F3F7FB;" + @">{9}</td></tr>
						<tr><td>Payment ID:</td><td>{10}</td></tr>
						<tr><td style=" + "background:#F3F7FB;" + @">Payment Date:</td><td style=" + "background:#F3F7FB;" + @">{11}</td></tr>
					</table>
					";

					RamaBLL ramaBLL = new RamaBLL(HttpSecurity.CurrentSession);
					RamaOBJ ramaOBJ = new RamaOBJ();

					ramaOBJ = ramaBLL.SelectRamaByIdParticipante(objParticipante.IdParticipante);


					List<string> lstCamposCorreo = new List<string>();

					lstCamposCorreo.Add(ramaOBJ.Nombre);//Modalidad
					lstCamposCorreo.Add(objParticipante.Nombre +
										" " +
										objParticipante.ApellidoPaterno +
										" " +
										objParticipante.ApellidoPaterno);//Nombre
					lstCamposCorreo.Add(objParticipante.FechaNacimiento.ToString());//Fecha de nacimiento
					lstCamposCorreo.Add(objParticipante.Email);//Email
					lstCamposCorreo.Add(objParticipante.Telefono);//Telefono personal
					lstCamposCorreo.Add(objParticipante.TelefonoEmergencia);//Telefono emergencia
					lstCamposCorreo.Add(objParticipante.Domicilio);//Dirección
					lstCamposCorreo.Add("Acepto");//Terminos
					lstCamposCorreo.Add(strPrecio.ToString());//Total
					lstCamposCorreo.Add(objParticipante.StatusPaypal);//Status
					lstCamposCorreo.Add(objParticipante.TransactionNumber);//PaymentID
					lstCamposCorreo.Add(DateTime.Today.ToString());//Payment Date

					body = string.Format(body
						, ramaOBJ.Nombre								//Modalidad
						, objParticipante.Nombre +" " +
							objParticipante.ApellidoPaterno +" " +
							objParticipante.ApellidoMaterno				//Nombre
						, objParticipante.FechaNacimiento.ToShortDateString()   //Fecha de nacimiento
						, objParticipante.Email							//Email
						, objParticipante.Telefono						//Telefono personal
						, objParticipante.TelefonoEmergencia			//Telefono emergencia
						, objParticipante.Domicilio						//Dirección
						, "Acepto"										//Terminos
						, strPrecio.ToString()							//Total
						, objParticipante.StatusPaypal					//Status
						, objParticipante.TransactionNumber				//PaymentID
						, DateTime.Now.ToString()						//Payment Date
						);

					tablaNotificacion.InnerHtml = body;

					Email email = new Email();
					email.SendEmail(body, objParticipante.Email, carreraOBJ.CC,carreraOBJ.BCC);
				}
			}
        }

        private void CallWSPayPal()
        {
            int IdCarreraProperty = 1;
            CarreraBLL objCarreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
            CarreraOBJ objCarreraOBJ = objCarreraBLL.SelectCarreraObject(IdCarreraProperty);

            string authToken = string.Empty;
            if (objCarreraOBJ != null)
            {
                // ejemplo: "wyws0SQYueHY3xZJte9l9nr4h1OT7FGixDL0a3bJwqwY0ABJbKoZkxzibR4"
                authToken = objCarreraOBJ.TokenPaypalTDP;
            }

            string txToken = Request.QueryString["tx"];
            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;

            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";

            //ServicePointManager.ServerCertificateValidationCallback =
            //    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //    { return true; };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback +=
                new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(query);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            Dictionary<string, string> results = new Dictionary<string, string>();
            if (strResponse != "")
            {
                // divRespuestaPaypal.Visible = true;

                StringReader reader = new StringReader(strResponse);
                string line = reader.ReadLine();

                if (line == "SUCCESS")
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        results.Add(line.Split('=')[0], line.Split('=')[1]);

                    }

                    // lblTituloRespuesta.Text = "Tu orden ha sido recibida";
                    // lblNombre.Text = results["first_name"] + " " + results["last_name"];
                    // lblItem.Text = results["item_name"];

                    // UpdatePayment(results["custom"]);
                    // Response.Write("<li>Amount: " + results["payment_gross"] + "</li>");
                    // Response.Write("<hr>");
                }
                else if (line == "FAIL")
                {
                    // Log for manual investigation
                    // lblTituloRespuesta.Text = "No se pudo recibir detalles de la transaccion";
                }
            }
            else
            {
                //unknown error
                Response.Write("ERROR");
            }
        }

        public void HasRequest()
        {
            string authToken = "wyws0SQYueHY3xZJte9l9nr4h1OT7FGixDL0a3bJwqwY0ABJbKoZkxzibR4"; // Este valor debe ir en algun lado
            string txToken = Request.QueryString["tx"];
            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;

            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";

            //ServicePointManager.ServerCertificateValidationCallback =
            //    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //    { return true; };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback +=
                new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            


            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(query);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            Dictionary<string, string> results = new Dictionary<string, string>();
            if (strResponse != "")
            {
                StringReader reader = new StringReader(strResponse);
                string line = reader.ReadLine();

                if (line == "SUCCESS")
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        results.Add(line.Split('=')[0], line.Split('=')[1]);

                    }
                    Response.Write("<p><h3>Your order has been received.</h3></p>");
                    Response.Write("<b>Details</b><br>");
                    Response.Write("<li>Name: " + results["first_name"] + " " + results["last_name"] + "</li>");
                    Response.Write("<li>Item: " + results["item_name"] + "</li>");
                    Response.Write("<li>Amount: " + results["payment_gross"] + "</li>");
                    Response.Write("<hr>");
                }
                else if (line == "FAIL")
                {
                    // Log for manual investigation
                    Response.Write("Unable to retrive transaction detail");
                }
            }
            else
            {
                //unknown error
                Response.Write("ERROR");
            }
        }
       
        protected void btnRegistraOtroParticipante_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PublicPages/RegistroParticipantes.aspx?IdCarrera=" + Request.QueryString["IdCarrera"]);
        }
    }
}