using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplicationTemplate.DAL;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web.PublicPages
{
	public partial class ConsultaResultados1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadCarreras();

				lblError.Text = string.Empty;
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

		protected void btnConsultarResultados_Click(object sender, EventArgs e)
		{
			try
			{
				lblError.Text = string.Empty;
				ResultadosBLL resultadosBLL = new ResultadosBLL();

				grdConsultarResultados.Columns.Clear();

				int idCarrera = 0;
				if (int.TryParse(ddlCarrera.SelectedValue.ToString(), out idCarrera))
				{
					if (idCarrera > 0)
					{
						if (resultadosBLL.VerificarResultadoDeCarrera(idCarrera))
						{
							CargarResultados(idCarrera);
						}
						else
						{
							lblErrorCarrera.Text = "No se han cargado resultados para esta carrera.";
						}
					}
					else
					{
						grdConsultarResultados.Visible = false;

						lblErrorCarrera.Text = "Debe seleccionar una carrera.";
						ddlCarrera.Focus();
					}
				}
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}

		protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				lblError.Text = string.Empty;
				grdConsultarResultados.PageIndex = e.NewPageIndex;
				grdConsultarResultados.DataBind();
				grdConsultarResultados.Visible = true;

				btnConsultarResultados_Click(null, e);
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}

		private void CargarResultados(int idCarrera)
		{
			try
			{
				lblError.Text = string.Empty;

				SqlConnection cn = new SqlConnection(
					DAL.DAL.ConnectionString);

				string query = GetConsulta(idCarrera);

				SqlCommand cmd = new SqlCommand(query, cn);
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);
				grdConsultarResultados.DataSource = dt;
				grdConsultarResultados.DataBind();
				cn.Close();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}

		private string GetConsulta(int idCarrera)
		{
			ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
			ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

			crOBJ = crBLL.SeleccionarConfiguracionByIdCarrera(idCarrera);

			string query = string.Empty;

			query = "SELECT ";

			if(crOBJ.Numero)
			{
				query += "Numero,";
			}
			if (crOBJ.Paterno)
			{
				query += "Paterno,";
			}
			if (crOBJ.Materno)
			{
				query += "Materno,";
			}
			if (crOBJ.Nombres)
			{
				query += "Nombres,";
			}
			if (crOBJ.Folio)
			{
				query += "Folio,";
			}
			if (crOBJ.Sexo)
			{
				query += "Sexo,";
			}
			if (crOBJ.Categoria)
			{
				query += "Categoria,";
			}
			if (crOBJ.Procedencia)
			{
				query += "Procedencia,";
			}
			if (crOBJ.Equipo)
			{
				query += "Equipo,";
			}
			if (crOBJ.Telefono)
			{
				query += "Telefono,";
			}
			if (crOBJ.T_Chip)
			{
				query += "T_Chip,";
			}
			if (crOBJ.T_Oficial)
			{
				query += "T_Oficial,";
			}
			if (crOBJ.Lug_Cat)
			{
				query += "Lug_Cat,";
			}
			if (crOBJ.Lug_Rama)
			{
				query += "Lug_Rama,";
			}
			if (crOBJ.Vel)
			{
				query += "Vel,";
			}
			if (crOBJ.Lug_Gral)
			{
				query += "Lug_Gral,";
			}
			if (crOBJ.Rama)
			{
				query += "Rama,";
			}

			query = query.Remove(query.Length - 1, 1);//Elimina la coma de al final de la cadena.

			query += " FROM RESULTADOS WHERE IdCarrera=" + idCarrera;

			return query;
		}
	}
}