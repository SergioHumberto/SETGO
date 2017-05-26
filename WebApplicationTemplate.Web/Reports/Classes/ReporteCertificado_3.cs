using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace WebApplicationTemplate.Web.Reports.Classes
{
    public class ReporteCertificado_3 : Tools.ReportControl
    {
        public int IdCarrera { get; set; }
        public int IdResultado { get; set; }

        public override string ReportPath()
        {
            return "~/Reports/ReporteCertificado_3.rdlc";
        }

        public override DataTable GenerateDataSource()
        {
            SqlParameter[] arrParams = new SqlParameter[2];
            arrParams[0] = new SqlParameter("IdCarrera", IdCarrera);
            arrParams[1] = new SqlParameter("IdResultado", IdResultado);

            DataTable dt = Tools.DataSetHelper.ExecuteStoredProcedure("ReporteCertificado1_PA", arrParams);
            dt.TableName = "DataSet1";

            return dt;
        }

        public override List<ReportParameter> GenerateReportParameters()
        {
            List<ReportParameter> lstParameters = new List<ReportParameter>();
            lstParameters.Add(new ReportParameter("ImagePath", Tools.Urls.Abs("~/Reports/Images/imgCertificado3.jpg")));

            return lstParameters;
        }

        public override string ReportName()
        {
            return "CertificadoApostolica";
        }

        public override EnumDisposition Disposition()
        {
            return EnumDisposition.inline;
        }
    }
}