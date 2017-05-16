using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web.PublicPages
{
	public partial class ConsultaResultados : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				lblError.Text = string.Empty;
				lblErrorCarrera.Text = string.Empty;

				LoadCarreras();

				chklstCampos.Visible = false;
				lblConfiguracion.Visible = false;
				btnSubmit.Visible = false;
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

		protected void btnUpload_Click(object sender, EventArgs e)
		{
			try
			{
				int idCarrera = 0;
				if (int.TryParse(ddlCarrera.SelectedValue, out idCarrera))
				{
					if (idCarrera < 0)
					{
						ddlCarrera.Focus();
						lblErrorCarrera.Text = "Debe seleccionar una carrera.";
						return;
					}
					else
					{
						lblErrorCarrera.Text = string.Empty;
					}
				}

				if (FileUpload1.HasFile)
				{
					string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
					string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
					string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

					string FilePath = Server.MapPath(FolderPath + FileName);
					FileUpload1.SaveAs(FilePath);
					Import_To_Grid(FilePath, Extension, "Yes");
				}
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}

		private void Import_To_Grid(string FilePath, string Extension, string isHDR)
		{
			string conStr = "";
			switch (Extension)
			{
				case ".xls": //Excel 97-03
					conStr = ConfigurationManager.AppSettings["Excel03ConString"];
					break;
				case ".xlsx": //Excel 07
					conStr = ConfigurationManager.AppSettings["Excel07ConString"];
					break;
			}
			conStr = String.Format(conStr, FilePath, isHDR);
			OleDbConnection connExcel = new OleDbConnection(conStr);
			OleDbCommand cmdExcel = new OleDbCommand();
			OleDbDataAdapter oda = new OleDbDataAdapter();
			DataTable dt = new DataTable();
			cmdExcel.Connection = connExcel;

			//Get the name of First Sheet
			connExcel.Open();
			DataTable dtExcelSchema;
			dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
			connExcel.Close();

			//Read Data from First Sheet
			connExcel.Open();
			cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
			oda.SelectCommand = cmdExcel;
			oda.Fill(dt);
			connExcel.Close();

			//Bind Data to GridView
			GridView1.Caption = Path.GetFileName(FilePath);
			GridView1.DataSource = dt;
			GridView1.DataBind();

			try
			{
				ResultadosBLL resultadosBLL = new ResultadosBLL();
				
				int idCarrera = 0;
				if (int.TryParse(ddlCarrera.SelectedValue, out idCarrera))
				{
					if (idCarrera < 0)
					{
						throw new Exception("Seleccione una Carrera");
					}
				}
				else
				{
					throw new Exception("Error al seleccionar la carrera");
				}

				//Si los resultados ya se habían cargado, los elimina para volver a insertarlos.
				if (resultadosBLL.VerificarResultadoDeCarrera(idCarrera))
				{
					resultadosBLL.EliminarResultadosDeCarrera(idCarrera);
				}

				foreach (DataRow row in dt.Rows)
				{
					ResultadosOBJ resultadosOBJ = new ResultadosOBJ();

					resultadosOBJ.IdCarrera = idCarrera;

					int numero = 0;
					if(int.TryParse(row["Numero"].ToString(), out numero))
					{
						resultadosOBJ.Numero = numero;
					}

					resultadosOBJ.Paterno = row["Paterno"].ToString();
					resultadosOBJ.Materno = row["Materno"].ToString();
					resultadosOBJ.Nombres = row["Nombres"].ToString();

					int folio = 0;
					if(int.TryParse(row["Folio"].ToString(), out folio))
					{
						resultadosOBJ.Folio = folio;
					}

					resultadosOBJ.Sexo = row["Sexo"].ToString();
					resultadosOBJ.Categoria = row["Categoria"].ToString();
					resultadosOBJ.Procedencia = row["Proceden"].ToString();
					resultadosOBJ.Equipo = row["Equipo"].ToString();
					resultadosOBJ.Telefono = row["Telefono"].ToString();
					resultadosOBJ.T_Chip = row["T_Chip"].ToString();
					resultadosOBJ.T_Oficial = row["T_Oficial"].ToString();

					int lug_cat = 0;
					if (int.TryParse(row["Lug_Cat"].ToString(), out lug_cat))
					{
						resultadosOBJ.Lug_Cat = lug_cat;
					}

					int lug_rama = 0;
					if (int.TryParse(row["Lug_Rama"].ToString(), out lug_rama))
					{
						resultadosOBJ.Lug_Rama = lug_rama;
					}

					resultadosOBJ.Vel = row["Vel"].ToString();

					int lug_gral = 0;
					if (int.TryParse(row["Lug_Gral"].ToString(), out lug_gral))
					{
						resultadosOBJ.Lug_Gral = lug_gral;
					}

					resultadosOBJ.Rama = row["Rama"].ToString();
					
					resultadosBLL.InsertarCarrera(resultadosOBJ);
				}
				chklstCampos.Visible = true;
				lblConfiguracion.Visible = true;
				btnSubmit.Visible = true;

				GuardarConfiguracion(idCarrera);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
			string FileName = GridView1.Caption;
			string Extension = Path.GetExtension(FileName);
			string FilePath = Server.MapPath(FolderPath + FileName);

			Import_To_Grid(FilePath, Extension, "Yes");
			GridView1.PageIndex = e.NewPageIndex;
			GridView1.DataBind();
			GridView1.Visible = true;
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			try
			{
				int idCarrera = 0;
				if (int.TryParse(ddlCarrera.SelectedValue, out idCarrera))
				{
					if (idCarrera < 0)
					{
						throw new Exception("Seleccione una Carrera");
					}
				}
				else
				{
					throw new Exception("Error al seleccionar la carrera");
				}

				GuardarConfiguracion(idCarrera);
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}

		private void GuardarConfiguracion(int idCarrera)
		{
			ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
			ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

			crOBJ.IdCarrera = idCarrera;
			crOBJ.Numero = chklstCampos.Items[0].Selected;
			crOBJ.Paterno = chklstCampos.Items[1].Selected;
			crOBJ.Materno = chklstCampos.Items[2].Selected;
			crOBJ.Nombres = chklstCampos.Items[3].Selected;
			crOBJ.Folio = chklstCampos.Items[4].Selected;
			crOBJ.Sexo = chklstCampos.Items[5].Selected;
			crOBJ.Categoria = chklstCampos.Items[6].Selected;
			crOBJ.Procedencia = chklstCampos.Items[7].Selected;
			crOBJ.Equipo = chklstCampos.Items[8].Selected;
			crOBJ.Telefono = chklstCampos.Items[9].Selected;
			crOBJ.T_Chip = chklstCampos.Items[10].Selected;
			crOBJ.T_Oficial = chklstCampos.Items[11].Selected;
			crOBJ.Lug_Cat = chklstCampos.Items[12].Selected;
			crOBJ.Lug_Rama = chklstCampos.Items[13].Selected;
			crOBJ.Vel = chklstCampos.Items[14].Selected;
			crOBJ.Lug_Gral = chklstCampos.Items[15].Selected;
			crOBJ.Rama = chklstCampos.Items[16].Selected;

			//Insertar
			if (!crBLL.VerificarConfiguracionDeCarrera(idCarrera))
			{
				crBLL.InsertarConfiguracionResultado(crOBJ);
			}
			else//Update
			{
				crBLL.ActualizarConfiguracion(crOBJ);
			}
		}

		protected void btnConsultarResultados_Click(object sender, EventArgs e)
		{
			try
			{
				ResultadosBLL resultadosBLL = new ResultadosBLL();

				GridView1.Columns.Clear();

				int idCarrera = 0;
				if (int.TryParse(ddlCarrera.SelectedValue.ToString(), out idCarrera))
				{
					if (idCarrera > 0)
					{
						if (resultadosBLL.VerificarResultadoDeCarrera(idCarrera))
						{
							GridView1.Visible = true;
							GridView1.DataSource = resultadosBLL.SeleccionarResultadosByIdCarrera(idCarrera);
							GridView1.DataBind();

							chklstCampos.Visible = true;
							lblConfiguracion.Visible = true;
							btnSubmit.Visible = true;
							lblErrorCarrera.Text = string.Empty;

							CargarConfiguracionResultados();
						}
						else
						{
							lblErrorCarrera.Text = "No se han cargado resultados para esta carrera.";
						}
					}
					else
					{
						GridView1.Visible = false;

						chklstCampos.Visible = false;
						lblConfiguracion.Visible = false;
						btnSubmit.Visible = false;

						lblErrorCarrera.Text = "Debe seleccionar una carrera.";
						ddlCarrera.Focus();
					}
				}
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
			}
		}

		private void CargarConfiguracionResultados()
		{
			ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();

			int idCarrera = 0;
			if (int.TryParse(ddlCarrera.SelectedValue.ToString(), out idCarrera))
			{
				if (crBLL.VerificarConfiguracionDeCarrera(idCarrera))
				{
					ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

					crOBJ = crBLL.SeleccionarConfiguracionByIdCarrera(idCarrera);
					
					
				}
			}
		}
	}
}