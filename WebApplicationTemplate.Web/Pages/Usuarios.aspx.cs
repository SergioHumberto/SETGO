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
using WebApplicationTemplate.DAL.Security;
using WebApplicationTemplate.BLL;
using System.Web.Services;

namespace WebApplicationTemplate.Web.Pages
{
	public partial class Usuarios : System.Web.UI.Page
	{
		#region Variables
		private string UserName
		{
			get
			{
				//if (Request.Form["username"] != null && !string.IsNullOrWhiteSpace(Request.Form["username"].ToString()))
				//{
				//	return Request.Form["username"].ToString();
				//}
				//return null;
				if (username != null && !string.IsNullOrWhiteSpace(username.Value.ToString()))
				{
					return username.Value.ToString();
				}
				return null;
			}
		}

		private string Nombre
		{
			get
			{
				//if (Request.Form["nombre"] != null && !string.IsNullOrWhiteSpace(Request.Form["nombre"].ToString()))
				//{
				//	return Request.Form["nombre"].ToString();
				//}
				//return null;
				if (nombre != null && !string.IsNullOrWhiteSpace(nombre.Value.ToString()))
				{
					return nombre.Value.ToString();
				}
				return null;
			}
		}

		private string Paterno
		{
			get
			{
				//if (Request.Form["paterno"] != null && !string.IsNullOrWhiteSpace(Request.Form["paterno"].ToString()))
				//{
				//	return Request.Form["paterno"].ToString();
				//}
				//return null;
				if (paterno != null && !string.IsNullOrWhiteSpace(paterno.Value.ToString()))
				{
					return paterno.Value.ToString();
				}
				return null;
			}
		}

		private string Materno
		{
			get
			{
				//if (Request.Form["materno"] != null && !string.IsNullOrWhiteSpace(Request.Form["materno"].ToString()))
				//{
				//	return Request.Form["materno"].ToString();
				//}
				//return null;
				if (materno != null && !string.IsNullOrWhiteSpace(materno.Value.ToString()))
				{
					return materno.Value.ToString();
				}
				return null;
			}
		}

		private string Email
		{
			get
			{
				//if (Request.Form["email"] != null && !string.IsNullOrWhiteSpace(Request.Form["email"].ToString()))
				//{
				//	return Request.Form["email"].ToString();
				//}
				//return null;
				if (email != null && !string.IsNullOrWhiteSpace(email.Value.ToString()))
				{
					return email.Value.ToString();
				}
				return null;
			}
		}

		private string Password
		{
			get
			{
				//if (Request.Form["password"] != null && !string.IsNullOrWhiteSpace(Request.Form["password"].ToString()))
				//{
				//	return Request.Form["password"].ToString();
				//}
				//return null;
				if (password != null && !string.IsNullOrWhiteSpace(password.Value.ToString()))
				{
					return password.Value.ToString();
				}
				else if(passwordEdit != null && !string.IsNullOrWhiteSpace(passwordEdit.Value.ToString()))
				{
					return passwordEdit.Value.ToString();
				}
				return null;
			}
		}

		private string CPassword
		{
			get
			{
				//if (Request.Form["cpassword"] != null && !string.IsNullOrWhiteSpace(Request.Form["cpassword"].ToString()))
				//{
				//	return Request.Form["cpassword"].ToString();
				//}
				//return null;
				if (cpassword != null && !string.IsNullOrWhiteSpace(cpassword.Value.ToString()))
				{
					return cpassword.Value.ToString();
				}
				else if (cpasswordEdit != null && !string.IsNullOrWhiteSpace(cpasswordEdit.Value.ToString()))
				{
					return cpasswordEdit.Value.ToString();
				}
				return null;
			}
		}

		private bool SuperUser
		{
			get
			{
				//if (Request.Form["superuser"] != null && !string.IsNullOrWhiteSpace(Request.Form["superuser"].ToString()))
				//{
				//	return Request.Form["superuser"] == "on" ? true : false;
				//}
				//return false;
				if (superuser != null)
				{
					return superuser.Checked;
				}
				return false;
			}
		}

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				UserSession currentSession = HttpSecurity.CurrentSession;
				UserBLL userBLL = new UserBLL(currentSession);
				User currentUser = new User();

				//obtiene el usuario que está logueado.
				currentUser = userBLL.SeleccionarUsuarioByIdUser(currentSession.IdUser);

				//Si no es administrador no debería entrar al formulario
				if (!currentUser.IsSuperUser)
				{
					HttpTool.Redirect(Urls.Home() + "?A=0");
				}

				LimpiarEtiquetas();
				ConsultarUsuarios();

