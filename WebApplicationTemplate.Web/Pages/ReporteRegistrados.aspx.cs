using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class ReporteRegistrados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCarreras();
            }
        }

        private void LoadCarreras()
        {
            CarreraBLL objCarreraBLL = new CarreraBLL(Tools.HttpSecurity.CurrentSession);
            IList<CarreraOBJ> lstCarreras = objCarreraBLL.SelectCarrera(new CarreraOBJ() { }); // Todas las carreras
            ddlCarrera.DataSource = lstCarreras;
            ddlCarrera.DataTextField = "Nombre";
            ddlCarrera.DataValueField = "IdCarrera";
            ddlCarrera.DataBind();
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            int IdCarrera;
            if (int.TryParse(ddlCarrera.SelectedValue, out IdCarrera))
            {
                Reports.Classes.ReporteRegistrados  reporteRegistrados = new Reports.Classes.ReporteRegistrados();
                reporteRegistrados.IdCarrera = IdCarrera;
                reporteRegistrados.NombreCarrera = ddlCarrera.SelectedItem.Text;
                reporteRegistrados.GenerateReport();
            }
        }
    }
}