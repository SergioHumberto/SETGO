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
using PayPal.PayPalAPIInterfaceService.Model;
using PayPal.PayPalAPIInterfaceService;
using System.Configuration;

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
                    // ProcessURL();
                    ProcessPaymentURL();
                }
                else if (Session["objSessionPayPal"] != null)
                {
                    // TestMethod();
                    CallWSExpressCheckOut();
                }
            }
        }


        private void ProcessPaymentURL()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");

            // example url: token=EC-58N52609DG781434X&PayerID=WF63RTAYUPJEN
            string strToken = Request.QueryString["token"].ToString();
            string strPayerID = Request.QueryString["PayerID"].ToString();
            Dictionary<string, string> configurationMap = GetApiCredentialsClassic();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            GetExpressCheckoutDetailsReq getECWrapper = new GetExpressCheckoutDetailsReq();
            // (Required) A timestamped token, the value of which was returned by SetExpressCheckout response.
            // Character length and limitations: 20 single-byte characters
            getECWrapper.GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType(strToken);
            // # API call 
            // Invoke the GetExpressCheckoutDetails method in service wrapper object
            GetExpressCheckoutDetailsResponseType getECResponse = service.GetExpressCheckoutDetails(getECWrapper);

            // Create request object
            DoExpressCheckoutPaymentRequestType request = new DoExpressCheckoutPaymentRequestType();
            DoExpressCheckoutPaymentRequestDetailsType requestDetails = new DoExpressCheckoutPaymentRequestDetailsType();
            request.DoExpressCheckoutPaymentRequestDetails = requestDetails;

            requestDetails.PaymentDetails = getECResponse.GetExpressCheckoutDetailsResponseDetails.PaymentDetails;
            // (Required) The timestamped token value that was returned in the SetExpressCheckout response and passed in the GetExpressCheckoutDetails request.
            requestDetails.Token = strToken;
            // (Required) Unique PayPal buyer account identification number as returned in the GetExpressCheckoutDetails response
            requestDetails.PayerID = strPayerID;
            // (Required) How you want to obtain payment. It is one of the following values:
            // * Authorization – This payment is a basic authorization subject to settlement with PayPal Authorization and Capture.
            // * Order – This payment is an order authorization subject to settlement with PayPal Authorization and Capture.
            // * Sale – This is a final sale for which you are requesting payment.
            // Note: You cannot set this value to Sale in the SetExpressCheckout request and then change this value to Authorization in the DoExpressCheckoutPayment request.
            requestDetails.PaymentAction = PaymentActionCodeType.SALE;

            // Invoke the API
            DoExpressCheckoutPaymentReq wrapper = new DoExpressCheckoutPaymentReq();
            wrapper.DoExpressCheckoutPaymentRequest = request;
            // # API call 
            // Invoke the DoExpressCheckoutPayment method in service wrapper object
            DoExpressCheckoutPaymentResponseType doECResponse = service.DoExpressCheckoutPayment(wrapper);

            if (doECResponse.Ack.Equals(AckCodeType.FAILURE) ||
                (doECResponse.Errors != null && doECResponse.Errors.Count > 0))
            {
                foreach (ErrorType errorType in doECResponse.Errors)
                {
                    litMessage.Text += errorType.LongMessage + " ";
                }
            }
            else
            {
                int IdParticipante;
                int IdCarrera;
                int IdEquipo;

                int.TryParse(Request.QueryString["IdParticipante"], out IdParticipante);
                int.TryParse(Request.QueryString["IdCarrera"], out IdCarrera);
                int.TryParse(Request.QueryString["IdEquipo"], out IdEquipo);

                string strStatus = doECResponse.Ack.Value.ToString();
                status = strStatus;

                LoadParticipanteDetails(IdParticipante, IdCarrera, IdEquipo, strToken, requestDetails.PaymentDetails, strStatus);

                if (IdEquipo > 0)
                {
                    SendInvitacionesAlEquipo(IdEquipo, IdParticipante, IdCarrera);
                }
            }
        }


        private void SendInvitacionesAlEquipo(int IdEquipo, int IdParticipante, int IdCarrera)
        {
            List<string> lstEmailsXEquipo = GetListEmailsXEquipo(IdEquipo);
            if (lstEmailsXEquipo.Count > 0)
            {
                Email emailClass = new Email();

                UserSession session = Tools.HttpSecurity.CurrentSession;
                ParticipantesBLL objParticipantesBLL = new ParticipantesBLL(session);
                ParticipantesOBJ objParticipante = objParticipantesBLL.SelectParticipanteObject(IdParticipante);

                if (objParticipante == null)
                {
                    return;
                }

                // string modalidad = GetModalidad(IdParticipante);

                EquipoBLL objEquipoBLL = new EquipoBLL(session);
                EquipoOBJ objEquipo = objEquipoBLL.SelectEquipoObject(IdEquipo);

                foreach (string itemEmail in lstEmailsXEquipo)
                {
                    if (itemEmail.CompareTo(objParticipante.Email) != 0) // Cualquier email, menos el que está registrando.
                    {
                        string body = @"
						<table>
                            <tr><td>El participante {0} te ha invitado a formar parte de su equipo</td></tr>
                            <tr><td>Haz click <a target='_blank' href='{1}'>aqui</a> para completar tu registro.</td></tr>
                        </table>
						";

                        string strURLRegistrarEquipo = ConfigurationManager.AppSettings["URLRedirectRegistrarEquipo"];
                        strURLRegistrarEquipo = string.Format(strURLRegistrarEquipo, IdCarrera, objEquipo.Guid, itemEmail);

                        body = string.Format(body
                            , objParticipante.Nombre + " " +
                                objParticipante.ApellidoPaterno + " " +
                                objParticipante.ApellidoMaterno             //Nombre
                           , strURLRegistrarEquipo                         // Url to return to register team
                        );

                        emailClass.SendEmail(body, itemEmail, "Te han enviado una invitación a participar en equipo");

                        

                    }
                }
            }
        }

        private string GetModalidad(int IdParticipante)
        {
            string modalidad = string.Empty;

            RamaBLL ramaBLL = new RamaBLL(HttpSecurity.CurrentSession);
            RamaOBJ ramaOBJ = new RamaOBJ();

            CategoriaBLL categoriaBLL = new CategoriaBLL(HttpSecurity.CurrentSession);
            CategoriaOBJ categoria = categoriaBLL.SelectCategoriaByIdParticipante(IdParticipante);

            RutaBLL rutaBLL = new RutaBLL(HttpSecurity.CurrentSession);
            RutaOBJ ruta = rutaBLL.SelectRutaByIdParticipante(IdParticipante);

            ramaOBJ = ramaBLL.SelectRamaByIdParticipante(IdParticipante);
            modalidad = (ramaOBJ != null) ? ramaOBJ.Nombre : string.Empty;
            modalidad += ((modalidad == string.Empty || categoria == null) ? string.Empty : ", ") + ((categoria != null) ? categoria.Nombre : string.Empty);
            modalidad += ((modalidad == string.Empty || ruta == null) ? string.Empty : ", ") + ((ruta != null) ? ruta.Nombre : string.Empty);

            return modalidad;
        }

        private List<string> GetListEmailsXEquipo(int IdEquipo)
        {
            UserSession session = Tools.HttpSecurity.CurrentSession;
            EquipoBLL objEquipoBLL = new EquipoBLL(session);
            EquipoOBJ objEquipo = objEquipoBLL.SelectEquipoObject(IdEquipo);

            List<string> lstEmailsXEquipo = new List<string>();

            if (objEquipo != null)
            {
                char[] arrSeparator = { ';' };
                string[] arrEmails = objEquipo.EmailsParticipantes.Split(arrSeparator);

                lstEmailsXEquipo.AddRange(arrEmails);
            }

            return lstEmailsXEquipo;
        }

        private void LoadParticipanteDetails(int IdParticipante, int IdCarrera, int IdEquipo, string strToken, List<PaymentDetailsType> lstPaymentDetails, string strStatus)
        {
            if (IdParticipante > 0 && IdCarrera > 0)
            {
                decimal precio = 0;
                if (lstPaymentDetails.Count > 0)
                {
                    decimal.TryParse(lstPaymentDetails[0].ItemTotal.value, out precio);
                }

                ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(HttpSecurity.CurrentSession);
                ParticipantesOBJ objParticipante = objParticipanteBLL.SelectParticipanteObject(IdParticipante);

                CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
                CarreraOBJ carreraOBJ = new CarreraOBJ();

                carreraOBJ = carreraBLL.SelectCarreraObject(IdCarrera);

                if (objParticipante != null)
                {
                    objParticipante.StatusPaypal = strStatus;
                    objParticipante.TransactionNumber = strToken;

					//Actualiza en la tabla de participante, la fecha de pago de paypal
					objParticipante.FechaPago = DateTime.Now;

					objParticipanteBLL.UpdateParticipante(objParticipante);
                }

                //Cadena para enviar el correo.
                string body = @"
						<table>
                            <tr><td style='font-weight:bold; background:#9FD0FF;' width = '50%'>Folio de Registro:</td><td style='background:#9FD0FF;' width='50%'>{0}</td></tr>
							<tr><td style='font-weight:bold;'>Modalidad:</td><td>{1}</td></tr>
							<tr><td style='font-weight:bold;background:#9FD0FF;'>Nombre:</td><td style='background:#9FD0FF;'>{2}</td></tr>
							<tr><td style='font-weight:bold;'>Fecha de Nacimiento:</td><td>{3}</td></tr>
							<tr><td style='font-weight:bold;background:#9FD0FF;'>Email:</td><td style='background:#9FD0FF;'>{4}</td></tr>
							<tr><td style='font-weight:bold;'>Teléfono Personal:</td><td>{5}</td></tr>
							<tr><td style='font-weight:bold;background:#9FD0FF;'>Teléfono de Contacto de Emergencia:</td><td style='background:#9FD0FF;'>{6}</ td></tr>
							<tr><td style='font-weight:bold;'>Dirección:</td><td>{7}</td></tr>
							<tr><td style='background:#9FD0FF;'>" + carreraOBJ.DescripcionPoliticas + @"</td><td style='background:#9FD0FF;'>{8}</td></tr>
							<tr><td style='font-weight:bold;'>Total:</td><td>{9}</td></tr>
							<tr><td style='font-weight:bold;background:#9FD0FF;'>Status:</td><td style='background:#9FD0FF;'>{10}</td></tr>
							<tr><td style='font-weight:bold;'>Payment ID:</td><td>{11}</td></tr>
							<tr><td style='font-weight:bold; background:#9FD0FF;'>Payment Date:</td><td style='background:#9FD0FF;'>{12}</td></tr>
                            <tr><td style='font-weight:bold;'>Tipo de registro:</td><td>{13}</td></tr>
						</table>
						";

                RamaBLL ramaBLL = new RamaBLL(HttpSecurity.CurrentSession);
                RamaOBJ ramaOBJ = new RamaOBJ();

                ramaOBJ = ramaBLL.SelectRamaByIdParticipante(objParticipante.IdParticipante);

                CategoriaBLL categoriaBLL = new CategoriaBLL(HttpSecurity.CurrentSession);
                CategoriaOBJ categoria = categoriaBLL.SelectCategoriaByIdParticipante(objParticipante.IdParticipante);

                RutaBLL rutaBLL = new RutaBLL(HttpSecurity.CurrentSession);
                RutaOBJ ruta = rutaBLL.SelectRutaByIdParticipante(objParticipante.IdParticipante);

                string modalidad = string.Empty;

                modalidad = (ramaOBJ != null) ? ramaOBJ.Nombre : string.Empty;
                modalidad += ((modalidad == string.Empty || categoria == null) ? string.Empty : ", " ) + ((categoria != null) ? categoria.Nombre : string.Empty);
                modalidad += ((modalidad == string.Empty || ruta == null) ? string.Empty : ", ") + ((ruta != null) ? ruta.Nombre : string.Empty);

                string strTipoRegistro = "Individual";

                if (IdEquipo > 0)
                {
                    strTipoRegistro = "Equipo";
                }

                body = string.Format(body
                    , objParticipante.Folio                         //Folio
                    , modalidad                                //Modalidad
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
                    , strTipoRegistro                               // tipo de registro
                    );

                tablaNotificacion.InnerHtml = body;

                Email email = new Email();
                email.SendEmail(body, objParticipante.Email, carreraOBJ.CC, carreraOBJ.BCC, objParticipante.Folio);
            }

            Session.Remove("paymentId");
        }

        // este metodo se usa para rest api
        [Obsolete]
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
							<tr><td style=" + "background:#9FD0FF;" + @"width=" + "50%" + @">Selecciona una modalidad:</td><td width=" + "50%" + @">{0}</td></tr>
							<tr><td style=" + "background:#9FD0FF;" + @">Nombe:</td><td style=" + "background:#9FD0FF;" + @">{1}</td></tr>
							<tr><td>Fecha de Nacimiento:</td><td>{2}</td></tr>
							<tr><td style=" + "background:#9FD0FF;" + @">Email:</td><td style=" + "background:#9FD0FF;" + @">{3}</td></tr>
							<tr><td>Teléfono Personal:</td><td>{4}</td></tr>
							<tr><td style=" + "background:#9FD0FF;" + @">Teléfono de Contacto de Emergencia:</td><td style=" + "background:#9FD0FF;" + @">{5}</ td></tr>
							<tr><td>Dirección:</td><td>{6}</td></tr>
							<tr><td style=" + "background:#9FD0FF;" + @">" + carreraOBJ.DescripcionPoliticas + @"</td><td style=" + "background:#9FD0FF;" + @">{7}</td></tr>
							<tr><td>Total:</td><td>{8}</td></tr>
							<tr><td style=" + "background:#9FD0FF;" + @">Status:</td><td style=" + "background:#9FD0FF;" + @">{9}</td></tr>
							<tr><td>Payment ID:</td><td>{10}</td></tr>
							<tr><td style=" + "background:#9FD0FF;" + @">Payment Date:</td><td style=" + "background:#9FD0FF;" + @">{11}</td></tr>
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
                    email.SendEmail(body, objParticipante.Email, carreraOBJ.CC, carreraOBJ.BCC, objParticipante.Folio);
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

        // Este metodo es de la manera de llamar a Rest API ( no disponible en nuestro pais MX)
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

        private void CallWSExpressCheckOut()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");

            // Create request object
            SetExpressCheckoutRequestType request = new SetExpressCheckoutRequestType();
            populateRequestObject(request);

            // Invoke the API
            SetExpressCheckoutReq wrapper = new SetExpressCheckoutReq();
            wrapper.SetExpressCheckoutRequest = request;

            Dictionary<string, string> configurationMap = GetApiCredentialsClassic();

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(configurationMap);

            // # API call 
            // Invoke the SetExpressCheckout method in service wrapper object  
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);

            // Check for API return status
            // HttpContext CurrContext = HttpContext.Current;
            // CurrContext.Items.Add("paymentDetails", request.SetExpressCheckoutRequestDetails.PaymentDetails);
            // setKeyResponseObjects(service, setECResponse);

            if (setECResponse.Ack.Equals(AckCodeType.FAILURE) ||
               (setECResponse.Errors != null && setECResponse.Errors.Count > 0))
            {
                foreach (ErrorType errorType in setECResponse.Errors)
                {
                    litMessage.Text += errorType.LongMessage + " ";
                }
            }
            else
            {
                Response.Redirect(WebSettings.PAYPAL_REDIRECT_URL + "_express-checkout&token=" + setECResponse.Token, false);
                Context.ApplicationInstance.CompleteRequest();

                // string strLastResponse = service.getLastResponse();
            }
        }

        private Dictionary<string, string> GetApiCredentialsClassic()
        {
            // Configuration map containing signature credentials and other required configuration.
            // For a full list of configuration parameters refer in wiki page 
            // [https://github.com/paypal/sdk-core-dotnet/wiki/SDK-Configuration-Parameters]
            Dictionary<string, string> configurationMap = new Dictionary<string, string>(); // Configuration.GetAcctAndConfig();

            configurationMap.Add("mode", WebSettings.ModePayPalClassic); // sandbox or live
            configurationMap.Add("account1.apiUsername", WebSettings.ApiUsername); // ej. "contacto.SEI.tech-facilitator_api1.gmail.com"
            configurationMap.Add("account1.apiPassword", WebSettings.ApiPassword); // ej. "PRR25LQPRV256SR6"
            configurationMap.Add("account1.apiSignature", WebSettings.ApiSignature); // ej. "AFcWxV21C7fd0v3bYYYRCpSSRl31A8LpSbTMqDrbFwEIq6oR12I.bqi2"
            return configurationMap;
        }

        private void populateRequestObject(SetExpressCheckoutRequestType request)
        {
            SessionPayPal objSessionPayPal = (SessionPayPal)Session["objSessionPayPal"];

            SetExpressCheckoutRequestDetailsType ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = objSessionPayPal.returnURL;
            ecDetails.CancelURL = objSessionPayPal.cancelURL;
            // ecDetails.BuyerEmail = GetBuyerEmail(objSessionPayPal.IdCarrera);  // buyerEmail.Value;

            /* Populate payment requestDetails. 
             * SetExpressCheckout allows parallel payments of upto 10 payments. 
             * This samples shows just one payment.
             */
            PaymentDetailsType paymentDetails = new PaymentDetailsType();

            paymentDetails.SellerDetails = new SellerDetailsType();
            paymentDetails.SellerDetails.PayPalAccountID = GetBuyerEmail(objSessionPayPal.IdCarrera);

            ecDetails.PaymentDetails.Add(paymentDetails);
           
            CurrencyCodeType currency = CurrencyCodeType.MXN; // moneda nacional mexicana

            paymentDetails.PaymentAction = PaymentActionCodeType.SALE;

            PaymentDetailsItemType itemDetails = new PaymentDetailsItemType();
            itemDetails.Name = objSessionPayPal.item_name;
            itemDetails.Amount = new BasicAmountType(currency, objSessionPayPal.amount.ToString());
            itemDetails.Quantity = 1; // Convert.ToInt32(itemQuantity.Value);
            itemDetails.ItemCategory = ItemCategoryType.PHYSICAL;

            paymentDetails.PaymentDetailsItem.Add(itemDetails);

            paymentDetails.ItemTotal = new BasicAmountType(currency, objSessionPayPal.amount.ToString());
            paymentDetails.OrderTotal = new BasicAmountType(currency, objSessionPayPal.amount.ToString());

            request.SetExpressCheckoutRequestDetails = ecDetails;
        }

        private string GetBuyerEmail(int IdCarrera)
        {
            CarreraBLL objCarreraBLL = new CarreraBLL(Tools.HttpSecurity.CurrentSession);
            CarreraOBJ objCarreraOBJ = objCarreraBLL.SelectCarreraObject(IdCarrera);
            if (objCarreraOBJ != null)
            {
                return objCarreraOBJ.PayPalEmail;
            }

            return string.Empty;
        }

    }
}