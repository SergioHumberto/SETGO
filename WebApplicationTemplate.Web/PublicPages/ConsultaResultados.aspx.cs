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
using System.Web.Services;

namespace WebApplicationTemplate.Web.PublicPages
{
	public partial class ConsultaResultados1 : System.Web.UI.Page
	{
        private int IdCarreraProperty
        {
            get {

                int IdCarrera;
                if (Request.QueryString["IdCarrera"] != null)
                {
                    if (int.TryParse(Request.QueryString["IdCarrera"], out IdCarrera))
                    {
                        if (IdCarrera > 0)
                        {
                            return IdCarrera;
                        }
                    }
                }

                if (int.TryParse(ddlCarrera.SelectedValue, out IdCarrera))
                {
                    if (IdCarrera > 0)
                    {
                        return IdCarrera;
                    }
                }

                return -1;
            }
        }

        public int IdCategoriaProperty
        {
            get
            {
                int IdCategoria;

                if (Request.QueryString["IdCategoria"] != null)
                {
                    if (int.TryParse(Request.QueryString["IdCategoria"], out IdCategoria))
                    {
                        if (IdCategoria > 0)
                        {
                            return IdCategoria;
                        }
                    }
                }

                if(int.TryParse(ddlCategoria.SelectedValue, out IdCategoria))
                {
                    if (IdCategoria > 0)
                    {
                        return IdCategoria;
                    }
                }

                return -1;
            }
        }

        public int IdResultadoProperty
        {
            get
            {
                int IdResultado;
                if (Request.QueryString["IdResultado"] != null)
                {
                    if (int.TryParse(Request.QueryString["IdResultado"], out IdResultado))
                    {
                        return IdResultado;
                    }
                }

                return -1;
            }
        }

        public string URLRedirectImprimirCertificado
        {
            get
            {
                string url = "~/PublicPages/ConsultaResultados.aspx";
                return Tools.Urls.Abs(url);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (IdResultadoProperty > 0)
                {
                    GeneraCertificado(IdResultadoProperty);
                }
                else
                {
                    LoadCarrera();
                }
			}
		}

        private void GeneraCertificado(int IdResultado)
        {
            Reports.Classes.ReporteCertificado_3 reportCertificado = new Reports.Classes.ReporteCertificado_3();

            reportCertificado.IdCarrera = IdCarreraProperty;
            reportCertificado.IdResultado = IdResultado;
            reportCertificado.GenerateReport();
        }

        private void LoadCarrera()
        {
            ResultadosBLL resultadosBLL = new ResultadosBLL();

            lblErrorCarrera.Text = string.Empty;
            if (IdCarreraProperty > 0)
            {
				ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
				ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();
				ConfiguracionResultadosOBJ crFinder = new ConfiguracionResultadosOBJ();
				crFinder.IdCarrera = IdCarreraProperty;

				if (IdCategoriaProperty > 0)
				{
					crFinder.IdCategoria = IdCategoriaProperty;
				}

				crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

                if (crOBJ != null &&  resultadosBLL.VerificarResultadoDeCarrera(crOBJ.IdConfiguracionResultados))
                {
                    if (IdCategoriaProperty <= 0)
                    {
                        CargarResultados(IdCarreraProperty, null);
                        LoadCategoriasXCarrera(IdCarreraProperty);
                    }
                    else
                    {
                        CargarResultados(IdCarreraProperty, IdCategoriaProperty);
                    }
                }
                else
                {
                    lblErrorCarrera.Text = "No se han cargado resultados para esta carrera.";
                }
            }
            else
            {
                // repeater.Visible = false;
                lblErrorCarrera.Text = "Debe seleccionar una carrera.";
                LoadDdlCarrera();
            }
        }

