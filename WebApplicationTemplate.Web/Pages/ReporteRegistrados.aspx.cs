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
                this.ReportViewer2.ProcessingMode = ProcessingMode.Local;
                this.ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Reports/ReporteRegistrados.rdlc");
                this.ReportViewer2.LocalReport.EnableExternalImages = false;

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
                GeneraReporte(IdCarrera);
                ExportToExcel();
            }
        }

        private void ExportToExcel()
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = ReportViewer2.LocalReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + GetReportName() + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        private string GetReportName()
        {
            return "ReporteRegistrados_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }

        private void GeneraReporte(int IdCarrera)
        {
            SqlParameter[] arrParams = new SqlParameter[1];
            arrParams[0] = new SqlParameter("IdCarrera", IdCarrera);

            DataTable dt = Tools.DataSetHelper.ExecuteStoredProcedure("ReporteRegistrados_PA", arrParams);
            dt.TableName = "DataSet1";

            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.EnableExternalImages = false;
            ReportDataSource rds = new ReportDataSource("RegistradosXCarrera", dt);
            ReportViewer2.LocalReport.DataSources.Add(rds);
            ReportViewer2.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Reports/ReporteRegistrados.rdlc");
            ReportViewer2.LocalReport.ReportEmbeddedResource = HttpContext.Current.Server.MapPath("~/Reports/ReporteRegistrados.rdlc");
            ReportViewer2.LocalReport.SetParameters(GetListReportParameter());
            ReportViewer2.LocalReport.Refresh();
        }

        private List<ReportParameter> GetListReportParameter()
        {
            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
            lstReportParameter.Add(new ReportParameter("NombreCarrera", ddlCarrera.SelectedItem.Text));

            string strPrefixGeneric = "Generic";
            ControlXCarreraBLL objControlXCarrera = new ControlXCarreraBLL(Tools.HttpSecurity.CurrentSession);

            int IdCarrera;
            int.TryParse(ddlCarrera.SelectedValue, out IdCarrera);

            for (int i = 1; i <= 10; i++)
            {
                string nameControl = "ph" + strPrefixGeneric + i.ToString("00");

                IList<ControlXCarreraOBJ> lstControls = objControlXCarrera.SelectControlXCarrera(new ControlXCarreraOBJ() { IdControlASP = nameControl, IdCarrera = IdCarrera });
                if (lstControls.Count > 0)
                {
                    lstReportParameter.Add(new ReportParameter("Generic" + i.ToString("00"), lstControls[0].Etiqueta));
                }
                else
                {
                    lstReportParameter.Add(new ReportParameter("Generic" + i.ToString("00"), "Generic" + i.ToString("00")));
                }
            }

            return lstReportParameter;
        }
    }
}