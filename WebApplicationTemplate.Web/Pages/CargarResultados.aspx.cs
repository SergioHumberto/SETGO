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
using WebApplicationTemplate.Web.Tools;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web.PublicPages
{
    public partial class ConsultaResultados : System.Web.UI.Page
    {
        private int IDCARRERA
        {
            get
            {
                int idCarrera = 0;
                if (int.TryParse(ddlCarrera.SelectedValue, out idCarrera))
                {
                    return idCarrera;
                }
                return -1;
            }
        }
        private int? IDCATEGORIA
        {
            get
            {
                int idCategoria = 0;
                if (int.TryParse(ddlCategoria.SelectedValue, out idCategoria))
                {
                    if (idCategoria > 0)
                    {
                        return idCategoria;
                    }
                }
                return null;
            }
        }

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

                FileUpload1.Attributes["onchange"] = "UploadFile(this)";
            }
        }

        protected void cargaURL()
        {
            string parametros = (IDCARRERA != -1) ? "?IdCarrera=" + IDCARRERA : string.Empty;
            parametros += (IDCARRERA != -1 && IDCATEGORIA != null) ? "&IdCategoria=" + IDCATEGORIA : string.Empty;
            txtURL.Text = (parametros != string.Empty) ? Urls.ConsultaResultados() + parametros : "";
            lnkVistaPrevia.NavigateUrl = txtURL.Text;

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
                lblError.Text = string.Empty;
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
            catch (Exception ex)
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
            grdResultados.Caption = Path.GetFileName(FilePath);
            grdResultados.DataSource = dt;
            grdResultados.DataBind();

            try
            {
                ResultadosBLL resultadosBLL = new ResultadosBLL();
                ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
                ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();
                ConfiguracionResultadosOBJ crFinder = new ConfiguracionResultadosOBJ();

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

                int idCategoria = 0;
                if (int.TryParse(ddlCategoria.SelectedValue, out idCategoria))
                {
                    if (idCategoria > 0)
                    {
                        crFinder.IdCategoria = idCategoria;
                    }
                }

                crFinder.IdCarrera = idCarrera;

                //Si los resultados ya se habían cargado, los elimina para volver a insertarlos.
                if (crBLL.VerificarConfiguracionDeCarrera(crFinder))
                {
                    crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

                    resultadosBLL.EliminarResultadosByIdConfiguracionResultados(crOBJ.IdConfiguracionResultados);
                    crBLL.EliminarConfiguracionByIdCarreraIdCategoria(crOBJ);
                }

                chklstCampos.Visible = true;
                lblConfiguracion.Visible = true;
                btnSubmit.Visible = true;

                crBLL.InsertarConfiguracionResultado(GetConfiguracionResultados());

                crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

                GuardarCarrera(crOBJ.IdConfiguracionResultados, dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void GuardarCarrera(int idConfiguracionResultados, DataTable dt)
        {
            ResultadosBLL resultadosBLL = new ResultadosBLL();

            foreach (DataRow row in dt.Rows)
            {
                ResultadosOBJ resultadosOBJ = new ResultadosOBJ();

                resultadosOBJ.IdConfiguracionResultados = idConfiguracionResultados;

                int numero = 0;
                if (int.TryParse(row["Numero"].ToString(), out numero))
                {
                    resultadosOBJ.Numero = numero;
                }

                resultadosOBJ.Paterno = row["Paterno"].ToString();
                resultadosOBJ.Materno = row["Materno"].ToString();
                resultadosOBJ.Nombres = row["Nombres"].ToString();

                int folio = 0;
                if (int.TryParse(row["Folio"].ToString(), out folio))
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

                // added by Erik C    
                if (row.Table.Columns.Contains("T_Intermedio"))
                    resultadosOBJ.T_Intermedio = row["T_Intermedio"].ToString();
                if (row.Table.Columns.Contains("Edad"))
                {
                    int edad = 0;
                    if (int.TryParse(row["Edad"].ToString(), out edad))
                        resultadosOBJ.Edad = edad;
                }
                resultadosBLL.InsertarCarrera(resultadosOBJ);
            }
        }

        protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultados.PageIndex = e.NewPageIndex;
            grdResultados.DataBind();
            grdResultados.Visible = true;

            btnConsultarResultados_Click(null, e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
                ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

                lblError.Text = string.Empty;

                crOBJ = GetConfiguracionResultados();

                crBLL.ActualizarConfiguracion(crOBJ);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private ConfiguracionResultadosOBJ GetConfiguracionResultados()
        {
            ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

            crOBJ.IdCarrera = IDCARRERA;
            crOBJ.IdCategoria = IDCATEGORIA;
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
            crOBJ.Edad = chklstCampos.Items[17].Selected;
            crOBJ.T_Intermedio = chklstCampos.Items[18].Selected;            

            return crOBJ;
        }

        protected void btnConsultarResultados_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = string.Empty;

                ResultadosBLL resultadosBLL = new ResultadosBLL();
                ConfiguracionResultadosOBJ crFinder = new ConfiguracionResultadosOBJ();
                ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
                ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

                grdResultados.Columns.Clear();

                int idCarrera = 0;
                if (int.TryParse(ddlCarrera.SelectedValue.ToString(), out idCarrera))
                {
                    if (idCarrera > 0)
                    {
                        crFinder.IdCarrera = idCarrera;
                        crFinder.IdCategoria = IDCATEGORIA;

                        crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

                        if (crOBJ != null)
                        {
                            if (resultadosBLL.VerificarResultadoDeCarrera(crOBJ.IdConfiguracionResultados))
                            {
                                grdResultados.Visible = true;
                                grdResultados.DataSource = resultadosBLL.SeleccionarResultadosByConfiguracionResultados(crOBJ.IdConfiguracionResultados);
                                grdResultados.DataBind();

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
                            lblErrorCarrera.Text = "No se han cargado resultados para esta carrera.";
                        }
                    }
                    else
                    {
                        grdResultados.Visible = false;

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
            ConfiguracionResultadosOBJ crFinder = new ConfiguracionResultadosOBJ();

            lblConfiguracion.Visible = true;
            btnSubmit.Visible = true;
            chklstCampos.Visible = true;

            int idCarrera = 0;
            if (int.TryParse(ddlCarrera.SelectedValue.ToString(), out idCarrera))
            {
                crFinder.IdCarrera = idCarrera;
                crFinder.IdCategoria = IDCATEGORIA;

                if (crBLL.VerificarConfiguracionDeCarrera(crFinder))
                {
                    ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

                    crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

                    if (crOBJ != null)
                    {
                        chklstCampos.Items[0].Selected = crOBJ.Numero;
                        chklstCampos.Items[1].Selected = crOBJ.Paterno;
                        chklstCampos.Items[2].Selected = crOBJ.Materno;
                        chklstCampos.Items[3].Selected = crOBJ.Nombres;
                        chklstCampos.Items[4].Selected = crOBJ.Folio;
                        chklstCampos.Items[5].Selected = crOBJ.Sexo;
                        chklstCampos.Items[6].Selected = crOBJ.Categoria;
                        chklstCampos.Items[7].Selected = crOBJ.Procedencia;
                        chklstCampos.Items[8].Selected = crOBJ.Equipo;
                        chklstCampos.Items[9].Selected = crOBJ.Telefono;
                        chklstCampos.Items[10].Selected = crOBJ.T_Chip;
                        chklstCampos.Items[11].Selected = crOBJ.T_Oficial;
                        chklstCampos.Items[12].Selected = crOBJ.Lug_Cat;
                        chklstCampos.Items[13].Selected = crOBJ.Lug_Rama;
                        chklstCampos.Items[14].Selected = crOBJ.Vel;
                        chklstCampos.Items[15].Selected = crOBJ.Lug_Gral;
                        chklstCampos.Items[16].Selected = crOBJ.Rama;
                        chklstCampos.Items[17].Selected = crOBJ.Edad;
                        chklstCampos.Items[18].Selected = crOBJ.T_Intermedio;
                    }
                }
            }
        }

        protected void ddlCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimpiarGrid();
                lblError.Text = string.Empty;

                CategoriaBLL catBLL = new CategoriaBLL(Tools.HttpSecurity.CurrentSession);
                CategoriaOBJ catOBJ = new CategoriaOBJ();
                IList<CategoriaOBJ> lstCat;

                int idCarrera = 0;
                if (int.TryParse(ddlCarrera.SelectedValue, out idCarrera))
                {
                    if (idCarrera > 0)
                    {
                        ddlCategoria.Items.Clear();
                        lblCategoria.Visible = true;
                        ddlCategoria.Visible = true;

                        catOBJ.IdCarrera = idCarrera;

                        lstCat = catBLL.SelectCategoria(catOBJ);

                        ListItem item = new ListItem();

                        item.Value = "-1";
                        item.Text = "-- Seleccione una Categoría --";

                        ddlCategoria.Items.Add(item);

                        ddlCategoria.DataSource = lstCat;
                        ddlCategoria.DataTextField = "Nombre";
                        ddlCategoria.DataValueField = "IdCategoria";
                        ddlCategoria.DataBind();
                    }
                    else
                    {
                        lblCategoria.Visible = false;
                        ddlCategoria.Visible = false;
                    }
                }
                cargaURL();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void Upload(object sender, EventArgs e)
        {
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimpiarGrid();
                lblError.Text = string.Empty;

                cargaURL();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void LimpiarGrid()
        {
            lblConfiguracion.Visible = false;
            btnSubmit.Visible = false;
            chklstCampos.Visible = false;

            grdResultados.DataSource = null;
            grdResultados.DataBind();
        }
    }
}
