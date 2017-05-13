using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace WebApplicationTemplate.Web.Reports.Classes
{
    public class ReporteCertificado_2 : Tools.ReportControl
    {
        public int IdCarrera { get; set; }

        public override string ReportPath()
        {
            return "~/Reports/ReporteCertificado_2.rdlc";
        }

        public override DataTable GenerateDataSource()
        {
            SqlParameter[] arrParams = new SqlParameter[1];
            arrParams[0] = new SqlParameter("IdCarrera", IdCarrera);

            DataTable dt = Tools.DataSetHelper.ExecuteStoredProcedure("ReporteCertificado1_PA", arrParams);
            dt.TableName = "DataSet1";

            return dt;
        }

        public override List<ReportParameter> GenerateReportParameters()
        {
            List<ReportParameter> lstParameters = new List<ReportParameter>();
            lstParameters.Add(new ReportParameter("ImagePath", Tools.Urls.Abs("~/Reports/Images/imgCertificado2.jpg")));

            return lstParameters;
        }

        public override string ReportName()
        {
            return "Certificado2";
        }

    }
}