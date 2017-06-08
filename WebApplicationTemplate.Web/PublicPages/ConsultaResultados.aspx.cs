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
using WebApplicationTemplate.Web.Reports.Classes;

namespace WebApplicationTemplate.Web.PublicPages
{
    public partial class ConsultaResultados1 : System.Web.UI.Page
    {
        private int IdCarreraProperty
        {
            get
            {

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

                if (int.TryParse(ddlCategoria.SelectedValue, out IdCategoria))
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
        public int IdCRProperty
        {
            get
            {
                int IdCR;
                if (Request.QueryString["IdCR"] != null)
                {
                    if (int.TryParse(Request.QueryString["IdCR"], out IdCR))
                    {
                        return IdCR;
                    }
                }

                return -1;
            }
        }

        public string URLRedirectImprimirCertificado
        {
            get
            {
                return Tools.Urls.ConsultaResultados();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IdResultadoProperty > 0)
                {
                    GeneraCertificado(IdResultadoProperty, IdCRProperty);
                }
                else
                {
                    LoadCarrera();
                }
            }
        }

        private void GeneraCertificado(int IdResultado, int IdConfiguracionResultados)
        {
            ReporteCertificado reportCertificado;

            ConfiguracionResultadosBLL crBLL = new ConfiguracionResultadosBLL();
            ConfiguracionResultadosOBJ crOBJ = new ConfiguracionResultadosOBJ();

            crOBJ = crBLL.SelectConfiguracionResultadosObject(IdConfiguracionResultados);
            if (crOBJ != null && crOBJ.IdCertificado > 0)
            {
                if (crOBJ.ImgCertificado != null)
                {
                    reportCertificado = new ReporteCertificado(
                                (ReporteCertificado.FormatoCertificado)Enum.ToObject(typeof(ReporteCertificado.FormatoCertificado), crOBJ.IdCertificado)
                                , crOBJ.ImgCertificado);
                }
                else
                {
                    reportCertificado = new ReporteCertificado(
                        (ReporteCertificado.FormatoCertificado)Enum.ToObject(typeof(ReporteCertificado.FormatoCertificado), crOBJ.IdCertificado));
                }
            }
            else
            {
                reportCertificado = new ReporteCertificado();
            }

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

                if (crOBJ != null && resultadosBLL.VerificarResultadoDeCarrera(crOBJ.IdConfiguracionResultados))
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

            if (IdCategoriaProperty > 0)
            {
                crFinder.IdCategoria = IdCategoriaProperty;
            }

            crOBJ = crBLL.SeleccionarConfiguracionByIdCarreraIdCategoria(crFinder);

            if (crOBJ == null)
            {
                return null;
            }

            string query = string.Empty;
            query = @"SELECT R.*, CR.IdCarrera, CR.IdConfiguracionResultados
                FROM RESULTADOS R 
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

            ddlCategoria.Items.Insert(0, new ListItem() { Text = "------", Value = "-1" });
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
                    System.Reflection.PropertyInfo[] props = objCR.GetType().GetProperties();

                    foreach (System.Reflection.PropertyInfo prop in props)
                    {
                        if (prop.Name != "IdConfiguracionResultados" && prop.Name != "IdCarrera" && prop.Name != "IdCategoria" && prop.Name != "IdCertificado" && prop.Name != "ImgCertificado")
                        {
                            Control th = e.Item.FindControl("th" + prop.Name);
                            if (th != null)
                                th.Visible = (bool)prop.GetValue(objCR);
                        }
                    }

                }
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (objCR != null)
                {
                    System.Reflection.PropertyInfo[] props = objCR.GetType().GetProperties();

                    foreach (System.Reflection.PropertyInfo prop in props)
                    {
                        if (prop.Name != "IdConfiguracionResultados" && prop.Name != "IdCarrera" && prop.Name != "IdCategoria" && prop.Name != "IdCertificado" && prop.Name != "ImgCertificado")
                        {
                            Control th = e.Item.FindControl("td" + prop.Name);
                            if (th != null)
                                th.Visible = (bool)prop.GetValue(objCR);
                        }
                    }
                }
            }
        }
    }
}