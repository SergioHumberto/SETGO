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
                if (Request.QueryString["tx"] != null)
                {
                    ProcesaURL();
                    // CallWSPayPal();
                }
            }
        }

        private void ProcesaURL()
        {
            string numeroTransaccion = Request.QueryString["tx"];
            string statusPaypal = Request.QueryString["st"];
            string strIdParticipante = Request.QueryString["IdParticipante"];

            lblStatus.Text = statusPaypal;

            if (!string.IsNullOrEmpty(strIdParticipante))
            {
                int IdParticipante;
                if (int.TryParse(strIdParticipante, out IdParticipante))
                {
                    ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(HttpSecurity.CurrentSession);
                    ParticipantesOBJ objParticipante = objParticipanteBLL.SelectParticipanteObject(IdParticipante);

                    if (objParticipante != null)
                    {
                        objParticipante.StatusPaypal = statusPaypal;
                        objParticipante.TransactionNumber = numeroTransaccion;

                        objParticipanteBLL.UpdateParticipante(objParticipante);
                    }
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
    }
}