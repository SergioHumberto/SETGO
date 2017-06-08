using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;


namespace WebApplicationTemplate.Web.Reports.Classes
{
    public class ReporteCertificado : Tools.ReportControl
    {
        public ReporteCertificado() : this(FormatoCertificado.Formato1) { }

        public ReporteCertificado(FormatoCertificado formato) : this(formato, "ReporteCertificado1_PA", "~/Reports/Images/imgCertificado4.jpg", "DataSet1") { }

        public ReporteCertificado(FormatoCertificado formato, string imagenFondo) : this(formato, "ReporteCertificado1_PA", imagenFondo, "DataSet1") { }

        public ReporteCertificado(FormatoCertificado formato, string storedProcedure, string imagenFondo, string tableName)
        {
            Formato = formato;
            StoredProcedure = storedProcedure;
            ImagenFondo = imagenFondo;
            TableName = tableName;
        }

        public int IdCarrera { get; set; }
        public int IdResultado { get; set; }
        public string ImagenFondo { get; set; }
        public FormatoCertificado Formato { get; set; }
        public string StoredProcedure { get; set; }
        public string TableName { get; set; }
        /******************************************************************************************
         * 
         * Agregar los nuevos formatos añadiendo el valor en la Enumeración y en el Dictionario
         * 
         ******************************************************************************************/
        public enum FormatoCertificado
        {
            Formato1 = 1,
            Formato2 = 2,
            Formato3 = 3,
            Formato4 = 4
        }

        Dictionary<FormatoCertificado, string> RutasRdlc = new Dictionary<FormatoCertificado, string>
        {            
            {FormatoCertificado.Formato1, "~/Reports/ReporteCertificado_1.rdlc"},
            {FormatoCertificado.Formato2, "~/Reports/ReporteCertificado_2.rdlc"},
            {FormatoCertificado.Formato3, "~/Reports/ReporteCertificado_3.rdlc"},
            {FormatoCertificado.Formato4, "~/Reports/ReporteCertificado_4.rdlc"},
        };
        /**************************************************************************************/
         

        public override string ReportPath()
        {
            string reportPath;
            RutasRdlc.TryGetValue(Formato, out reportPath);            
            return reportPath;
        }

        public override DataTable GenerateDataSource()
        {
            SqlParameter[] arrParams = new SqlParameter[2];
            arrParams[0] = new SqlParameter("IdCarrera", IdCarrera);
            arrParams[1] = new SqlParameter("IdResultado", IdResultado);

            DataTable dt = Tools.DataSetHelper.ExecuteStoredProcedure(StoredProcedure, arrParams);
            dt.TableName = TableName;

            return dt;
        }

        public override List<ReportParameter> GenerateReportParameters()
        {
            List<ReportParameter> lstParameters = new List<ReportParameter>();
            lstParameters.Add(new ReportParameter("ImagePath", Tools.Urls.Abs(ImagenFondo)));

            return lstParameters;
        }

        public override string ReportName()
        {
            return "Certificado";
        }

        public override EnumDisposition Disposition()
        {
            return EnumDisposition.inline;
        }
    }
}