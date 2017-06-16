using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using WebApplicationTemplate.BLL.Security;
using WebApplicationTemplate.Objects.Security;
using WebApplicationTemplate.BLL;

namespace WebApplicationTemplate.Web.Tools
{
	public static class Urls
	{
		public static String Abs(String url)
		{
            //StringBuilder abs = new StringBuilder("http://setgosrv-001-site1.ftempurl.com");
            //StringBuilder abs = new StringBuilder(HttpContext.Current.Request.ApplicationPath);
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
            return Abs("~/PublicPages/PayPalRestAPI.aspx");
        }       

		public static String CargarResultados()
		{
			return Abs("~/Pages/CargarResultados.aspx");
		}
        public static String BuscaCarrera()
        {
            return Abs("~/Pages/BuscaCarrera.aspx");
        }
        public static String ConfiguraCarrera()
        {
            return Abs("~/Pages/ConfiguraCarrera.aspx");
        }
        public static String ConsultaResultados()
        {
            return Abs("~/PublicPages/ConsultaResultados.aspx");
        }

        public static String ConsultaResultadosIFrame()
        {
            return Abs("~/Pages/ConsultaResultadosIFrame.aspx");
        }

		public static String Usuarios()
		{
			UserSession currentSession = HttpSecurity.CurrentSession;
			UserBLL userBLL = new UserBLL(currentSession);
			User currentUser = new User();

			//Obtiene el usuario que está logueado.
			currentUser = userBLL.SeleccionarUsuarioByIdUser(currentSession.IdUser);

			//Si el usuario es administrador
			if (currentUser.IsSuperUser)
			{
				//se redirecciona al formulario de Usuarios.
				return Abs("~/Pages/Usuarios.aspx");
			}
			else//Si no es administrador
			{
				return Home() + "?A=0";
			}
		}
	}
}