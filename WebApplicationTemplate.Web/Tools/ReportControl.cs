using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace WebApplicationTemplate.Web.Tools
{
    public class ReportControl
    {
        public enum EnumDisposition
        {
            attachment = 0
            , inline
        }

        public enum EnumFormatDocument
        {
            PDF = 0
            , EXCEL
        }

        public virtual LocalReport GenerateLocalReport()
        {
            LocalReport localReport = new LocalReport();

            localReport.DataSources.Clear();
            localReport.EnableExternalImages = true;

            ReportDataSource rds = new ReportDataSource(DataSourceName(), GenerateDataSource());
            localReport.DataSources.Add(rds);
            localReport.ReportPath = HttpContext.Current.Server.MapPath(ReportPath());
            // localReport.ReportEmbeddedResource = HttpContext.Current.Server.MapPath(ReportPath());
            IList<ReportParameter> lstReportParameters = GenerateReportParameters();
            if (lstReportParameters != null)
            {
                localReport.SetParameters(lstReportParameters);
            }

            ReportPageSettings rpsettings = localReport.GetDefaultPageSettings();
            localReport.Refresh();

            return localReport;
        }

        public virtual string DataSourceName()
        {
            return "DataSet1";
        }

        public virtual string ReportPath()
        {
            // ex. "~/Reports/Certificado_1.rdlc"
            return string.Empty;
        }

        public virtual List<ReportParameter> GenerateReportParameters()
        {
            return null;
        }

        public virtual DataTable GenerateDataSource()
        {
            return null;
        }

        public virtual EnumFormatDocument FormatDocument()
        {
            return EnumFormatDocument.PDF;
        }

        public virtual string ReportName()
        {
            return "reporte";
        }

        public virtual string FormatStringDate()
        {
            return DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }
        
        public void SentReport(LocalReport localReport)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = localReport.Render(FormatDocument().ToString(), null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = mimeType;
            HttpContext.Current.Response.AddHeader("content-disposition", Disposition().ToString() + "; filename=" + ReportName() + "_" + FormatStringDate() + "." + extension);
            HttpContext.Current.Response.BinaryWrite(bytes); // create the file
            HttpContext.Current.Response.Flush(); // send it to the client to download
        }

        public void GenerateReport()
        {
            LocalReport localReport = GenerateLocalReport();
            SentReport(localReport);
        }

        public virtual EnumDisposition Disposition()
        {
            return EnumDisposition.attachment;
        }
    }
}