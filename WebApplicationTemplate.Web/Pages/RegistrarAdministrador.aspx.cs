using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplicationTemplate.BLL.Security;
using WebApplicationTemplate.Objects.Security;
using WebApplicationTemplate.Web.Tools;

using System.Security.Cryptography;
using System.Text;

namespace WebApplicationTemplate.Web.Pages
{
	public partial class RegistrarAdministrador : System.Web.UI.Page
	{
		private string UserName
		{
			get
			{
				if (Request.QueryString["username"] != null)
				{
					return Request.QueryString["username"].ToString();
				}
				return null;
			}
		}

		private string Nombre
		{
			get
			{
				if (Request.QueryString["nombre"] != null)
				{
					return Request.QueryString["nombre"].ToString();
				}
				return null;
			}
		}

		private string Paterno
		{
			get
			{
				if (Request.QueryString["paterno"] != null)
				{
					return Request.QueryString["paterno"].ToString();
				}
				return null;
			}
		}

		private string Materno
		{
			get
			{
				if (Request.QueryString["materno"] != null)
				{
					return Request.QueryString["materno"].ToString();
				}
				return null;
			}
		}

		private string Email
		{
			get
			{
				if (Request.QueryString["email"] != null)
				{
					return Request.QueryString["email"].ToString();
				}
				return null;
			}
		}

		private string Password
		{
			get
			{
				if (Request.QueryString["password"] != null)
				{
					return Request.QueryString["password"].ToString();
				}
				return null;
			}
		}

		private string CPassword
		{
			get
			{
				if (Request.QueryString["cpassword"] != null)
				{
					return Request.QueryString["cpassword"].ToString();
				}
				return null;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (VerificarInsercionDeUsuario())
				{
					InsertarUsuario();
				}
			}
		}

		/// <summary>
		/// Verifica si se va a insertar un usuario
		/// </summary>
		/// <returns></returns>
		private bool VerificarInsercionDeUsuario()
		{
			if(UserName == null
				|| Nombre == null
				|| Paterno == null
				|| Materno == null
				|| Email == null
				|| Password == null
				|| CPassword == null)
			{
				return false;
			}
			return true;
		}

		private void InsertarUsuario()
		{
			User user = new User();
			UserBLL userBLL = new UserBLL(HttpSecurity.CurrentSession);

			user = GetUser();

			if (!userBLL.ExisteUsername(user))
			{
				userBLL.InsertUser(user);
			}
			else
			{
				ClientScriptManager cs = Page.ClientScript;
				cs.RegisterStartupScript(GetType(), "Username", "alert('Ya existe el nombre de usuario');", true);
			}
		}

		private User GetUser()
		{
			User user = new User();

			user.Username = UserName;
			user.Nombre = Nombre;
			user.ApellidoPaterno = Paterno;
			user.ApellidoMaterno = Materno;
			user.Email = Email;
			user.Password = MD5(Password);
			user.IsSuperUser = true;
			user.SessionToken = null;

			return user;
		}

		private string MD5(string word)
		{
			MD5 md5 = MD5CryptoServiceProvider.Create();
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] stream = null;
			StringBuilder sb = new StringBuilder();
			stream = md5.ComputeHash(encoding.GetBytes(word));
			for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
			return sb.ToString();
		}

	}
}