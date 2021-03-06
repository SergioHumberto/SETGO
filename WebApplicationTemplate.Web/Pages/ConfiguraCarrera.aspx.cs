﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Web.Tools;
using WebApplicationTemplate.Objects;
using System.Globalization;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class ModificaCarrera : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LimpiaMensajes();
                if (!Page.IsPostBack)
                {
                    int idCarrera = getIdCarrera();
                    if (idCarrera != -1)
                    {
                        txtURL.Text = Urls.RegistroParticipantes() + "?IdCarrera=" + idCarrera;
                        lnkVistaPrevia.NavigateUrl = txtURL.Text;

                        BindDataToGenerales(idCarrera);
                        BindDataToCampos(getDataControlXCarrera(idCarrera));
                        BindDataToRamas(getDataRamas(idCarrera));
                        BindDataToCategorias(getDataCategorias(idCarrera));
                        BindDataToRutas(getDataRutas(idCarrera));
                        BindDataToClasificacion(getDataClasificaciones(idCarrera));
                        BindDataToEquiposGridView(getDataTipoEquipos(idCarrera));
                        
                    }
                    else
                    {
                        txtURL.Text = "No disponible";
                        lnkVistaPrevia.NavigateUrl = "#";
                    }

                    lnkShowInactiveRamas.Text = getTextlnkShowInactiveRama();
                    lnkShowInactiveCategoria.Text = getTextlnkShowInactiveCategoria();
                    lnkShowInactiveRutas.Text = getTextlnkShowInactiveRutas();
                    lnkShowInactiveEquipos.Text = getTextlnkShowInactiveEquipos();
                }
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected int getIdCarrera()
        {
            int idCarrera = -1;
            if (Request.QueryString["IdCarrera"] != null)
                int.TryParse(Request.QueryString["IdCarrera"], out idCarrera);
            return idCarrera;
        }
        protected void LimpiaMensajes()
        {
            lblError.InnerText = string.Empty;
            lblError.Visible = false;

            lblSuccessGenerales.InnerText = string.Empty;
            lblSuccessGenerales.Visible = false;

            lblErrorMessagesCampos.InnerText = string.Empty;
            lblErrorMessagesCampos.Visible = false;

            lblErrorRamas.InnerText = string.Empty;
            lblErrorRamas.Visible = false;

            lblErrorCategoria.InnerText = string.Empty;
            lblErrorCategoria.Visible = false;

            lblErrorRutas.InnerText = string.Empty;
            lblErrorRutas.Visible = false;

            lblErrorClasificaciones.InnerText = string.Empty;
            lblErrorClasificaciones.Visible = false;

            lblErrorEquipos.InnerText = string.Empty;
            lblErrorEquipos.Visible = false;
        }
        #region DatosGenerales
        protected void BindDataToGenerales(int idCarrera)
        {
            UserSession session = HttpSecurity.CurrentSession;
            CarreraBLL carreraBLL = new CarreraBLL(session);

            CarreraOBJ carreraObj = carreraBLL.SelectCarreraObject(idCarrera);
            if (carreraObj != null)
            {
                lblEstatus.Text = (carreraObj.Activo) ? "Activa" : "Inactiva";
                txtNombreCarrera.Text = carreraObj.Nombre;
                txtFecha.Text = carreraObj.Fecha.ToString("dd/MM/yyyy");
                ckeEncabezado.Text = carreraObj.ContenidoHtml;
                txtPaypalEmail.Text = carreraObj.PayPalEmail;
                txtTerminosCondic.Text = carreraObj.DescripcionPoliticas;
                txtCC.Text = carreraObj.CC;
                txtBCC.Text = carreraObj.BCC;
                txtURLRegistro.Text = carreraObj.URLRegistro;
                txtFolioInicial.Text = carreraObj.FolioInicial.ToString();
            }
        }
        protected bool validaGenerales(CarreraOBJ carreraObj)
        {
            string errores = string.Empty;
            if (carreraObj.ContenidoHtml == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El encabezado es requerido";
            }
            if (errores != string.Empty)
            {
                lblError.InnerText = errores;
                lblError.Visible = true;
                return false;
            }
            else
                return true;
        }
        protected void btnGuardarGenerales_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtiene ID CARRERA
                int idCarrera = getIdCarrera();

                // Crea y llena objeto Carrera
                CarreraOBJ carreraObj = new CarreraOBJ();

                carreraObj.IdCarrera = idCarrera;
                carreraObj.Activo = (lblEstatus.Text == "Activa") ? true : false;
                carreraObj.Nombre = txtNombreCarrera.Text;

                CultureInfo ci = CultureInfo.CreateSpecificCulture("es-MX");
                DateTime fecha = new DateTime(1753, 1, 1);
                if (txtFecha.Text != string.Empty)
                    DateTime.TryParse(txtFecha.Text, ci.DateTimeFormat, DateTimeStyles.None, out fecha);
                carreraObj.Fecha = fecha;

                carreraObj.ContenidoHtml = ckeEncabezado.Text;
                carreraObj.PayPalEmail = txtPaypalEmail.Text;
                carreraObj.DescripcionPoliticas = txtTerminosCondic.Text;
                carreraObj.CC = txtCC.Text;
                carreraObj.BCC = txtBCC.Text;
                carreraObj.URLRegistro = txtURLRegistro.Text;

                int FolioInicial;
                int.TryParse(txtFolioInicial.Text, out FolioInicial);
                carreraObj.FolioInicial = FolioInicial;

                if (validaGenerales(carreraObj))
                {
                    // Inicializa BLL y Guarda
                    UserSession session = HttpSecurity.CurrentSession;
                    CarreraBLL carreraBLL = new CarreraBLL(session);
                    if (carreraObj.IdCarrera == -1)
                    {
                        carreraObj.SiguienteFolio = carreraObj.FolioInicial;
                        carreraObj.Activo = true;
                        int id = carreraBLL.InsertarCarrera(carreraObj);
                        Response.Redirect(Urls.ConfiguraCarrera() + "?IdCarrera=" + id.ToString());
                    }
                    else
                    {
                        carreraBLL.UpdateCarrera(carreraObj);
                    }
                }
                lblSuccessGenerales.InnerText = "Se ha guardado satisfactoriamente";
                lblSuccessGenerales.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }

        }
        #endregion
        #region ControlXCarrera / Campos
        protected IList<ControlXCarreraOBJ> getDataControlXCarrera(int idCarrera)
        {
            ControlXCarreraBLL controlxcarreraBll = new ControlXCarreraBLL(HttpSecurity.CurrentSession);
            ControlXCarreraOBJ CxCObj = new ControlXCarreraOBJ();
            CxCObj.IdCarrera = idCarrera;

            return controlxcarreraBll.SelectControlXCarrera(CxCObj);
        }

        protected void BindDataToCampos(IList<ControlXCarreraOBJ> listCxC)
        {
            grdCampos.DataSource = listCxC;
            grdCampos.DataBind();
        }

        protected void obtieneListaControles()
        {
            UserSession session = HttpSecurity.CurrentSession;
            ControlBLL controlesBll = new ControlBLL(session);
            IList<ControlOBJ> lstControles = controlesBll.SelectControles();
            ViewState.Remove("lstControles");
            ViewState.Add("lstControles", lstControles);
        }

        protected void grdCampos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlControl = (e.Row.FindControl("ddlControl") as DropDownList);
                    if (ddlControl != null)
                    {
                        IList<ControlOBJ> lstControles = (IList<ControlOBJ>)ViewState["lstControles"];
                        if (lstControles.Count > 0)
                        {
                            ddlControl.DataSource = lstControles;
                            ddlControl.DataTextField = "IdControlASP";
                            ddlControl.DataValueField = "IdControl";
                            ddlControl.DataBind();
                        }
                        ddlControl.Items.Insert(0, new ListItem("< Seleccionar >"));
                        string idControl = (e.Row.FindControl("hdnIdControl") as HiddenField).Value;
                        ListItem selectedItem = ddlControl.Items.FindByValue(idControl);
                        if (selectedItem != null)
                            selectedItem.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessagesCampos.InnerText = ex.Message;
                lblErrorMessagesCampos.Visible = true;
            }
        }

        protected void grdCampos_DataBinding(object sender, EventArgs e)
        {
            try
            {
                obtieneListaControles();
            }
            catch (Exception ex)
            {
                lblErrorMessagesCampos.InnerText = ex.Message;
                lblErrorMessagesCampos.Visible = true;
            }
        }

        protected void grdCampos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();
                grdCampos.EditIndex = e.NewEditIndex;
                if (ViewState["NuevoCampo"] != null && (bool)ViewState["NuevoCampo"])
                {
                    IList<ControlXCarreraOBJ> lstCxC = getDataControlXCarrera(idCarrera);
                    lstCxC.Add(new ControlXCarreraOBJ());
                    BindDataToCampos(lstCxC);
                }
                else
                    BindDataToCampos(getDataControlXCarrera(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorMessagesCampos.InnerText = ex.Message;
                lblErrorMessagesCampos.Visible = true;
            }
        }

        protected void grdCampos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();

                grdCampos.EditIndex = -1;
                ViewState.Remove("NuevoCampo");
                BindDataToCampos(getDataControlXCarrera(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorMessagesCampos.InnerText = ex.Message;
                lblErrorMessagesCampos.Visible = true;
            }
        }
        protected bool validaCampos(ControlXCarreraOBJ CxC)
        {
            string errores = string.Empty;
            if (CxC.IdControl <= 0)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "Es necesario seleccionar una opción de la lista de Campo";
            }

            if (CxC.Etiqueta == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "La Etiqueta no debe estar vacia";
            }

            if (CxC.Requerido && CxC.EtiquetaRequerido == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El Error Requerido no debe estar vacio";
            }

            if (CxC.RegularExpression && CxC.ValidationExpression == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "La Expresion Regular no debe estar vacia";
            }

            if (CxC.RegularExpression && CxC.RegularErrorMessage == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El Error de Expresion Regular no debe estar vacio";
            }

            /*
             * Verifica si ya existe el control agregado o editado en la carrera, solo si es nuevo
             */
            if (ViewState["NuevoCampo"] != null && (bool)ViewState["NuevoCampo"])
            {
                ControlXCarreraOBJ controlXcarrera = new ControlXCarreraOBJ();
                controlXcarrera.IdCarrera = CxC.IdCarrera;
                controlXcarrera.IdControl = CxC.IdControl;

                UserSession session = HttpSecurity.CurrentSession;
                ControlXCarreraBLL controlXCarreraBll = new ControlXCarreraBLL(session);
                IList<ControlXCarreraOBJ> lstCxC = controlXCarreraBll.SelectControlXCarrera(controlXcarrera);
                if (lstCxC.Count > 0)
                {
                    errores += (errores == string.Empty) ? "" : ", ";
                    errores += "El campo seleccionado ya se está usando en esta carrera, debes seleccionar otro campo";
                }
            }
            /*
            * Si existen mensajes de error los hace visibles
            */
            if (errores != string.Empty)
            {
                lblErrorMessagesCampos.InnerText = errores;
                lblErrorMessagesCampos.Visible = true;
                return false;
            }
            else
                return true;
        }
        protected void grdCampos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                GridViewRow grdRow = (GridViewRow)grdCampos.Rows[e.RowIndex];
                //obtener los nuevos valores               
                int IdControlXCarrera;
                int.TryParse(e.NewValues["IdControlXCarrera"].ToString(), out IdControlXCarrera);

                DropDownList ddlControl = grdRow.FindControl("ddlControl") as DropDownList;
                int IdControl;
                int.TryParse(ddlControl.SelectedItem.Value, out IdControl);

                string etiqueta = (e.NewValues["Etiqueta"] != null) ? e.NewValues["Etiqueta"].ToString() : string.Empty;
                bool requerido;
                bool.TryParse(e.NewValues["Requerido"].ToString(), out requerido);
                string ErrorRequerido = (e.NewValues["EtiquetaRequerido"] != null) ? e.NewValues["EtiquetaRequerido"].ToString() : string.Empty;
                bool validar;
                bool.TryParse(e.NewValues["RegularExpression"].ToString(), out validar);
                string ExpReg = (e.NewValues["ValidationExpression"] != null) ? e.NewValues["ValidationExpression"].ToString() : string.Empty;
                string ErrorExpReg = (e.NewValues["RegularErrorMessage"] != null) ? e.NewValues["RegularErrorMessage"].ToString() : string.Empty;

                int idCarrera = getIdCarrera();

                ControlXCarreraOBJ controlXcarrera = new ControlXCarreraOBJ();
                controlXcarrera.IdCarrera = idCarrera;
                controlXcarrera.IdControlXCarrera = IdControlXCarrera;
                controlXcarrera.IdControl = IdControl;
                controlXcarrera.Etiqueta = etiqueta;
                controlXcarrera.Requerido = requerido;
                controlXcarrera.EtiquetaRequerido = ErrorRequerido;
                controlXcarrera.RegularExpression = validar;
                controlXcarrera.ValidationExpression = ExpReg;
                controlXcarrera.RegularErrorMessage = ErrorExpReg;


                if (!validaCampos(controlXcarrera)) return;
                UserSession session = HttpSecurity.CurrentSession;
                ControlXCarreraBLL controlXCarreraBll = new ControlXCarreraBLL(session);

                if (ViewState["NuevoCampo"] != null && (bool)ViewState["NuevoCampo"])
                {
                    controlXCarreraBll.InsertControlXCarrera(controlXcarrera);
                    ViewState.Remove("NuevoCampo");
                }
                else
                    controlXCarreraBll.UpdateControlXCarrera(controlXcarrera);

                grdCampos.EditIndex = -1;

                BindDataToCampos(getDataControlXCarrera(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorMessagesCampos.InnerText = ex.Message;
                lblErrorMessagesCampos.Visible = true;
            }
        }

        protected void grdCampos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                UserSession session = HttpSecurity.CurrentSession;
                ControlXCarreraBLL controlXCarreraBll = new ControlXCarreraBLL(session);


                GridViewRow row = grdCampos.Rows[e.RowIndex];
                HiddenField hdnIdControlXCarrera = row.FindControl("hdnIdControlXCarrera") as HiddenField;
                int IdControlXCarrera;
                int.TryParse(hdnIdControlXCarrera.Value, out IdControlXCarrera);

                ControlXCarreraOBJ controlXcarrera = new ControlXCarreraOBJ();
                controlXcarrera.IdControlXCarrera = IdControlXCarrera;

                int idCarrera = getIdCarrera();

                controlXCarreraBll.DeleteControlXCarrera(controlXcarrera);
                BindDataToCampos(getDataControlXCarrera(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorMessagesCampos.InnerText = ex.Message;
                lblErrorMessagesCampos.Visible = true;
            }
        }

        protected void btnAddNewRow_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            try
            {
                if (getIdCarrera() != -1)
                {
                    ViewState.Add("NuevoCampo", true);
                    grdCampos.SetEditRow(grdCampos.Rows.Count);
                }
                else
                {
                    lblErrorMessagesCampos.InnerText = "Primero debes capturar y guardar los datos generales de la carrera";
                    lblErrorMessagesCampos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMessagesCampos.InnerText = ex.Message;
                lblErrorMessagesCampos.Visible = true;
            }
        }
        #endregion
        #region Rama
        protected IList<RamaOBJ> getDataRamas(int idCarrera)
        {
            RamaBLL ramaBLL = new RamaBLL(HttpSecurity.CurrentSession);
            RamaOBJ ramaObj = new RamaOBJ();
            ramaObj.IdCarrera = idCarrera;
            ramaObj.Activo = (ViewState["ShowInactiveRamas"] != null && (bool)ViewState["ShowInactiveRamas"]) ? false : true;

            return ramaBLL.SelectRama(ramaObj);
        }
        protected void BindDataToRamas(IList<RamaOBJ> listRamas)
        {
            grdRamas.DataSource = listRamas;
            grdRamas.DataBind();
        }

        protected void grdRamas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();
                grdRamas.EditIndex = e.NewEditIndex;
                if (ViewState["NuevaRama"] != null && (bool)ViewState["NuevaRama"])
                {
                    IList<RamaOBJ> lstRama = getDataRamas(idCarrera);
                    lstRama.Add(new RamaOBJ());
                    BindDataToRamas(lstRama);
                }
                else
                    BindDataToRamas(getDataRamas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRamas.InnerText = ex.Message;
                lblErrorRamas.Visible = true;
            }
        }

        protected void grdRamas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();

                grdRamas.EditIndex = -1;
                ViewState.Remove("NuevaRama");
                BindDataToRamas(getDataRamas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRamas.InnerText = ex.Message;
                lblErrorRamas.Visible = true;
            }
        }

        protected bool validaRama(RamaOBJ param)
        {
            string errores = string.Empty;
            if (param.Nombre == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El Nombre no debe estar vacio";
            }

            /*
            * Si existen mensajes de error los hace visibles
            */
            if (errores != string.Empty)
            {
                lblErrorRamas.InnerText = errores;
                lblErrorRamas.Visible = true;
                return false;
            }
            else
                return true;
        }

        protected void grdRamas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                GridViewRow grdRow = (GridViewRow)grdRamas.Rows[e.RowIndex];
                //obtener los nuevos valores               
                int idRama;
                int.TryParse(e.NewValues["IdRama"].ToString(), out idRama);

                string nombre = (e.NewValues["Nombre"] != null) ? e.NewValues["Nombre"].ToString() : string.Empty;
                bool activo;
                bool.TryParse(e.NewValues["Activo"].ToString(), out activo);

                int idCarrera = getIdCarrera();

                RamaOBJ rama = new RamaOBJ();
                rama.IdCarrera = idCarrera;
                rama.IdRama = idRama;
                rama.Nombre = nombre;
                rama.Activo = activo;


                if (!validaRama(rama)) return;
                UserSession session = HttpSecurity.CurrentSession;
                RamaBLL ramaBll = new RamaBLL(session);

                if (ViewState["NuevaRama"] != null && (bool)ViewState["NuevaRama"])
                {
                    ramaBll.InsertarRama(rama);
                    ViewState.Remove("NuevaRama");
                }
                else
                    ramaBll.UpdateRama(rama);

                grdRamas.EditIndex = -1;

                BindDataToRamas(getDataRamas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRamas.InnerText = ex.Message;
                lblErrorRamas.Visible = true;
            }
        }

        protected void grdRamas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                UserSession session = HttpSecurity.CurrentSession;
                RamaBLL ramaBLL = new RamaBLL(session);


                GridViewRow row = grdRamas.Rows[e.RowIndex];
                HiddenField hdnIdRama = row.FindControl("hdnIdRama") as HiddenField;
                int idRama;
                int.TryParse(hdnIdRama.Value, out idRama);

                RamaOBJ rama = new RamaOBJ();
                rama.IdRama = idRama;

                int idCarrera = getIdCarrera();

                ramaBLL.DeleteRama(rama);
                BindDataToRamas(getDataRamas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRamas.InnerText = ex.Message;
                lblErrorRamas.Visible = true;
            }
        }

        protected void btnAgregarRama_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            try
            {
                if (getIdCarrera() != -1)
                {
                    ViewState.Add("NuevaRama", true);
                    grdRamas.SetEditRow(grdRamas.Rows.Count);
                }
                else
                {
                    lblErrorRamas.InnerText = "Primero debes capturar y guardar los datos generales de la carrera";
                    lblErrorRamas.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorRamas.InnerText = ex.Message;
                lblErrorRamas.Visible = true;
            }
        }
        protected string getTextlnkShowInactiveRama()
        {
            return (ViewState["ShowInactiveRamas"] != null && (bool)ViewState["ShowInactiveRamas"]) ? "Mostrar Activos" : "Mostrar Inactivos";
        }

        protected void lnkShowInactiveRamas_Click(object sender, EventArgs e)
        {
            if (ViewState["ShowInactiveRamas"] == null)
            {
                ViewState.Add("ShowInactiveRamas", true);
            }
            else
            {
                ViewState.Add("ShowInactiveRamas", !(bool)ViewState["ShowInactiveRamas"]);
            }

            int idCarrera = getIdCarrera();


            lnkShowInactiveRamas.Text = getTextlnkShowInactiveRama();
            BindDataToRamas(getDataRamas(idCarrera));
        }
        #endregion
        #region Categorias           
        protected string getTextlnkShowInactiveCategoria()
        {
            return (ViewState["ShowInactiveCategorias"] != null && (bool)ViewState["ShowInactiveCategorias"]) ? "Mostrar Activos" : "Mostrar Inactivos";
        }
        protected IList<CategoriaOBJ> getDataCategorias(int idCarrera)
        {
            CategoriaBLL categoriaBLL = new CategoriaBLL(HttpSecurity.CurrentSession);
            CategoriaOBJ categoriaObj = new CategoriaOBJ();
            categoriaObj.IdCarrera = idCarrera;
            categoriaObj.Activo = (ViewState["ShowInactiveCategorias"] != null && (bool)ViewState["ShowInactiveCategorias"]) ? false : true;

            return categoriaBLL.SelectCategoria(categoriaObj);
        }
        protected void BindDataToCategorias(IList<CategoriaOBJ> param)
        {
            grdCategorias.DataSource = param;
            grdCategorias.DataBind();
        }
        protected void lnkShowInactiveCategoria_Click(object sender, EventArgs e)
        {
            if (ViewState["ShowInactiveCategorias"] == null)
            {
                ViewState.Add("ShowInactiveCategorias", true);
            }
            else
            {
                ViewState.Add("ShowInactiveCategorias", !(bool)ViewState["ShowInactiveCategorias"]);
            }

            int idCarrera = getIdCarrera();


            lnkShowInactiveCategoria.Text = getTextlnkShowInactiveCategoria();
            BindDataToCategorias(getDataCategorias(idCarrera));
        }

        protected void grdCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();

                grdCategorias.EditIndex = e.NewEditIndex;
                if (ViewState["NuevaCategoria"] != null && (bool)ViewState["NuevaCategoria"])
                {
                    IList<CategoriaOBJ> lstCategorias = getDataCategorias(idCarrera);
                    lstCategorias.Add(new CategoriaOBJ());
                    BindDataToCategorias(lstCategorias);
                }
                else
                    BindDataToCategorias(getDataCategorias(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorCategoria.InnerText = ex.Message;
                lblErrorCategoria.Visible = true;
            }
        }

        protected void grdCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();

                grdCategorias.EditIndex = -1;
                ViewState.Remove("NuevaCategoria");
                BindDataToCategorias(getDataCategorias(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorCategoria.InnerText = ex.Message;
                lblErrorCategoria.Visible = true;
            }
        }
        protected bool validaCategoria(CategoriaOBJ param)
        {
            string errores = string.Empty;
            if (param.Nombre == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El Nombre no debe estar vacio";
            }

            if (param.Precio < 0)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El Precio debe ser mayor o igual a cero";
            }

            /*
            * Si existen mensajes de error los hace visibles
            */
            if (errores != string.Empty)
            {
                lblErrorCategoria.InnerText = errores;
                lblErrorCategoria.Visible = true;
                return false;
            }
            else
                return true;
        }

        protected void grdCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                GridViewRow grdRow = (GridViewRow)grdCategorias.Rows[e.RowIndex];
                //obtener los nuevos valores               
                int idCategoria;
                int.TryParse(e.NewValues["IdCategoria"].ToString(), out idCategoria);

                string nombre = (e.NewValues["Nombre"] != null) ? e.NewValues["Nombre"].ToString() : string.Empty;
                decimal precio;
                decimal.TryParse(e.NewValues["Precio"].ToString(), out precio);
                bool activo;
                bool.TryParse(e.NewValues["Activo"].ToString(), out activo);

                int idCarrera = getIdCarrera();

                CategoriaOBJ categoria = new CategoriaOBJ();
                categoria.IdCarrera = idCarrera;
                categoria.IdCategoria = idCategoria;
                categoria.Precio = precio;
                categoria.Nombre = nombre;
                categoria.Activo = activo;


                if (!validaCategoria(categoria)) return;
                UserSession session = HttpSecurity.CurrentSession;
                CategoriaBLL categoriaBLL = new CategoriaBLL(session);

                if (ViewState["NuevaCategoria"] != null && (bool)ViewState["NuevaCategoria"])
                {
                    categoriaBLL.InsertarCategoria(categoria);
                    ViewState.Remove("NuevaCategoria");
                }
                else
                    categoriaBLL.UpdateCategoria(categoria);

                grdCategorias.EditIndex = -1;

                BindDataToCategorias(getDataCategorias(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorCategoria.InnerText = ex.Message;
                lblErrorCategoria.Visible = true;
            }
        }

        protected void grdCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                UserSession session = HttpSecurity.CurrentSession;
                CategoriaBLL categoriaBLL = new CategoriaBLL(session);


                GridViewRow row = grdCategorias.Rows[e.RowIndex];
                HiddenField hdnIdCategoria = row.FindControl("hdnIdCategoria") as HiddenField;
                int idCategoria;
                int.TryParse(hdnIdCategoria.Value, out idCategoria);

                CategoriaOBJ categoria = new CategoriaOBJ();
                categoria.IdCategoria = idCategoria;

                int idCarrera = getIdCarrera();

                categoriaBLL.DeleteCategoria(categoria);
                BindDataToCategorias(getDataCategorias(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorCategoria.InnerText = ex.Message;
                lblErrorCategoria.Visible = true;
            }
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            try
            {
                if (getIdCarrera() != -1)
                {
                    ViewState.Add("NuevaCategoria", true);
                    grdCategorias.SetEditRow(grdCategorias.Rows.Count);
                }
                else
                {
                    lblErrorCategoria.InnerText = "Primero debes capturar y guardar los datos generales de la carrera";
                    lblErrorCategoria.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorCategoria.InnerText = ex.Message;
                lblErrorCategoria.Visible = true;
            }
        }
        #endregion
        #region Rutas
        protected IList<RutaOBJ> getDataRutas(int idCarrera)
        {
            RutaBLL rutaBLL = new RutaBLL(HttpSecurity.CurrentSession);

            RutaOBJ ruta = new RutaOBJ();
            ruta.IdCarrera = idCarrera;
            ruta.Activo = (ViewState["ShowInactiveRutas"] != null && (bool)ViewState["ShowInactiveRutas"]) ? false : true;

            return rutaBLL.SeleccionarRutasByIdCarrera(ruta);
        }
        protected void BindDataToRutas(IList<RutaOBJ> listRutas)
        {
            grdRutas.DataSource = listRutas;
            grdRutas.DataBind();
        }
        protected void obtieneListaCategorias(int idCarrera)
        {
            IList<CategoriaOBJ> lstCategorias = getDataCategorias(idCarrera);
            ViewState.Remove("lstCategorias");
            ViewState.Add("lstCategorias", lstCategorias);
        }

        protected void grdRutas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlCategoria = (e.Row.FindControl("ddlCategoria") as DropDownList);
                    if (ddlCategoria != null)
                    {
                        IList<CategoriaOBJ> lstCategorias = (IList<CategoriaOBJ>)ViewState["lstCategorias"];
                        if (lstCategorias.Count > 0)
                        {
                            ddlCategoria.DataSource = lstCategorias;
                            ddlCategoria.DataTextField = "Nombre";
                            ddlCategoria.DataValueField = "IdCategoria";
                            ddlCategoria.DataBind();
                        }
                        ddlCategoria.Items.Insert(0, new ListItem("< Seleccionar >"));
                        string idControl = (e.Row.FindControl("hdnIdCategoria") as HiddenField).Value;
                        ListItem selectedItem = ddlCategoria.Items.FindByValue(idControl);
                        if (selectedItem != null)
                            selectedItem.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorRutas.InnerText = ex.Message;
                lblErrorRutas.Visible = true;
            }
        }

        protected void grdRutas_DataBinding(object sender, EventArgs e)
        {
            try
            {
                int idCarrera = getIdCarrera();

                obtieneListaCategorias(idCarrera);
            }
            catch (Exception ex)
            {
                lblErrorRutas.InnerText = ex.Message;
                lblErrorRutas.Visible = true;
            }
        }
        protected string getTextlnkShowInactiveRutas()
        {
            return (ViewState["ShowInactiveRutas"] != null && (bool)ViewState["ShowInactiveRutas"]) ? "Mostrar Activos" : "Mostrar Inactivos";
        }
        protected void lnkShowInactiveRutas_Click(object sender, EventArgs e)
        {
            if (ViewState["ShowInactiveRutas"] == null)
            {
                ViewState.Add("ShowInactiveRutas", true);
            }
            else
            {
                ViewState.Add("ShowInactiveRutas", !(bool)ViewState["ShowInactiveRutas"]);
            }

            int idCarrera = getIdCarrera();

            lnkShowInactiveRutas.Text = getTextlnkShowInactiveRutas();
            BindDataToRutas(getDataRutas(idCarrera));
        }

        protected void grdRutas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();

                grdRutas.EditIndex = e.NewEditIndex;
                if (ViewState["NuevaRuta"] != null && (bool)ViewState["NuevaRuta"])
                {
                    IList<RutaOBJ> lstRutas = getDataRutas(idCarrera);
                    lstRutas.Add(new RutaOBJ());
                    BindDataToRutas(lstRutas);
                }
                else
                    BindDataToRutas(getDataRutas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRutas.InnerText = ex.Message;
                lblErrorRutas.Visible = true;
            }
        }

        protected void grdRutas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                int idCarrera = getIdCarrera();

                grdRutas.EditIndex = -1;
                ViewState.Remove("NuevaRuta");
                BindDataToRutas(getDataRutas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRutas.InnerText = ex.Message;
                lblErrorRutas.Visible = true;
            }
        }

        protected bool validaRuta(RutaOBJ param)
        {
            string errores = string.Empty;
            if (param.IdCategoria <= 0)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "Es necesario seleccionar una categoria";
            }

            if (param.Nombre == string.Empty)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El Nombre no debe estar vacio";
            }

            if (param.DistanciaKM < 0)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "La distancia debe ser mayor o igual a cero";
            }

            /*
            * Si existen mensajes de error los hace visibles
            */
            if (errores != string.Empty)
            {
                lblErrorMessagesCampos.InnerText = errores;
                lblErrorMessagesCampos.Visible = true;
                return false;
            }
            else
                return true;
        }

        protected void grdRutas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                GridViewRow grdRow = (GridViewRow)grdRutas.Rows[e.RowIndex];
                //obtener los nuevos valores   
                HiddenField hdnIdRuta = grdRow.FindControl("hdnIdRuta") as HiddenField;
                int IdRuta;
                int.TryParse(hdnIdRuta.Value, out IdRuta);

                DropDownList ddlCategoria = grdRow.FindControl("ddlCategoria") as DropDownList;
                int IdCategoria;
                int.TryParse(ddlCategoria.SelectedItem.Value, out IdCategoria);

                string nombre = (e.NewValues["Nombre"] != null) ? e.NewValues["Nombre"].ToString() : string.Empty;
                decimal distancia;
                decimal.TryParse(e.NewValues["DistanciaKM"].ToString(), out distancia);
                bool activo;
                bool.TryParse(e.NewValues["Activo"].ToString(), out activo);

                int idCarrera = getIdCarrera();

                RutaOBJ ruta = new RutaOBJ();
                ruta.IdCategoria = IdCategoria;
                ruta.IdRuta = IdRuta;
                ruta.Nombre = nombre;
                ruta.DistanciaKM = distancia;
                ruta.Activo = activo;

                if (!validaRuta(ruta)) return;

                UserSession session = HttpSecurity.CurrentSession;
                RutaBLL rutaBLL = new RutaBLL(session);

                if (ViewState["NuevaRuta"] != null && (bool)ViewState["NuevaRuta"])
                {
                    rutaBLL.InsertarRuta(ruta);
                    ViewState.Remove("NuevaRuta");
                }
                else
                    rutaBLL.UpdateRuta(ruta);

                grdRutas.EditIndex = -1;

                BindDataToRutas(getDataRutas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRutas.InnerText = ex.Message;
                lblErrorRutas.Visible = true;
            }
        }

        protected void grdRutas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                UserSession session = HttpSecurity.CurrentSession;
                RutaBLL rutaBLL = new RutaBLL(session);

                GridViewRow row = grdRutas.Rows[e.RowIndex];
                HiddenField hdnIdRuta = row.FindControl("hdnIdRuta") as HiddenField;
                int idRuta;
                int.TryParse(hdnIdRuta.Value, out idRuta);

                RutaOBJ ruta = new RutaOBJ();
                ruta.IdRuta = idRuta;

                int idCarrera = getIdCarrera();

                rutaBLL.DeleteRuta(ruta);
                BindDataToRutas(getDataRutas(idCarrera));
            }
            catch (Exception ex)
            {
                lblErrorRutas.InnerText = ex.Message;
                lblErrorRutas.Visible = true;
            }
        }

        protected void btnAregarRuta_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            try
            {
                if (getIdCarrera() != -1)
                {
                    ViewState.Add("NuevaRuta", true);
                    grdRutas.SetEditRow(grdRutas.Rows.Count);
                }
                else
                {
                    lblErrorRutas.InnerText = "Primero debes capturar y guardar los datos generales de la carrera";
                    lblErrorRutas.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorRutas.InnerText = ex.Message;
                lblErrorRutas.Visible = true;
            }
        }
        #endregion
        #region Clasificaciones Genéricas
        protected IList<ClasificacionOBJ> getDataClasificaciones(int idCarrera)
        {
            ClasificacionBLL clasificacionBLL = new ClasificacionBLL(HttpSecurity.CurrentSession);

            ClasificacionOBJ ruta = new ClasificacionOBJ();
            ruta.IdCarrera = idCarrera;

            return clasificacionBLL.SelectClasificacion(ruta);
        }
        protected IList<ValorClasificacionOBJ> getDataValores(int idClasificacion)
        {
            ValorClasificacionBLL clasificacionBLL = new ValorClasificacionBLL(HttpSecurity.CurrentSession);

            ValorClasificacionOBJ valor = new ValorClasificacionOBJ();
            valor.IdClasificacion = idClasificacion;

            return clasificacionBLL.SelectValorClasificacion(valor);
        }
        protected void BindDataToClasificacion(IList<ClasificacionOBJ> lstClasificacion)
        {
            lstBxClasificaciones.DataSource = lstClasificacion;
            lstBxClasificaciones.DataTextField = "Nombre";
            lstBxClasificaciones.DataValueField = "IdClasificacion";
            lstBxClasificaciones.DataBind();
        }
        protected void BindDataToValorClasificacion(IList<ValorClasificacionOBJ> lstValClas)
        {
            lstBxValores.DataSource = lstValClas;
            lstBxValores.DataTextField = "Etiqueta";
            lstBxValores.DataValueField = "IdValorClasificacion";
            lstBxValores.DataBind();
        }

        protected void lstBxClasificaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idClasificacion;
                int.TryParse(lstBxClasificaciones.SelectedItem.Value, out idClasificacion);
                BindDataToValorClasificacion(getDataValores(idClasificacion));
            }
            catch (Exception ex)
            {
                lblErrorClasificaciones.InnerText = ex.Message;
                lblErrorClasificaciones.Visible = true;
            }
        }
        protected void btnAddClasificacion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtClasificacion.Text == string.Empty)
                {
                    throw new Exception("Debes ingresar el nombre de la clasificacion");
                }

                ClasificacionOBJ clasificacion = new ClasificacionOBJ();
                clasificacion.IdCarrera = getIdCarrera();
                clasificacion.Nombre = txtClasificacion.Text;

                ClasificacionBLL clasificacionBLL = new ClasificacionBLL(HttpSecurity.CurrentSession);
                clasificacionBLL.InsertarClasificacion(clasificacion);

                txtClasificacion.Text = string.Empty;
                BindDataToClasificacion(getDataClasificaciones(getIdCarrera()));
            }
            catch (Exception ex)
            {
                lblErrorClasificaciones.InnerText = ex.Message;
                lblErrorClasificaciones.Visible = true;
            }
        }

        protected void btnDeleteClasificacion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lstBxClasificaciones.SelectedIndex == -1)
                {
                    throw new Exception("Debes seleccionar una clasificacion");
                }

                ClasificacionOBJ clasificacion = new ClasificacionOBJ();
                int idClasificacion;
                int.TryParse(lstBxClasificaciones.SelectedValue, out idClasificacion);
                clasificacion.IdClasificacion = idClasificacion;

                ClasificacionBLL clasificacionBLL = new ClasificacionBLL(HttpSecurity.CurrentSession);
                clasificacionBLL.DeleteClasificacion(clasificacion);

                BindDataToClasificacion(getDataClasificaciones(getIdCarrera()));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ValorClasificacion_Clasificacion_IdClasificacion"))
                {
                    lblErrorClasificaciones.InnerText = "Primero se deben borrar todos los valores de la clasificacion seleccionada";
                }
                else
                    lblErrorClasificaciones.InnerText = ex.Message;
                lblErrorClasificaciones.Visible = true;
            }
        }

        protected void btnAddValor_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtValor.Text == string.Empty)
                {
                    throw new Exception("Debes ingresar la etiqueta valor");
                }

                ValorClasificacionOBJ valor = new ValorClasificacionOBJ();
                int idClasificacion;
                int.TryParse(lstBxClasificaciones.SelectedValue, out idClasificacion);
                valor.IdClasificacion = idClasificacion;
                valor.Etiqueta = txtValor.Text;

                ValorClasificacionBLL valorBLL = new ValorClasificacionBLL(HttpSecurity.CurrentSession);
                valorBLL.InsertarValorClasificacion(valor);

                txtValor.Text = string.Empty;
                BindDataToValorClasificacion(getDataValores(idClasificacion));
            }
            catch (Exception ex)
            {
                lblErrorClasificaciones.InnerText = ex.Message;
                lblErrorClasificaciones.Visible = true;
            }
        }

        protected void btnDeleteValor_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (lstBxValores.SelectedIndex == -1)
                {
                    throw new Exception("Debes seleccionar un valor");
                }

                ValorClasificacionOBJ valor = new ValorClasificacionOBJ();
                int idValor;
                int.TryParse(lstBxValores.SelectedValue, out idValor);
                valor.IdValorClasificacion = idValor;

                int idClasificacion;
                int.TryParse(lstBxClasificaciones.SelectedValue, out idClasificacion);

                ValorClasificacionBLL valorBLL = new ValorClasificacionBLL(HttpSecurity.CurrentSession);
                valorBLL.DeleteValorClasificacion(valor);

                BindDataToValorClasificacion(getDataValores(idClasificacion));
            }
            catch (Exception ex)
            {
                lblErrorClasificaciones.InnerText = ex.Message;
                lblErrorClasificaciones.Visible = true;
            }
        }
        #endregion

        #region Equipos
        protected string getTextlnkShowInactiveEquipos()
        {
            return (ViewState["ShowInactiveEquipos"] != null && (bool)ViewState["ShowInactiveEquipos"]) ? "Mostrar Activos" : "Mostrar Inactivos";
        }

        protected IList<TipoEquipoOBJ> getDataTipoEquipos( int idCarrera)
        {
            UserSession session = HttpSecurity.CurrentSession;
            TipoEquipoBLL objTipoEquipoBll = new TipoEquipoBLL(session);

            TipoEquipoOBJ tipoEquipoOBJ = new TipoEquipoOBJ();
            tipoEquipoOBJ.Activo = (ViewState["ShowInactiveEquipos"] != null && (bool)ViewState["ShowInactiveEquipos"]) ? false : true;
            tipoEquipoOBJ.IdCarrera = idCarrera;

            return objTipoEquipoBll.SelectTipoEquipo(tipoEquipoOBJ);
        }

        protected void BindDataToEquiposGridView(IList<TipoEquipoOBJ> lstTipoEquipos)
        {
            grdEquipos.DataSource = lstTipoEquipos;
            grdEquipos.DataBind();
        }

        protected void btnAddNewEquipment_Click(object sender, EventArgs e)
        {
            LimpiaMensajes();
            try
            {
                ViewState.Add("Nuevo", true);
                grdEquipos.SetEditRow(grdEquipos.Rows.Count);
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void grdEquipos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                grdEquipos.EditIndex = e.NewEditIndex;
                if (ViewState["Nuevo"] != null && (bool)ViewState["Nuevo"])
                {
                    IList<TipoEquipoOBJ> lstTipoEquipos = getDataTipoEquipos(getIdCarrera());

                    lstTipoEquipos.Add(new TipoEquipoOBJ());
                    BindDataToEquiposGridView(lstTipoEquipos);
                }
                else
                    BindDataToEquiposGridView(getDataTipoEquipos(getIdCarrera()));
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected bool validaTipoEquipo(TipoEquipoOBJ p_tipoEquipoObj)
        {
            string errores = string.Empty;
            if (p_tipoEquipoObj.CantidadParticipantes <= 0)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "La cantidad de participantes debe ser mayor a cero";
            }
            if (p_tipoEquipoObj.Precio < 0)
            {
                errores += (errores == string.Empty) ? "" : ", ";
                errores += "El Precio debe ser mayor a cero";
            }

            if (errores != string.Empty)
            {
                lblError.InnerText = errores;
                lblError.Visible = true;
                return false;
            }
            else
                return true;
        }

        protected void grdEquipos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                GridViewRow grdRow = (GridViewRow)grdEquipos.Rows[e.RowIndex];

                int IdTipoEquipo;
                int.TryParse(grdRow.Cells[0].Text, out IdTipoEquipo);
                int CantidadParticipantes;
                int.TryParse(e.NewValues["CantidadParticipantes"].ToString(), out CantidadParticipantes);
                decimal precio;
                decimal.TryParse(e.NewValues["Precio"].ToString(), out precio);

                DropDownList ddlCategoria = grdRow.FindControl("ddlCategoria") as DropDownList;
                int IdCategoria;
                int.TryParse(ddlCategoria.SelectedItem.Value, out IdCategoria);

                TipoEquipoOBJ tipoEquipoObj = new TipoEquipoOBJ();
                tipoEquipoObj.IdTipoEquipo = IdTipoEquipo;
                tipoEquipoObj.CantidadParticipantes = CantidadParticipantes;
                tipoEquipoObj.Precio = precio;
                tipoEquipoObj.IdCategoria = IdCategoria;
                tipoEquipoObj.Activo = (bool)e.NewValues["Activo"];

                if (!validaTipoEquipo(tipoEquipoObj)) return;

                UserSession session = HttpSecurity.CurrentSession;
                TipoEquipoBLL objTipoEquipoBll = new TipoEquipoBLL(session);

                if (ViewState["Nuevo"] != null && (bool)ViewState["Nuevo"])
                {
                    objTipoEquipoBll.InsertaTipoEquipo(tipoEquipoObj);
                    ViewState.Remove("Nuevo");
                }
                else
                    objTipoEquipoBll.UpdateTipoEquipo(tipoEquipoObj);

                grdEquipos.EditIndex = -1;
                BindDataToEquiposGridView(getDataTipoEquipos(getIdCarrera()));
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void grdEquipos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                UserSession session = HttpSecurity.CurrentSession;
                TipoEquipoBLL objTipoEquipoBll = new TipoEquipoBLL(session);

                int IdTipoEquipo;
                int.TryParse(e.Values["IdTipoEquipo"].ToString(), out IdTipoEquipo);

                TipoEquipoOBJ tipoEquipoObj = new TipoEquipoOBJ();
                tipoEquipoObj.IdTipoEquipo = IdTipoEquipo;

                objTipoEquipoBll.DeleteTipoEquipo(tipoEquipoObj);
                BindDataToEquiposGridView(getDataTipoEquipos(getIdCarrera()));
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void grdEquipos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LimpiaMensajes();
            try
            {
                grdEquipos.EditIndex = -1;
                ViewState.Remove("Nuevo");
                BindDataToEquiposGridView(getDataTipoEquipos(getIdCarrera()));
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }
       
        protected void obtieneDatosCategorias()
        {
            UserSession session = HttpSecurity.CurrentSession;
            CategoriaBLL categoriaBLL = new CategoriaBLL(session);
            CategoriaOBJ finder = new CategoriaOBJ();
            finder.IdCarrera = getIdCarrera();
            IList<CategoriaOBJ> lstCategorias = categoriaBLL.SelectCategoria(finder);
            ViewState.Remove("lstCategorias");
            ViewState.Add("lstCategorias", lstCategorias);
        }

        protected void grdEquipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlCountries = (e.Row.FindControl("ddlCategoria") as DropDownList);
                    if (ddlCountries != null)
                    {
                        IList<CategoriaOBJ> lstCategorias = (IList<CategoriaOBJ>)ViewState["lstCategorias"];
                        if (lstCategorias.Count > 0)
                        {
                            ddlCountries.DataSource = lstCategorias;
                            ddlCountries.DataTextField = "Nombre";
                            ddlCountries.DataValueField = "IdCategoria";
                            ddlCountries.DataBind();
                        }
                        ddlCountries.Items.Insert(0, new ListItem("< Seleccionar >"));
                        string IdCategoria = (e.Row.FindControl("hdnIdCategoria") as HiddenField).Value;
                        ListItem selectedItem = ddlCountries.Items.FindByValue(IdCategoria);
                        if (selectedItem != null)
                            selectedItem.Selected = true;
                    }

                    Label lblCategoria = (e.Row.FindControl("lblCategoria") as Label);
                    if (lblCategoria != null)
                    {
                        IList<CategoriaOBJ> lstCategorias = (IList<CategoriaOBJ>)ViewState["lstCategorias"];
                        int IdCategoria;
                        int.TryParse((e.Row.FindControl("hdnIdCategoria") as HiddenField).Value, out IdCategoria);
                        if (lstCategorias.Count > 0)
                        {
                            CategoriaOBJ categoria = lstCategorias.Where(i => i.IdCategoria == IdCategoria).FirstOrDefault();
                            lblCategoria.Text = categoria.Nombre;
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

        protected void grdEquipos_DataBinding(object sender, EventArgs e)
        {
            try
            {
                obtieneDatosCategorias();
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void lnkShowInactiveEquipos_Click(object sender, EventArgs e)
        {
            if (ViewState["ShowInactiveEquipos"] == null)
            {
                ViewState.Add("ShowInactiveEquipos", true);
            }
            else
            {
                ViewState.Add("ShowInactiveEquipos", !(bool)ViewState["ShowInactiveEquipos"]);
            }
            lnkShowInactiveEquipos.Text = getTextlnkShowInactiveEquipos();
            BindDataToEquiposGridView(getDataTipoEquipos(getIdCarrera()));
        }
        #endregion
    }
}