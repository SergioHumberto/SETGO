﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;
using WebApplicationTemplate.Web.Tools;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class PayPalRestAPI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PayerID"] != null)
                {
                    ProcessURL();
                }
            }
        }

        private void ProcessURL()
        {
            // exmple url parameters: paymentId=PAY-82T48127285547200LD5W3LQ&token=EC-7WB48951HL5170437&PayerID=WF63RTAYUPJEN
            if (Session["paymentId"] != null)
            {
                var paymentId = Session["paymentId"].ToString();
                var payment = new Payment() { id = paymentId };

                var payerId = Request.QueryString["PayerID"];
                var paymentExecution = new PaymentExecution() { payer_id = payerId  };

                var apiContext = GetAPIContext();

                var executedPayment = payment.Execute(apiContext, paymentExecution);

                litMessage.Text = "<p> you order has been completed </p>";

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

            var apiContext = GetAPIContext();

            decimal postagePackingCost = 3.95m;
            decimal examPaperPrice = 10.00m;
            int quantityOfExamPapers = 2;
            decimal subTotal = (quantityOfExamPapers * examPaperPrice);
            decimal total = subTotal + postagePackingCost;

            var examPaperItem = new Item();
            examPaperItem.name = "Past Examen paper";
            examPaperItem.currency = "GBP";
            examPaperItem.price = examPaperPrice.ToString();
            examPaperItem.sku = "PEPCO5027m15"; // code manufacture
            examPaperItem.quantity = quantityOfExamPapers.ToString();

            var transactionDetails = new Details();
            transactionDetails.tax = "0";
            transactionDetails.shipping = postagePackingCost.ToString();
            transactionDetails.subtotal = subTotal.ToString("0.00");

            var transactionAmount = new Amount();
            transactionAmount.currency = "GBP";
            transactionAmount.total = total.ToString("0.00");
            transactionAmount.details = transactionDetails;

            var payee = new Payee();
            payee.email = "humberto1_sergio-facilitator@hotmail.com"; // Este correo debe de venir de la base de datos.

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

        protected void btnTestPayPayRest_Click(object sender, EventArgs e)
        {
            TestMethod();
        }
    }
}