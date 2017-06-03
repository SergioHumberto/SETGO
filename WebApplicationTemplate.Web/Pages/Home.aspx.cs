using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class Home : System.Web.UI.Page
    {
		private int? IsAdministrador
		{
			get
			{
				if (Request.QueryString["A"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["A"].ToString()))
				{
					if (Request.QueryString["A"].ToString() == "0")
						return 0;
					else if (Request.QueryString["A"].ToString() == "1")
						return 1;
				}
				return null;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
        {
			//si el usuario quiere ingresar al formulario de usuarios, muestra un mensaje diciendo que no puede ingresar.
			if (IsAdministrador != null && IsAdministrador == 0)
			{
				lblModalTitle.Text = "¡Aviso!";
				lblModalBody.Text = "No tiene acceso a esta pantalla.";
				ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
				upModal.Update();
			}
        }
    }
}