using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web.Pages
{
	public partial class PayPal : System.Web.UI.Page
	{
        public string PayPalEmail { get; set; }
        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public string Custom { get; set; }
        public string ReturnURL { get; set; }
        public string CancelURL { get; set; }

        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                if (Session["objSessionPayPal"] != null)
                {
                    SessionPayPal objSessionPayPal = (SessionPayPal)Session["objSessionPayPal"];
                    CarreraBLL objCarreraBLL = new CarreraBLL(Tools.HttpSecurity.CurrentSession);
                    CarreraOBJ objCarreraOBJ = objCarreraBLL.SelectCarreraObject(objSessionPayPal.IdCarrera);
                    PayPalEmail = objCarreraOBJ.PayPalEmail;
                    ItemName = objSessionPayPal.item_name;
                    Amount = objSessionPayPal.amount;
                    Custom = objSessionPayPal.custom;
                    ReturnURL = objSessionPayPal.returnURL;
                    CancelURL = objSessionPayPal.cancelURL;

                    Session.Remove("objSessionPayPal");

                    // ClientScript.RegisterStartupScript(GetType(), "doPostBack", "__doPostBack('','')", true);
                }
            }
        }
    }
}