using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web.PublicPages
{
    public partial class GenerarCertificados : System.Web.UI.Page
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

        protected void btnGenerarCertificado1_Click(object sender, EventArgs e)
        {
            Reports.Classes.ReporteCertificado_1 reportCertificado = new Reports.Classes.ReporteCertificado_1();

            int IdCarrera;
            int.TryParse(ddlCarrera.SelectedValue, out IdCarrera);

            reportCertificado.IdCarrera = IdCarrera;
            reportCertificado.GenerateReport();
        }

        protected void btnGenerarCertificado2_Click(object sender, EventArgs e)
        {
            Reports.Classes.ReporteCertificado_2 reporte = new Reports.Classes.ReporteCertificado_2();
            int IdCarrera;
            int.TryParse(ddlCarrera.SelectedValue, out IdCarrera);

            reporte.IdCarrera = IdCarrera;
            reporte.GenerateReport();
        }
    }
}