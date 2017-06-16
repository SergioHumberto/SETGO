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
using WebApplicationTemplate.Web.Reports.Classes;

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
                BindDataToDDlFormatosCert();

                divConfig.Visible = false;

                FileUpload1.Attributes["onchange"] = "UploadFile(this)";
                upldImgCertf.Attributes["onchange"] = "UploadImageCertf(this)";
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
                cargaItemsChkBoxListConfiguracionResultados();
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
            switch (Extension.ToLower())
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
            try
            {
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
                divConfig.Visible = true;

                crBLL.InsertarConfiguracionResultado(GetConfiguracionResultados());

                crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

                GuardarCarrera(crOBJ.IdConfiguracionResultados, dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connExcel.Close();
                connExcel.Dispose();
            }
        }

        private void GuardarCarrera(int idConfiguracionResultados, DataTable dt)
        {
            ResultadosBLL resultadosBLL = new ResultadosBLL();

            foreach (DataRow row in dt.Rows)
            {
                ResultadosOBJ resultadosOBJ = new ResultadosOBJ();

                resultadosOBJ.IdConfiguracionResultados = idConfiguracionResultados;

                if (row.Table.Columns.Contains("Numero"))
                {
                    int numero = 0;
                    if (int.TryParse(row["Numero"].ToString(), out numero))
                        resultadosOBJ.Numero = numero;
                }

                if (row.Table.Columns.Contains("Paterno"))
                    resultadosOBJ.Paterno = row["Paterno"].ToString();

                if (row.Table.Columns.Contains("Materno"))
                    resultadosOBJ.Materno = row["Materno"].ToString();

                if (row.Table.Columns.Contains("Nombres"))
                    resultadosOBJ.Nombres = row["Nombres"].ToString();

                if (row.Table.Columns.Contains("Folio"))
                {
                    int folio = 0;
                    if (int.TryParse(row["Folio"].ToString(), out folio))
                        resultadosOBJ.Folio = folio;
                }

                if (row.Table.Columns.Contains("Sexo"))
                    resultadosOBJ.Sexo = row["Sexo"].ToString();

                if (row.Table.Columns.Contains("Categoria"))
                    resultadosOBJ.Categoria = row["Categoria"].ToString();

                if (row.Table.Columns.Contains("Proceden"))
                    resultadosOBJ.Procedencia = row["Proceden"].ToString();

                if (row.Table.Columns.Contains("Equipo"))
                    resultadosOBJ.Equipo = row["Equipo"].ToString();

                if (row.Table.Columns.Contains("Telefono"))
                    resultadosOBJ.Telefono = row["Telefono"].ToString();

                if (row.Table.Columns.Contains("T_Chip"))
                    resultadosOBJ.T_Chip = row["T_Chip"].ToString();

                if (row.Table.Columns.Contains("T_Oficial"))
                    resultadosOBJ.T_Oficial = row["T_Oficial"].ToString();

                if (row.Table.Columns.Contains("Lug_Cat"))
                {
                    int lug_cat = 0;
                    if (int.TryParse(row["Lug_Cat"].ToString(), out lug_cat))
                        resultadosOBJ.Lug_Cat = lug_cat;
                }

                if (row.Table.Columns.Contains("Lug_Rama"))
                {
                    int lug_rama = 0;
                    if (int.TryParse(row["Lug_Rama"].ToString(), out lug_rama))
                        resultadosOBJ.Lug_Rama = lug_rama;
                }

                if (row.Table.Columns.Contains("Vel"))
                    resultadosOBJ.Vel = row["Vel"].ToString();

                if (row.Table.Columns.Contains("Lug_Gral"))
                {
                    int lug_gral = 0;
                    if (int.TryParse(row["Lug_Gral"].ToString(), out lug_gral))
                        resultadosOBJ.Lug_Gral = lug_gral;
                }

                if (row.Table.Columns.Contains("Rama"))
                    resultadosOBJ.Rama = row["Rama"].ToString();

                // added by Erik C    
                if (row.Table.Columns.Contains("T_Intermedio"))
                    resultadosOBJ.T_Intermedio = row["T_Intermedio"].ToString();
                if (row.Table.Columns.Contains("Edad"))// added by Erik C
                {
                    int edad = 0;
                    if (int.TryParse(row["Edad"].ToString(), out edad))
                        resultadosOBJ.Edad = edad;
                }
                if (row.Table.Columns.Contains("Ruta"))// added by Erik C
                    resultadosOBJ.Ruta = row["Ruta"].ToString();
                
                if (row.Table.Columns.Contains("T_5K"))// added by Erik C
                    resultadosOBJ.Ruta = row["T_5K"].ToString();

                if (row.Table.Columns.Contains("T_10K"))// added by Erik C
                    resultadosOBJ.Ruta = row["T_10K"].ToString();

                if (row.Table.Columns.Contains("T_15K"))// added by Erik C
                    resultadosOBJ.Ruta = row["T_15K"].ToString();

                if (row.Table.Columns.Contains("T_21K"))// added by Erik C
                    resultadosOBJ.Ruta = row["T_21K"].ToString();

                if (row.Table.Columns.Contains("T_25K"))// added by Erik C
                    resultadosOBJ.Ruta = row["T_25K"].ToString();

                if (row.Table.Columns.Contains("T_30K"))// added by Erik C
                    resultadosOBJ.Ruta = row["T_30K"].ToString();

                if (row.Table.Columns.Contains("T_35K"))// added by Erik C
                    resultadosOBJ.Ruta = row["T_35K"].ToString();

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
                lblSuccessConfig.InnerText = "!Los cambios han sido guardados!";
                lblSuccessConfig.Visible = true;
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
            System.Reflection.PropertyInfo[] props = crOBJ.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo prop in props)
            {
                if (prop.Name != "IdConfiguracionResultados" && prop.Name != "IdCarrera" && prop.Name != "IdCategoria" && prop.Name != "IdCertificado" && prop.Name != "ImgCertificado")
                    prop.SetValue(crOBJ, chklstCampos.Items.FindByText(prop.Name).Selected);
            }

            crOBJ.ImgCertificado = txtImgFileName.Text;
            int idCert;
            int.TryParse(ddlFormatoCert.SelectedValue, out idCert);
            crOBJ.IdCertificado = idCert;

            return crOBJ;
        }

        protected void btnConsultarResultados_Click(object sender, EventArgs e)
        {
            try
            {
                cargaItemsChkBoxListConfiguracionResultados();
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

                                divConfig.Visible = true;

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

                        divConfig.Visible = false;

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

        protected void cargaItemsChkBoxListConfiguracionResultados()
        {
            ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();
            System.Reflection.PropertyInfo[] props = crOBJ.GetType().GetProperties();
            chklstCampos.Items.Clear();

            foreach (System.Reflection.PropertyInfo prop in props)
            {
                if (prop.Name != "IdConfiguracionResultados" && prop.Name != "IdCarrera" && prop.Name != "IdCategoria" && prop.Name != "IdCertificado" && prop.Name != "ImgCertificado")
                    chklstCampos.Items.Add(new ListItem(prop.Name, "true"));
            }
        }
        private void CargarConfiguracionResultados()
        {
            limpiaMensajes();
            ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
            ConfiguracionResultadosOBJ crFinder = new ConfiguracionResultadosOBJ();

            divConfig.Visible = true;

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
                        System.Reflection.PropertyInfo[] props = crOBJ.GetType().GetProperties();

                        foreach (System.Reflection.PropertyInfo prop in props)
                        {
                            if (prop.Name != "IdConfiguracionResultados" && prop.Name != "IdCarrera" && prop.Name != "IdCategoria" && prop.Name != "IdCertificado" && prop.Name != "ImgCertificado")
                                chklstCampos.Items.FindByText(prop.Name).Selected = (bool)prop.GetValue(crOBJ);
                        }
                    }

                    txtImgFileName.Text = crOBJ.ImgCertificado;
                    ddlFormatoCert.SelectedValue = crOBJ.IdCertificado.ToString();
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

        private void limpiaMensajes()
        {
            lblSuccessConfig.InnerText = string.Empty;
            lblSuccessConfig.Visible = false;
        }

        private void LimpiarGrid()
        {
            divConfig.Visible = false;
            txtImgFileName.Text = string.Empty;
            limpiaMensajes();

            grdResultados.DataSource = null;
            grdResultados.DataBind();
        }

        protected void btnUpldImgCertf_Click(object sender, EventArgs e)
        {
            try
            {
                limpiaMensajes();
                if (upldImgCertf.HasFile)
                {
                    if (upldImgCertf.PostedFile.FileName.ToLower().EndsWith(".jpg"))
                    {
                        string FileName = "img_"
                            + DateTime.Now.Year.ToString()
                            + DateTime.Now.Month.ToString()
                            + DateTime.Now.Day.ToString()
                            + DateTime.Now.Hour.ToString()
                            + DateTime.Now.Minute.ToString()
                            + DateTime.Now.Second.ToString()
                            + DateTime.Now.Millisecond.ToString()
                            + ".jpg";
                        string Extension = Path.GetExtension(upldImgCertf.PostedFile.FileName);
                        string FolderPath = ConfigurationManager.AppSettings["ReportImagesPath"];

                        string FilePath = Server.MapPath(FolderPath + FileName);
                        upldImgCertf.SaveAs(FilePath);
                        txtImgFileName.Text = FolderPath + FileName;
                    }
                    else
                        throw new Exception("Formato no válido, solo se aceptan imagenes en formato JPG.");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void BindDataToDDlFormatosCert()
        {
            IDictionary<int, string> data = Tools.Enumeration.GetAll<ReporteCertificado.FormatoCertificado>();
            data.Add(new KeyValuePair<int, string>(0, "(----)"));
            ddlFormatoCert.DataSource = data;
            ddlFormatoCert.DataTextField = "Value";
            ddlFormatoCert.DataValueField = "Key";
            ddlFormatoCert.DataBind();
        }
    }
}