        private void LoadDdlCarrera()
        {
            phCarrera.Visible = true;

            UserSession session = Tools.HttpSecurity.CurrentSession;
            CarreraBLL objCarreraBLL = new CarreraBLL(session);
            IList<CarreraOBJ> lstCarrera = objCarreraBLL.SelectCarrera(new CarreraOBJ() { }); // Todos los resultados

            ddlCarrera.DataSource = lstCarrera;
            ddlCarrera.DataTextField = "Nombre";
            ddlCarrera.DataValueField = "IdCarrera";
            ddlCarrera.DataBind();

            ddlCarrera.Items.Insert(0, new ListItem() { Text = "--------", Value = "-1" });
        }

		private void CargarResultados(int idCarrera, int? IdCategoria)
		{
			try
			{
                lblErrorCarrera.Text = string.Empty;
				string query = GetConsulta(idCarrera, IdCategoria);

                if (!string.IsNullOrEmpty(query))
                {
                    DataTable dt = GetDataTable(query);
                    repeater.DataSource = dt;
                    repeater.DataBind();                    
                }
			}
			catch (Exception ex)
			{
                lblErrorCarrera.Text = ex.Message;
			}
		}

        // Este metodo se moverá a una clase que ejecute consultas con SqlClient.
        private DataTable GetDataTable(string strQuery)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(DAL.DAL.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strQuery, cn))
                {
                    // connection timeout default its 30 seconds.
                    // to fix
                    // cmd.CommandTimeout = cn.ConnectionTimeout;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

		private string GetConsulta(int idCarrera, int? IdCategoria)
		{
			ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
			ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();
			ConfiguracionResultadosOBJ crFinder = new ConfiguracionResultadosOBJ();

			crFinder.IdCarrera = idCarrera;

			if(IdCategoriaProperty > 0)
			{
				crFinder.IdCategoria = IdCategoriaProperty;
			}

			crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

            if (crOBJ == null)
            {
                return null;
            }

			string query = string.Empty;

			query = "SELECT R.IdResultado, CR.IdCarrera,";

			//if(crOBJ.Numero)
			//{
				query += "R.Numero,";
			// }
			//if (crOBJ.Paterno)
			//{
				query += "R.Paterno,";
			// }
			//if (crOBJ.Materno)
			//{
				query += "R.Materno,";
			// }
			//if (crOBJ.Nombres)
			//{
				query += "R.Nombres,";
			//}
			//if (crOBJ.Folio)
			//{
				query += "R.Folio,";
			// }
			//if (crOBJ.Sexo)
			//{
				query += "R.Sexo,";
			//}
			//if (crOBJ.Categoria)
			//{
				query += "R.Categoria,";
			//}
			//if (crOBJ.Procedencia)
			//{
				query += "R.Procedencia,";
			//}
			//if (crOBJ.Equipo)
			//{
				query += "R.Equipo,";
			//}
			//if (crOBJ.Telefono)
			//{
				query += "R.Telefono,";
			//}
			//if (crOBJ.T_Chip)
			//{
				query += "R.T_Chip,";
			//}
			//if (crOBJ.T_Oficial)
			//{
				query += "R.T_Oficial,";
			//}
			//if (crOBJ.Lug_Cat)
			//{
				query += "R.Lug_Cat,";
			//}
			//if (crOBJ.Lug_Rama)
			//{
				query += "R.Lug_Rama,";
			//}
			//if (crOBJ.Vel)
			//{
				query += "R.Vel,";
			//}
			//if (crOBJ.Lug_Gral)
			//{
				query += "R.Lug_Gral,";
			//}
			//if (crOBJ.Rama)
			//{
				query += "R.Rama,";
			// }

			query = query.Remove(query.Length - 1, 1);//Elimina la coma de al final de la cadena.

			query += @" FROM RESULTADOS R 
				INNER JOIN ConfiguracionResultados CR ON CR.IdConfiguracionResultados = R.IdConfiguracionResultados
				WHERE CR.IdCarrera=" + idCarrera;

            if (IdCategoria == null)
            {
                query += " AND CR.IdCategoria IS NULL";
            }
			else
			{
				query += " AND CR.IdCategoria = " + IdCategoria;
			}

			return query;
		}

        protected void ddlCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleanRepeater();

            if (ddlCarrera.SelectedValue != "-1")
            {
                lblErrorCarrera.Text = "";

                int IdCarrera;
                if (int.TryParse(ddlCarrera.SelectedValue, out IdCarrera))
                {
                    if (IdCarrera > 0)
                    {
                        LoadCategoriasXCarrera(IdCarrera);
                        CargarResultados(IdCarrera, null);
                    }
                }
            }

            if (ddlCategoria.Items.Count > 0)
            {
                ddlCategoria.SelectedIndex = 0;
            }
        }

