using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.BLL.Security;
using WebApplicationTemplate.Objects.Security;
using WebApplicationTemplate.Web.Tools;


namespace WebApplicationTemplate.Web
{
	public partial class Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			/* do nothing */
		}

		protected void btnSignIn_Click(Object sender, EventArgs e)
		{
			try
			{
				//Authenticate user session
				HttpSecurity.SignIn(txtUsername.Text, txtPassword.Text);

				string returnUrl = Request.QueryString["ReturnUrl"];

				if (string.IsNullOrEmpty(returnUrl))
				{
					HttpTool.Redirect(Urls.Home());
				}
				else
				{
					HttpTool.Redirect(returnUrl);
				}
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}
	}
}