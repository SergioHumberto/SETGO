using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace WebApplicationTemplate.Web.Tools
{
	public static class Urls
	{
		public static String Abs(String url)
		{
            //StringBuilder abs = new StringBuilder("http://setgosrv-001-site1.ftempurl.com");
            // StringBuilder abs = new StringBuilder(HttpContext.Current.Request.ApplicationPath);
            StringBuilder abs = new StringBuilder(WebSettings.URLApplicationPath);

            if (url.StartsWith("/"))
			{
				abs.Append(url);
			}
			else if (url.StartsWith("~"))
			{
                abs.Append(url.Substring(1));
                // return HttpContext.Current.Server.MapPath(url);
			}
			else
			{
				abs.Append('/');
				abs.Append(url);
			}

			return abs.ToString();
		}

		public static String Format(String urlFormat, params Object[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = Uri.EscapeDataString(Convert.ToString(args[i]));
			}

			return string.Format(urlFormat, args);
		}

		public static String Default()
		{
			return Abs("/");
		}

		public static String Home()
		{
			return Abs("/Pages/Home.aspx");
		}

		public static String Demo()
		{
			return Abs("/Pages/Demo.aspx");
		}

		public static String SignOut()
		{
			return Abs("/Pages/SignOut.aspx");
		}

        public static String RegistroParticipantes()
        {
            return Abs("/PublicPages/RegistroParticipantes.aspx");
        }

        public static String TestIFrame()
        {
            return Abs("/Pages/TestIFrame.aspx");
        }

        public static String ReporteRegistrados()
        {
            return Abs("/Pages/ReporteRegistrados.aspx");
        }

        public static String PayPalPage()
        {
            return Abs("~/PublicPages/PayPal.aspx");
        }

        public static String AdmonCatEquipos()
        {
            return Abs("~/Pages/AdmonCatEquipos.aspx");
        }
    }
}