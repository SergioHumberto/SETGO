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
        private int IdCarreraProperty
        {
            get {

                if (Request.QueryString["IdCarrera"] != null)
                {
                    int IdCarrera;
                    if (int.TryParse(Request.QueryString["IdCarrera"], out IdCarrera))
                    {
                        if (IdCarrera > 0)
                        {
                            return IdCarrera;
                        }
                    }
                }

                return -1;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                // LoadCarreras();
                ResultadosBLL resultadosBLL = new ResultadosBLL();

                if (IdCarreraProperty > 0)
                {
                    if (resultadosBLL.VerificarResultadoDeCarrera(IdCarreraProperty))
                    {
                        CargarResultados(IdCarreraProperty);
                        // tbRegistros.Visible = true;
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
                }

                lblError.Text = string.Empty;
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

			query = "SELECT IdResultado, ";

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

        protected void lnkBtnImprimirCertificado_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            Obout.Grid.TemplateContainer templateContainer = lnkBtn.NamingContainer as Obout.Grid.TemplateContainer;

            if (templateContainer != null)
            {
                Obout.Grid.GridTemplate gridTemplate = templateContainer.NamingContainer as Obout.Grid.GridTemplate;

                if (gridTemplate != null)
                {
                    Obout.Grid.GridRow gridRow = gridTemplate.NamingContainer as Obout.Grid.GridRow;

                    if (gridRow != null)
                    {
                        DataRowView drv = gridRow.DataItem as DataRowView;
                        if (drv != null)
                        {
                            int IdResultado = int.Parse(drv["IdResultado"].ToString());
                            Reports.Classes.ReporteCertificado_1 reportCertificado = new Reports.Classes.ReporteCertificado_1();

                            reportCertificado.IdCarrera = IdCarreraProperty;
                            reportCertificado.IdResultado = IdResultado;
                            reportCertificado.GenerateReport();
                        }
                    }
                }
            }            
        }
    }
}