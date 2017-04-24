using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;
using WebApplicationTemplate.Web.Tools;

using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class PayPalRestAPI : System.Web.UI.Page
    {
        private string status;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PayerID"] != null)
                {
                    status = string.Empty;
                    ProcessURL();
                }
                else if (Session["objSessionPayPal"] != null)
                {
                    TestMethod();
                }
            }
        }

        private void ProcessURL()
        {
            // exmple url parameters: paymentId=PAY-82T48127285547200LD5W3LQ&token=EC-7WB48951HL5170437&PayerID=WF63RTAYUPJEN
            if (Session["paymentId"] != null)
            {
                SessionPayPal objSessionPayPal = (SessionPayPal)Session["objSessionPayPal"];

                var paymentId = Session["paymentId"].ToString();
                var payment = new Payment() { id = paymentId };

                var payerId = Request.QueryString["PayerID"];
                var paymentExecution = new PaymentExecution() { payer_id = payerId };

                var apiContext = GetAPIContext();

                var executedPayment = payment.Execute(apiContext, paymentExecution);
                status = executedPayment.payer.status;

                string numeroTransaccion = Request.QueryString["paymentId"];
                int idParticipante = objSessionPayPal.IdParticipante;
                decimal precio = objSessionPayPal.amount;
                int idCarrera = objSessionPayPal.IdCarrera;

                if (idParticipante > 0)
                {
                    ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(HttpSecurity.CurrentSession);
                    ParticipantesOBJ objParticipante = objParticipanteBLL.SelectParticipanteObject(idParticipante);

                    CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
                    CarreraOBJ carreraOBJ = new CarreraOBJ();

                    carreraOBJ = carreraBLL.SelectCarreraObject(idCarrera);

                    if (objParticipante != null)
                    {
                        objParticipante.StatusPaypal = status;
                        objParticipante.TransactionNumber = numeroTransaccion;

                        objParticipanteBLL.UpdateParticipante(objParticipante);
                    }

                    //Cadena para enviar el correo.
                    string body = @"
						<table>
							<tr><td width=" + "50%" + @">Selecciona una modalidad:</td><td width=" + "50%" + @">{0}</td></tr>
							<tr><td style=" + "background:#F3F7FB;" + @">Nombe:</td><td style=" + "background:#F3F7FB;" + @">{1}</td></tr>
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

                    body = string.Format(body
                        , ramaOBJ.Nombre                                //Modalidad
                        , objParticipante.Nombre + " " +
                            objParticipante.ApellidoPaterno + " " +
                            objParticipante.ApellidoMaterno             //Nombre
                        , objParticipante.FechaNacimiento.ToShortDateString()   //Fecha de nacimiento
                        , objParticipante.Email                         //Email
                        , objParticipante.Telefono                      //Telefono personal
                        , objParticipante.TelefonoEmergencia            //Telefono emergencia
                        , objParticipante.Domicilio                     //Dirección
                        , "Acepto"                                      //Terminos
                        , precio.ToString()                             //Total
                        , status                                        //Status
                        , objParticipante.TransactionNumber             //PaymentID
                        , DateTime.Now.ToString()                       //Payment Date
                        );

                    tablaNotificacion.InnerHtml = body;

                    Email email = new Email();
                    email.SendEmail(body, objParticipante.Email, carreraOBJ.CC, carreraOBJ.BCC);
                }

                Session.Remove("paymentId");

            }
        }

        private APIContext GetAPIContext()
        {
            var config = ConfigManager.Instance.GetProperties();

            config.Add("mode", WebSettings.ModePayPayRestAPI);
            config.Add("clientId", WebSettings.ClientIDPayPalRestAPI); // "AQCXi-7ZYtxy_0MvMMeez-JCQx4xITJSmpwKj9m3EKsoWTBFHLN9ksnHtCxx2rG3UwrXoEj2mizbE4Pp");
            config.Add("clientSecret", WebSettings.ClientSecretPayPalRestAPI); // "EHr9h68sZYIqsIZVZcO1Xml_IOcRkt5u77jdYRJMO1UZ5POL43Pa821WJh5apDY_AyV3eiPwqNGqPzVV");

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            var apiContext = new APIContext(accessToken);
            return apiContext;
        }

        private void TestMethod()
        {
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");

            SessionPayPal objSessionPayPal = (SessionPayPal)Session["objSessionPayPal"];

            var apiContext = GetAPIContext();

            decimal postagePackingCost = 0;
            decimal examPaperPrice = objSessionPayPal.amount;
            int quantityOfExamPapers = 1;
            decimal subTotal = (quantityOfExamPapers * examPaperPrice);
            decimal total = subTotal + postagePackingCost;

            var examPaperItem = new Item();
            examPaperItem.name = objSessionPayPal.item_name;
            examPaperItem.currency = "MXN";
            examPaperItem.price = examPaperPrice.ToString();
            examPaperItem.sku = "PEPCO5027m15"; // code manufacture
            examPaperItem.quantity = quantityOfExamPapers.ToString();

            var transactionDetails = new Details();
            transactionDetails.tax = "0";
            transactionDetails.shipping = postagePackingCost.ToString();
            transactionDetails.subtotal = subTotal.ToString("0.00");

            var transactionAmount = new Amount();
            transactionAmount.currency = "MXN";
            transactionAmount.total = total.ToString("0.00");
            transactionAmount.details = transactionDetails;

            CarreraBLL objCarreraBLL = new CarreraBLL(Tools.HttpSecurity.CurrentSession);
            CarreraOBJ objCarreraOBJ = objCarreraBLL.SelectCarreraObject(objSessionPayPal.IdCarrera);

            var payee = new Payee();
            payee.email = objCarreraOBJ.PayPalEmail; // Este correo debe de venir de la base de datos.

            var transaction = new Transaction();
            transaction.description = "your orden of past exam papers";
            transaction.invoice_number = Guid.NewGuid().ToString(); // autogenerado 
            transaction.amount = transactionAmount;
            transaction.item_list = new ItemList { items = new List<Item> { examPaperItem } };
            transaction.payee = payee; // el Email del beneficiado debe ir en la transaccion, no en el pago directamente.

            var payer = new Payer();
            payer.payment_method = "paypal";

            var redirectUrls = new RedirectUrls();
            redirectUrls.cancel_url = Urls.Abs("~/PublicPages/PayPalRestAPI.aspx"); // URL cuando cancela el pago
            redirectUrls.return_url = Urls.Abs("~/PublicPages/PayPalRestAPI.aspx"); // URL cuando hace el pago

            //Session.Remove("objSessionPayPal");

            try
            {
                var payment = Payment.Create(apiContext, new Payment
                {
                    intent = "sale",
                    payer = payer,
                    transactions = new List<Transaction> { transaction },
                    redirect_urls = redirectUrls
                });

                Session.Add("paymentId", payment.id);

                foreach (var link in payment.links)
                {
                    if (link.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        // found the appropiate link, send the user there
                        Response.Redirect(link.href, false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                litMessage.Text = ex.Message;
            }
        }

        protected void btnRegistraOtroParticipante_Click(object sender, EventArgs e)
        {
            SessionPayPal objSessionPayPal = (SessionPayPal)Session["objSessionPayPal"];

            if (objSessionPayPal != null)
            {
                Session.Remove("objSessionPayPal");

                CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
                CarreraOBJ carreraOBJ = new CarreraOBJ();

                carreraOBJ = carreraBLL.SelectCarreraObject(objSessionPayPal.IdCarrera);

                if (carreraOBJ != null && carreraOBJ.URLRegistro != null && carreraOBJ.URLRegistro != string.Empty)
                {
                    Response.Redirect(carreraOBJ.URLRegistro);
                }
                else
                {
                    Response.Redirect(WebSettings.DefaultRegistrationURL + objSessionPayPal.IdCarrera);
                }
            }


        }
    }
}