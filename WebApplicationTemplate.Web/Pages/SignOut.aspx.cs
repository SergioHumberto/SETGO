using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTemplate.Web.Tools;

namespace WebApplicationTemplate.Web.Pages
{
	public partial class SignOut : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HttpSecurity.SignOut();

			HttpTool.Redirect(Urls.Home());
		}
	}
}