				hdnIdUsuario.Value = "";
			}
		}

		/// <summary>
		/// Verifica si hay datos en el post.
		/// </summary>
		/// <returns></returns>
		private bool VerificarDatosDeUsuario()
		{
			if (UserName == null
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

		/// <summary>
		/// Guarda un usuario.
		/// Si el usuario ya existe, lo modifica.
		/// Si el usuario no existe, lo inserta
		/// </summary>
		private void GuardarUsuario()
		{
			LimpiarEtiquetas();

			UserSession currentSession = HttpSecurity.CurrentSession;
			UserBLL userBLL = new UserBLL(currentSession);
			User currentUser = new User();
			User user = new User();

			//obtiene el usuario que esta logueado
			currentUser = userBLL.SeleccionarUsuarioByIdUser(currentSession.IdUser);

			if (currentUser.IsSuperUser)//si es super usuario
			{
				user = GetUser();//obtiene la información del formulario.
				
				int ihdnIdUsuario;
				if (int.TryParse(hdnIdUsuario.Value.ToString(), out ihdnIdUsuario))//se va a modificar un usuario
				{
					User oldUser = userBLL.SeleccionarUsuarioByIdUser(ihdnIdUsuario);

					if (oldUser.Username == user.Username || !userBLL.ExisteUsername(user))//verifica que no exista el username
					{
						//sel le asigna el id del usuario que se va a modificar.
						user.IdUser = ihdnIdUsuario;

						/*Compara los primeros 15 dígitos de la contraseña encriptada, con la
						*contraseña que esta en el formulario, ya que al cargar la información
						*para modificar un usuario carga los primeros 15 dígitos de la contraseña
						*encriptada.
						*/
						//si las contraseñas son iguales, quiere decir que no se modificó
						//la contraseña
						if (user.Password.CompareTo(oldUser.Password.Remove(14)) == 0)
						{
							//Se queda con la misma contraseña
							user.Password = oldUser.Password;
						}
						else//Si el usuario cambió la contraseña
						{
							//encripta la contraseña que escribió el usuario.
							user.Password = MD5(user.Password.ToString());
						}

						//Se actualiza el usuario en base de datos.
						userBLL.ModificarUsuario(user);

						//Limpia el hidden field
						hdnIdUsuario.Value = "";

						password.Visible = true;
						passwordEdit.Visible = false;
						cpassword.Visible = true;
						cpasswordEdit.Visible = false;

						//oculta el botón de Cancelar Edición dado que ya se modifico el usuario.
						btnCancelarEdicion.Visible = false;

						lblUsuario.InnerText = "¡Se guardaron los cambios!";
						lblUsuario.Visible = true;

						LimpiarCampos();

						grdUsuarios_RowCancelingEdit(this, null);
					}
					else
					{
						lblError.InnerText = "¡El usuario " + user.Username + " ya existe!";
						lblError.Visible = true;
					}
				}
				else//se va a insertar un nuevo usuario
				{
					if (!userBLL.ExisteUsername(user))//verifica que no exista el username
					{
						//se encripta la contraseña
						user.Password = MD5(user.Password);

						userBLL.InsertUser(user);//inserta el usuario en la base de datos

						//Consulta los usuarios y limpia los campos del formulario.
						ConsultarUsuarios();
						LimpiarCampos();

						//muestra mensaje de que el usuario fue guardado
						lblUsuario.InnerText = "¡Usuario guardado con éxito!";
						lblUsuario.Visible = true;
					}
					else
					{
						lblError.InnerText = "¡El usuario " + user.Username + " ya existe!";
						lblError.Visible = true;
					}
				}
			}
			else
			{
				lblError.InnerText = "No tiene los permisos para agregar un usuario.";
				lblError.Visible = true;

				LimpiarCampos();
			}
		}

		/// <summary>
		/// Regresa un objeto de tipo User con la información que esta en el formulario
		/// </summary>
		/// <returns></returns>
		private User GetUser()
		{
			User user = new User();

			user.Username = UserName;
			user.Nombre = Nombre;
			user.ApellidoPaterno = Paterno;
			user.ApellidoMaterno = Materno;
			user.Email = Email;
			user.Password = Password;
			user.IsSuperUser = SuperUser;
			user.SessionToken = null;

			return user;
		}

		//Algoritmo para encriptar
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

		private void ConsultarUsuarios()
		{
			UserBLL userBLL = new UserBLL(HttpSecurity.CurrentSession);
			UserSession currentSession = HttpSecurity.CurrentSession;
			User currentUser = new User();

			/******************************************************************/
			//Selecciona los usuarios y los muestra en el grid.
			grdUsuarios.DataSource = userBLL.SeleccionarUsuarios();
			grdUsuarios.DataBind();
			udpGrdUsuarios.Update();
			/******************************************************************/
			
			//selecciona el usuario que esta logueado.
			currentUser = userBLL.SeleccionarUsuarioByIdUser(currentSession.IdUser);

			//Si no es super usuario
			if (!currentUser.IsSuperUser)
			{
				int iColumns = grdUsuarios.Columns.Count;
				//oculta columnas de editar y eliminar, dado que no tiene permiso para estas acciones.
				grdUsuarios.Columns[iColumns-2].Visible = false;//columna Editar
				grdUsuarios.Columns[iColumns-1].Visible = false;//columna Eliminar
			}
		}

		protected void grdUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			grdUsuarios.EditIndex = -1;
			ConsultarUsuarios();
		}

		protected void grdUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				LimpiarEtiquetas();

				UserSession currentSession = HttpSecurity.CurrentSession;
				UserBLL userBLL = new UserBLL(currentSession);
				User currentUser = new User();

				//obtiene el usuario que esta logueado
				currentUser = userBLL.SeleccionarUsuarioByIdUser(currentSession.IdUser);

				if(currentUser != null)
				{
					if (currentUser.IsSuperUser)//Checa si es super usuario para poder eliminar.
					{
						int IdUser;
						if (int.TryParse(e.Values["IdUser"].ToString(), out IdUser))//Id del usuario que se va a eliminar
						{
							//Para que un usuario no se pueda eliminar a si mismo.
							if (IdUser != currentUser.IdUser)
							{
								userBLL.EliminarUsuarioByIdUser(IdUser);//Se elimina el usuario.
								ConsultarUsuarios();//Muestra en el grid los usuarios que hay.

								//Muestra un mensaje de que se eliminó un usuario.
								lblUsuarioEliminado.InnerText = "¡Usuario eliminado!";
								lblUsuarioEliminado.Visible = true;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				lblError.InnerText = ex.Message;
				lblError.Visible = true;
			}
		}

		protected void grdUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
		{
			try
			{
				LimpiarEtiquetas();

				//Se obtiene el renglón que seleccionó el usuario para editar
				GridViewRow grdRow = (GridViewRow)grdUsuarios.Rows[e.NewEditIndex];
				int IdUser;
				if (int.TryParse(grdRow.Cells[0].Text.ToString(), out IdUser))//Se obtiene el Id del usuario a editar.
				{
					UserBLL userBLL = new UserBLL(HttpSecurity.CurrentSession);
					User userOBJ = new User();

					//se selecciona el usuario que se va a editar
					userOBJ = userBLL.SeleccionarUsuarioByIdUser(IdUser);

					/*Cuando se va a modificar un usuario, se cambia el control de contraseña,
					 * por un input type=text.
					 * Esto, porque el input password esta protegido y no permite asignar un
					 * valor desde el servidor. 
					 */
					password.Visible = false;
					passwordEdit.Visible = true;
					cpassword.Visible = false;
					cpasswordEdit.Visible = true;

					//Pone la información del usuario en el formualrio.
					PutUserInForm(userOBJ);

					//pone el Id del usuario en un hidden field para, al momento de presionar el
					//botón de Guadar, saber si se va a insertar un usuario nuevo o
					//se va a modificar un usuario.
					hdnIdUsuario.Value = userOBJ.IdUser.ToString();

					//Muestra el botón para cancelar la modificación
					btnCancelarEdicion.Visible = true;

					username.Focus();
				}
			}
			catch (Exception ex)
			{
				lblError.InnerText = ex.Message;
				lblError.Visible = true;
			}
		}

		protected void grdUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{

		}

		protected void LimpiarEtiquetas()
		{
			lblError.InnerText = "";
			lblError.Visible = false;

			lblUsuario.InnerText = "";
			lblUsuario.Visible = false;

			lblUsuarioEliminado.InnerText = "";
			lblUsuarioEliminado.Visible = false;
		}

		/// <summary>
		/// Limpia los campos del formulario
		/// </summary>
		private void LimpiarCampos()
		{
			username.Value = "";
			nombre.Value = "";
			paterno.Value = "";
			materno.Value = "";
			email.Value = "";
			password.Value = "";
			cpassword.Value = "";
			superuser.Checked = false;
		}

		/// <summary>
		/// Pone la información de un usuario en el formulario.
		/// </summary>
		private void PutUserInForm(User user)
		{
			if(user != null)
			{
				username.Value = user.Username;
				nombre.Value = user.Nombre;
				paterno.Value = user.ApellidoPaterno;
				materno.Value = user.ApellidoMaterno;
				email.Value = user.Email;

				int lengthPass = user.Password.Length > 15 ? 14 : user.Password.Length-1;

				passwordEdit.Value = user.Password.Remove(lengthPass);
				passwordEdit.Attributes.Add("type", "password");
				cpasswordEdit.Value = user.Password.Remove(lengthPass);
				cpasswordEdit.Attributes.Add("type", "password");

				superuser.Checked = user.IsSuperUser;
			}
		}

		protected void btnCancelarEdicion_Click(object sender, EventArgs e)
		{
			try
			{
				hdnIdUsuario.Value = "";
				LimpiarCampos();
				LimpiarEtiquetas();
				btnCancelarEdicion.Visible = false;

				password.Visible = true;
				passwordEdit.Visible = false;
				cpassword.Visible = true;
				cpasswordEdit.Visible = false;

				grdUsuarios_RowCancelingEdit(this, null);
			}
			catch (Exception ex)
			{
				lblError.InnerText = ex.Message;
				lblError.Visible = true;
			}
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				//Verifica que se recibieron datos mediante el post.
				if (VerificarDatosDeUsuario())
				{
					GuardarUsuario();
				}
			}
			catch (Exception ex)
			{
				lblError.InnerText = ex.Message;
				lblError.Visible = true;
			}
		}
	}
}