        private void LoadCategoriasXCarrera(int IdCarrera)
        {
            phCategoria.Visible = true;
            UserSession session = Tools.HttpSecurity.CurrentSession;
            CategoriaBLL objCategoriaBLL = new CategoriaBLL(session);
            IList<CategoriaOBJ> lstCategorias = objCategoriaBLL.SelectCategoria(new CategoriaOBJ() { IdCarrera = IdCarrera });

            ddlCategoria.DataSource = lstCategorias;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();

            ddlCategoria.Items.Insert(0, new ListItem() { Text="------", Value = "-1" });
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdCarrera;
            CleanRepeater();

            if (!int.TryParse(ddlCarrera.SelectedValue, out IdCarrera) || IdCarrera <= 0)
            {
                IdCarrera = IdCarreraProperty;
            }

            if (IdCarrera > 0)
            {
                // repeater.Visible = true;

                int IdCategoria;
                if (int.TryParse(ddlCategoria.SelectedValue, out IdCategoria))
                {
                    if (IdCategoria > 0)
                    {
                        CargarResultados(IdCarrera, IdCategoria);
                    }
                    else
                    {
                        CargarResultados(IdCarrera, null);
                    }

                    if (repeater.Items.Count == 0)
                    {
                        lblErrorCarrera.Text = "No existen resultados para la carrera con la categoria seleccionada";
                    }
                }
            }
        }

        private void CleanRepeater()
        {
            repeater.DataSource = null;
            repeater.DataBind();
        }

        protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ConfiguracionResultadosBLL objConfiguracionResultadosBLL = new ConfiguracionResultadosBLL();
            ConfiguracionResultadosOBJ finder = new ConfiguracionResultadosOBJ();
            finder.IdCarrera = IdCarreraProperty;

            if (IdCategoriaProperty > 0)
            {
                finder.IdCategoria = IdCategoriaProperty;
            }

            ConfiguracionResultadosOBJ objCR = objConfiguracionResultadosBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(finder);

            if (e.Item.ItemType == ListItemType.Header)
            {
                if (objCR != null)
                {
                    e.Item.FindControl("thNombres").Visible = objCR.Nombres;
                    e.Item.FindControl("thPaterno").Visible = objCR.Paterno;
                    e.Item.FindControl("thMaterno").Visible = objCR.Materno;
                    e.Item.FindControl("thSexo").Visible = objCR.Sexo;
                    e.Item.FindControl("thTiempoChip").Visible = objCR.T_Chip;
                    e.Item.FindControl("thLugarRama").Visible = objCR.Lug_Rama;
                    e.Item.FindControl("thVel").Visible = objCR.Vel;
                    e.Item.FindControl("thLugarGeneral").Visible = objCR.Lug_Gral;
                }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (objCR != null)
                {
                    e.Item.FindControl("tdNombres").Visible = objCR.Nombres;
                    e.Item.FindControl("tdPaterno").Visible = objCR.Paterno;
                    e.Item.FindControl("tdMaterno").Visible = objCR.Materno;
                    e.Item.FindControl("tdSexo").Visible = objCR.Sexo;
                    e.Item.FindControl("tdTiempoChip").Visible = objCR.T_Chip;
                    e.Item.FindControl("tdLugarRama").Visible = objCR.Lug_Rama;
                    e.Item.FindControl("tdVel").Visible = objCR.Vel;
                    e.Item.FindControl("tdLugarGeneral").Visible = objCR.Lug_Gral;
                }
            }
        }
    }
}