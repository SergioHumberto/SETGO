using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;
using WebApplicationTemplate.Web.Tools;

namespace WebApplicationTemplate.Web.Reports.Classes
{
    public class ReporteRegistrados : Tools.ReportControl
    {
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }

        public override DataTable GenerateDataSource()
        {
            SqlParameter[] arrParams = new SqlParameter[1];
            arrParams[0] = new SqlParameter("IdCarrera", IdCarrera);

            DataTable dt = Tools.DataSetHelper.ExecuteStoredProcedure("ReporteRegistrados_PA", arrParams);
            dt.TableName = "DataSet1";
            return dt;
        }

        public override string DataSourceName()
        {
            return "RegistradosXCarrera";
        }

        public override string ReportPath()
        {
            return "~/Reports/ReporteRegistrados.rdlc";
        }

        public override string ReportName()
        {
            return "ReporteRegistrados";
        }

        public override EnumFormatDocument FormatDocument()
        {
            return EnumFormatDocument.EXCEL;
        }

        public override List<ReportParameter> GenerateReportParameters()
        {
            List<ReportParameter> lstReportParameter = new List<ReportParameter>();
            lstReportParameter.Add(new ReportParameter("NombreCarrera", NombreCarrera));

            string strPrefixGeneric = "Generic";
            ControlXCarreraBLL objControlXCarrera = new ControlXCarreraBLL(Tools.HttpSecurity.CurrentSession);

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