using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Web.Tools;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class RegistroParticipantes : System.Web.UI.Page
    {
        private int IdCarreraProperty
        {
            get
            {
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

        private int IdParticipanteVSProperty
        {
            get
            {
                if (ViewState["IdParticipante"] != null)
                {
                    int id;
                    if (int.TryParse(ViewState["IdParticipante"].ToString(), out id))
                    {
                        return id;
                    }
                }

                return -1;
            }

            set
            {
                ViewState["IdParticipante"] = value;
            }
        }

        private int IdParticipanteXCarreraProperty
        {
            get
            {
                if (ViewState["IdParticipanteXCarreraProperty"] != null)
                {
                    int id;
                    if (int.TryParse(ViewState["IdParticipanteXCarreraProperty"].ToString(), out id))
                    {
                        return id;
                    }
                }

                return -1;
            }

            set
            {
                ViewState["IdParticipanteXCarreraProperty"] = value;
            }
        }

        public string URLWSGetPrecioCategoria
        {
            get
            {
                string URL = Urls.Abs("~/Pages/RegistroParticipantes.aspx/WSGetPrecioCategoria");
                return URL;
            }
        }

        public bool RegistroEnEquipo
        {
            get { return rblTipoRegistro.SelectedValue == "E"; }
        }

        public int IdCategoriaProperty
        {
            get
            {
                int IdCategoria;

                if (int.TryParse(rblCategoria.SelectedValue, out IdCategoria))
                {
                    if (IdCategoria > 0)
                    {
                        return IdCategoria;
                    }
                }

                return -1;
            }
        }
        
        public enum ETipoRegistro
        {
            Individual = 'I'
            , Equipo = 'E'
        }

        private int IdEquipoPropertyVS
        {
            get
            {
                if (ViewState["IdEquipo"] != null)
                {
                    return int.Parse(ViewState["IdEquipo"].ToString());
                }

                return -1;
            }

            set { ViewState["IdEquipo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EliminaVariablesDeSession();
                if (IdCarreraProperty > 0)
                {
                    LoadCarrera(IdCarreraProperty);

                    if (Request.QueryString["UEQ"] != null)
                    {
                        string emailTo = string.Empty;

                        if (Request.QueryString["emailTo"] != null)
                        {
                            emailTo = Request.QueryString["emailTo"];
                        }

                        LoadEquipoSettings(IdCarreraProperty, Request.QueryString["UEQ"], emailTo);
                    }
                }
                else
                {
                    cusError.ErrorMessage = "No ha seleccionado una carrera";
                    cusError.IsValid = false;
                    btnEnviar.Visible = false;
                }

				PagoOffline();

				// lblRuta.Visible = false;
			}
        }

        private void LoadEquipoSettings(int IdCarrera, string strUEQ, string emailTo)
        {
            try
            {
                Guid UEQ = new Guid(strUEQ);
                UserSession session = Tools.HttpSecurity.CurrentSession;
                EquipoBLL objEquipoBLL = new EquipoBLL(session);
                IList<EquipoOBJ> lstEquipos = objEquipoBLL.SelectEquipos(new EquipoOBJ() { IdCarrera = IdCarrera, Guid = UEQ });

                if (lstEquipos.Count == 0)
                {
                    throw new Exception("No se encontró el equipo con la carrera indicada");
                }
                else if (lstEquipos.Count == 1)
                {
                    EquipoOBJ objEquipo = lstEquipos[0];
                    TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);

                    if (objEquipo.IdTipoEquipo.HasValue)
                    {
                        TipoEquipoOBJ objTipoEquipo = objTipoEquipoBLL.SelectTipoEquipoObject(objEquipo.IdTipoEquipo.Value);
                        if (objTipoEquipo != null)
                        {
                            BloqueaCategoriaRbl(objTipoEquipo.IdCategoria);
                            BloqueaTipoRegistroRbl(ETipoRegistro.Equipo);
                            CargarRutasByIdCategoria(objTipoEquipo.IdCategoria);

                            if (!string.IsNullOrEmpty(emailTo))
                            {
                                txtEmail.Text = emailTo;
                            }
                        }
                    }
                }
                else if(lstEquipos.Count > 1)
                {
                    throw new Exception("Se encontró mas de un equipo registrado");
                }
            }
            catch(Exception ex)
            {
                cusError.ErrorMessage = "Error: " + ex.Message;
                cusError.IsValid = false;
            }
        }

        private void BloqueaTipoRegistroRbl(ETipoRegistro eTipoRegistro)
        {
            char value = (char)eTipoRegistro;
            ListItem item = BuscaEnRbl(rblTipoRegistro, value.ToString());
            if (item != null)
            {
                item.Selected = true;
                rblTipoRegistro.SelectedValue = item.Value;
                rblTipoRegistro.Enabled = false;
            }
        }

        private void BloqueaCategoriaRbl(int IdCategoria)
        {
            ListItem item = BuscaEnRbl(rblCategoria, IdCategoria.ToString());
            if (item != null)
            {
                item.Selected = true;
                rblCategoria.Enabled = false;
            }
        }

        private ListItem BuscaEnRbl(RadioButtonList rblControl, string value)
        {
            if (rblControl.Items.Count > 0)
            {
                foreach (ListItem item in rblControl.Items)
                {
                    if(item.Value.CompareTo(value) == 0)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public static Control[] FlattenHierachy(Control root)
        {
            List<Control> list = new List<Control>();
            list.Add(root);
            if (root.HasControls())
            {
                foreach (Control control in root.Controls)
                {
                    list.AddRange(FlattenHierachy(control));
                }
            }
            return list.ToArray();
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IdCarreraProperty > 0)
                {
                    Control[] allControls = FlattenHierachy(Page);

                    ControlXCarreraBLL objControlXCarreraBLL = new ControlXCarreraBLL(HttpSecurity.CurrentSession);
                    IList<ControlXCarreraOBJ> lstControlXCarrera = objControlXCarreraBLL.SelectControlXCarrera(new ControlXCarreraOBJ() { IdCarrera = IdCarreraProperty });

                    foreach (ControlXCarreraOBJ itemControlXCarrera in lstControlXCarrera)
                    {
                        foreach (Control itemControl in allControls)
                        {
                            if (itemControl.ID == itemControlXCarrera.IdControlASP)
                            {
                                itemControl.Visible = true;

                                Type typeControl = itemControl.GetType();

                                if (typeControl == typeof(PlaceHolder))
                                {
                                    string strNameControl = itemControlXCarrera.IdControlASP.Substring(2, itemControlXCarrera.IdControlASP.Length - 2);

                                    string strPrefixLabel = "lbl";
                                    Label lblGenericControl = itemControl.FindControl(strPrefixLabel + strNameControl) as Label;
                                    if (lblGenericControl != null)
                                    {
                                        lblGenericControl.Text = itemControlXCarrera.Etiqueta;
                                    }
                                    else
                                    {
                                        HtmlGenericControl htmlGenericControl = itemControl.FindControl(strPrefixLabel + strNameControl) as HtmlGenericControl;
                                        if (htmlGenericControl != null)
                                        {
                                            htmlGenericControl.InnerText = itemControlXCarrera.Etiqueta;
                                        }
                                    }

                                    string strPrefixCheckBox = "chk";
                                    CheckBox chkBoxControl = itemControl.FindControl(strPrefixCheckBox + strNameControl) as CheckBox;
                                    if (chkBoxControl != null)
                                    {
                                        chkBoxControl.Text = itemControlXCarrera.Etiqueta;
                                    }

                                    string strPrefixCustomValidator = "cus";
                                    CustomValidator cusValidatorControl = itemControl.FindControl(strPrefixCustomValidator + strNameControl) as CustomValidator;
                                    if (cusValidatorControl != null)
                                    {
                                        cusValidatorControl.Text = itemControlXCarrera.EtiquetaRequerido;
                                        cusValidatorControl.Enabled = itemControlXCarrera.Requerido;
                                    }

                                    string strPrefixRequired = "req";
                                    RequiredFieldValidator reqGenericControl = itemControl.FindControl(strPrefixRequired + strNameControl) as RequiredFieldValidator;
                                    if (reqGenericControl != null)
                                    {
                                        reqGenericControl.Enabled = itemControlXCarrera.Requerido;
                                        reqGenericControl.ErrorMessage = itemControlXCarrera.EtiquetaRequerido;
                                    }

                                    string strPrefixRegularExpression = "rev";
                                    RegularExpressionValidator revGenericControl = itemControl.FindControl(strPrefixRegularExpression + strNameControl) as RegularExpressionValidator;
                                    if (revGenericControl != null)
                                    {
                                        revGenericControl.Enabled = itemControlXCarrera.RegularExpression;
                                        revGenericControl.ErrorMessage = itemControlXCarrera.RegularErrorMessage;
                                        revGenericControl.ValidationExpression = itemControlXCarrera.ValidationExpression ?? "";
                                    }

                                    Control controlDatePickerEdad = itemControl.FindControl("datePickerEdad");
                                    if (controlDatePickerEdad != null && itemControl.ID == "phDatePickerEdad")
                                    {
                                        Web.Controls.UserControls.DatePickerControl datePickerControl = controlDatePickerEdad as Web.Controls.UserControls.DatePickerControl;
                                        datePickerControl.Text = itemControlXCarrera.Etiqueta;
                                        datePickerControl.IsRequired = itemControlXCarrera.Requerido;
                                        datePickerControl.ErrorMessage = itemControlXCarrera.EtiquetaRequerido;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            base.OnInit(e);
        }

        private void LoadCarrera(int IdCarrera)
        {
            UserSession session = HttpSecurity.CurrentSession;
            CarreraBLL objCarreraBLL = new CarreraBLL(session);
            CarreraOBJ objCarrera = objCarreraBLL.SelectCarreraObject(IdCarrera);

            if (objCarrera != null)
            {
                divHtmlRender.InnerHtml = objCarrera.ContenidoHtml;

                RamaBLL objRamaBLL = new RamaBLL(session);

                IList<RamaOBJ> lstRamas = objRamaBLL.SelectRama(new RamaOBJ() {
                    IdCarrera = IdCarrera
                });

                rblRamas.DataSource = lstRamas;
                rblRamas.DataTextField = "Nombre";
                rblRamas.DataValueField = "IdRama";
                rblRamas.DataBind();

                LoadCategoriasRbl(IdCarrera);
                // LoadTipoEquipoDdl();

                LoadValoresFechas();

                LoadClasificaciones(IdCarrera);

                lblPoliticas.Text = objCarrera.DescripcionPoliticas;

                SetVisibilityTipoRegistro();    
            }
        }

        private void SetVisibilityTipoRegistro()
        {
            // En caso de que la carrera tenga un tipo de equipo entonces se debe habilitar la opcion
            // de  que se registre por equipo, de otra manera entonces permanecerá invisible.

            UserSession session = Tools.HttpSecurity.CurrentSession;
            TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
            IList<TipoEquipoOBJ> lstTipoEquipo = objTipoEquipoBLL.SelectTipoEquipo(new TipoEquipoOBJ() { IdCarrera = IdCarreraProperty });

            if (lstTipoEquipo.Count > 0)
            {
                phTipoRegistro.Visible = true;
            }
        }

        private void LoadClasificaciones(int idCarrera)
        {
            UserSession session = HttpSecurity.CurrentSession;
            ClasificacionBLL objClasificacionBLL = new ClasificacionBLL(session);
            ClasificacionOBJ clasificacion = new ClasificacionOBJ();
            clasificacion.IdCarrera = idCarrera;

            IList<ClasificacionOBJ> lstClasificaciones = objClasificacionBLL.SelectClasificacion(clasificacion);

            rptClasificacion.DataSource = lstClasificaciones;
            rptClasificacion.DataBind();
        }

        private void LoadTipoEquipoDdl(int IdCategoria)
        {
            UserSession session = HttpSecurity.CurrentSession;
            TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
            IList<TipoEquipoOBJ> lstTipoEquipo = objTipoEquipoBLL.SelectTipoEquipo(new TipoEquipoOBJ() { Activo = true, IdCategoria = IdCategoria }); // Todos los tipos de equipo
            ddlTipoEquipo.DataSource = lstTipoEquipo;
            ddlTipoEquipo.DataTextField = "CantidadParticipantes";
            ddlTipoEquipo.DataValueField = "IdTipoEquipo";
            ddlTipoEquipo.DataBind();

            int IdTipoEquipo;
            if (int.TryParse(ddlTipoEquipo.SelectedValue, out IdTipoEquipo))
            {
                LoadEmailParticipanteXEquipo(IdTipoEquipo);
            }
        }

        private void LoadCategoriasRbl(int IdCarrera)
        {
            UserSession session = HttpSecurity.CurrentSession;
            CategoriaBLL objCategoriaBLL = new CategoriaBLL(session);
            IList<CategoriaOBJ> lstCategorias = objCategoriaBLL.SelectCategoria(new CategoriaOBJ() { IdCarrera = IdCarrera });

            if (RegistroEnEquipo)
            {
                TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);

                foreach (CategoriaOBJ itemCategoria in lstCategorias)
                {
                    int numParticipantes = 0;
                    if(!string.IsNullOrEmpty(ddlTipoEquipo.SelectedValue))
                    {
                        int.TryParse(ddlTipoEquipo.SelectedItem.Text, out numParticipantes);
                    }
                    

                    IList<TipoEquipoOBJ> lstTipoEquipos = objTipoEquipoBLL.SelectTipoEquipo(
                        new TipoEquipoOBJ() { IdCategoria = itemCategoria.IdCategoria, CantidadParticipantes = numParticipantes });

                    if (lstTipoEquipos.Count == 1)
                    {
                        itemCategoria.Precio = lstTipoEquipos[0].Precio;
                    }
                    else
                    {
                        itemCategoria.Precio = 0;
                    }
                }
            }

            rblCategoria.DataSource = lstCategorias; // GetListConcatCategoriasConPrecio(lstCategorias);
            rblCategoria.DataTextField = "Nombre";
            rblCategoria.DataValueField = "IdCategoria";
            rblCategoria.DataBind();
        }

        private IList<CategoriaOBJ> GetListConcatCategoriasConPrecio(IList<CategoriaOBJ> lstCategoriasXCarrera)
        {
            IList<CategoriaOBJ> lstCategoriasResult = new List<CategoriaOBJ>();

            foreach (CategoriaOBJ itemCategoria in lstCategoriasXCarrera)
            {
                CategoriaOBJ objCategoria = new CategoriaOBJ();
                objCategoria.IdCategoria = itemCategoria.IdCategoria;
                objCategoria.Nombre = itemCategoria.Nombre + " $" + itemCategoria.Precio;
                lstCategoriasResult.Add(objCategoria);
            }

            return lstCategoriasResult;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
				InsertarParticipante();

                decimal Amount = 0;
                if (RegistroEnEquipo)
                {
                    Amount = GetPrecioEquipoXCategoria();
                }
                else
                {
                    ParticipanteXCarreraBLL objpxcbll = new ParticipanteXCarreraBLL(HttpSecurity.CurrentSession);
                    ParticipanteXCarreraOBJ objpxc = objpxcbll.SelectParticipanteXCarreraObject(IdParticipanteXCarreraProperty);

                    if (objpxc != null)
                    {
                        if (objpxc.IdCategoria.HasValue)
                        {
                            Amount = GetPrecioXCategoria(objpxc.IdCategoria.Value);
                        }
                    }
                }

				CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
				CarreraOBJ carreraOBJ = new CarreraOBJ();

				carreraOBJ = carreraBLL.SelectCarreraObject(IdCarreraProperty);

				//Si existe un correo de Paypal, ingresa al método de pago.
				//Si no, el pago es en efectivo u otro tipo de pago y no entra a Paypal.
				if(!string.IsNullOrEmpty(carreraOBJ.PayPalEmail))
				{
                    bool bPrimeroEnRegistrar = false;
                    UserSession session = HttpSecurity.CurrentSession;
                    EquipoBLL objEquipoBLL = new EquipoBLL(session);

                    EquipoOBJ objEquipoOBJ = objEquipoBLL.SelectEquipoObject(IdEquipoPropertyVS);
                    if (objEquipoOBJ != null)
                    {
                        if (objEquipoOBJ.CantidadRegistrados == 1)
                        {
                            bPrimeroEnRegistrar = true;
                        }
                    }

                    // Si es registro individual o si es el primero en registar el equipo entonces manda a la pantalla de paypal
                    if (!RegistroEnEquipo || bPrimeroEnRegistrar)
                    {
                        PaypalDelegateForm(Amount);
                    }
                    else
                    {
                        LimpiarCampos();
                        lblRuta.Visible = false; // To see how to load invisibility

                        //lblModalTitle.Text = "¡Registro con éxito!";
                        //lblModalBody.Text = "¡Gracias por registrarte!";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        //upModal.Update();


                        string urlToRedirect = Urls.PayPalPage();
                        Session.Add("SessionIdParticipanteXCarrera", IdParticipanteXCarreraProperty);

                        Response.Redirect(urlToRedirect, false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
				}
				else
				{
					LimpiarCampos();

					lblModalTitle.Text = "¡Registro con éxito!";
					lblModalBody.Text = "¡Gracias por registrarte!";
					ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
					upModal.Update();
				}
            }
            catch (Exception ex)
            {
                cusError.ErrorMessage = ex.Message;
                cusError.IsValid = false;
            }
        }

        private void LimpiarCampos()
        {
            txtNombres.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            // datePickerEdad.Text = string.Empty;
            txtDomicilio.Text = string.Empty;
            txtSocio.Text = string.Empty;
            txtNoAccion.Text = string.Empty;
            txtTelefonoPersonal.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefonoEmergencia.Text = string.Empty;
            rblCategoria.ClearSelection();
            rblRamas.ClearSelection();
            chkAcepto.Checked = false;
			txtFolioOffline.Text = string.Empty;
			rblRuta.Items.Clear();
			ddlDia.ClearSelection();
			ddlMes.ClearSelection();
			ddlAnio.ClearSelection();
		}

        private void validaParticipante(ParticipantesOBJ participanteOBJ)
        {
            if (participanteOBJ.FechaNacimiento < new DateTime(1900, 1, 1))
            {
                throw new Exception("La fecha de nacimiento tiene un formato incorrecto o debe ser mayor a 1/1/1900");
            }
        }

        private void InsertarParticipante()
        {
            try
            {
                DAL.DAL.BeginTransaction();

                UserSession session = HttpSecurity.CurrentSession;

                ParticipantesOBJ objParticipante = FillParticipanteOBJ();
                ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(session);

                int IdParticipante = objParticipanteBLL.InsertParticipante(objParticipante);

                InsertaParticipanteXCarrera(IdParticipante, IdCarreraProperty, objParticipante.Email);
                InsertarClasificacionXParticipante(IdParticipante);

                DAL.DAL.CommitTransaction();

                IdParticipanteVSProperty = objParticipante.IdParticipante;
			}
            catch (Exception ex)
            {                
                DAL.DAL.RollbackTransaction();
                throw new Exception("Hubo un error al guardar participante, detalle del error: " + ex.Message);
            }
        }

        private void InsertarClasificacionXParticipante(int IdParticipante)
        {
            UserSession session = Tools.HttpSecurity.CurrentSession;
            ClasificacionXParticipanteBLL objClasificacionXParticipanteBLL = new ClasificacionXParticipanteBLL(session);

            foreach (RepeaterItem item in rptClasificacion.Controls)
            {
                RadioButtonList rblClasificacionItem = item.FindControl("rblClasificacionItem") as RadioButtonList;
                if (rblClasificacionItem != null)
                {
                    int IdValorClasificacion;
                    if (int.TryParse(rblClasificacionItem.SelectedValue, out IdValorClasificacion))
                    {
                        ClasificacionXParticipanteOBJ objClasificacionXParticipante = new ClasificacionXParticipanteOBJ();
                        objClasificacionXParticipante.IdParticipante = IdParticipante;
                        objClasificacionXParticipante.IdValorClasificacion = IdValorClasificacion;

                        objClasificacionXParticipanteBLL.InsertClasificacionXParticipante(objClasificacionXParticipante);
                    }
                }
            }
        }

        private void InsertaParticipanteXCarrera(int IdParticipante, int IdCarrera, string Email)
        {
            UserSession session = HttpSecurity.CurrentSession;
            ParticipanteXCarreraBLL objPxCBLL = new ParticipanteXCarreraBLL(session);
            ParticipanteXCarreraOBJ objParticipanteXCarreraOBJ = new ParticipanteXCarreraOBJ();
            objParticipanteXCarreraOBJ.IdCarrera = IdCarrera;
            objParticipanteXCarreraOBJ.IdParticipante = IdParticipante;

            int IdRama;
            if(int.TryParse(rblRamas.SelectedValue, out IdRama))
            {
                objParticipanteXCarreraOBJ.IdRama = IdRama;
            }

            int IdCategoria;
            if (int.TryParse(rblCategoria.SelectedValue, out IdCategoria))
            {
                objParticipanteXCarreraOBJ.IdCategoria = IdCategoria;
            }

            int IdRuta;
			if (int.TryParse(rblRuta.SelectedValue, out IdRuta))
			{
				objParticipanteXCarreraOBJ.IdRuta = IdRuta;
			}

            EquipoBLL objEquipoBLL = new EquipoBLL(session);
            IList<EquipoOBJ> lstEquipos = objEquipoBLL.SelectEquipos(
                new EquipoOBJ() { IdCarrera = IdCarreraProperty, EmailsParticipantes = Email });

            int? IdEquipo = null;
            if (RegistroEnEquipo)
            {
                if (lstEquipos.Count == 0)
                {
                    // insertar nuevo equipo
                    EquipoOBJ nuevoEquipo = new EquipoOBJ();
                    nuevoEquipo.EmailsParticipantes = GetEmailsParticipantes();
                    nuevoEquipo.IdCarrera = IdCarreraProperty;
                    nuevoEquipo.CantidadRegistrados = 1;
                    nuevoEquipo.Nombre = txtNombreEquipo.Text.Trim();

                    int IdTipoEquipo;
                    if (int.TryParse(ddlTipoEquipo.SelectedValue, out IdTipoEquipo))
                    {
                        nuevoEquipo.IdTipoEquipo = IdTipoEquipo;
                    }

                    IdEquipo = objEquipoBLL.InsertEquipo(nuevoEquipo);
                }
            }

            if (lstEquipos.Count == 1)
            {
                IdEquipo = lstEquipos[0].IdEquipo;
                EquipoOBJ oldEquipo = lstEquipos[0];

                // comparar con el equipo registrado anteriormente
                TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
                TipoEquipoOBJ objTipoEquipo = objTipoEquipoBLL.SelectTipoEquipoObject(oldEquipo.IdTipoEquipo.Value);

                if (objTipoEquipo != null)
                {
                    if (oldEquipo.CantidadRegistrados >= objTipoEquipo.CantidadParticipantes)
                    {
                        throw new Exception("Se ha alcanzado el cupo máximo en el equipo");
                    }
                    else
                    {
                        oldEquipo.CantidadRegistrados += 1;
                    }
                }

                objEquipoBLL.UpdateEquipo(oldEquipo);
            }


			CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
			CarreraOBJ carreraOBJ = new CarreraOBJ();
			carreraOBJ = carreraBLL.SelectCarreraObject(IdCarreraProperty);

			if (carreraOBJ != null)
			{
				objParticipanteXCarreraOBJ.Folio = carreraOBJ.SiguienteFolio;
				carreraOBJ.SiguienteFolio++;
				carreraBLL.UpdateSiguienteFolio(carreraOBJ);
			}

			objParticipanteXCarreraOBJ.FechaRegistro = DateTime.Now;

			objParticipanteXCarreraOBJ.IdEquipo = IdEquipo;
            objPxCBLL.InsertParticipanteXCarrera(objParticipanteXCarreraOBJ);

            IdParticipanteXCarreraProperty = objParticipanteXCarreraOBJ.IdParticipanteXCarrera;

            if (IdEquipo.HasValue)
            {
                IdEquipoPropertyVS = IdEquipo.Value;
            }
        }


        public string GetEmailsParticipantes()
        {
            StringBuilder strBuilder = new StringBuilder();

            if (repeaterEmailParticipanteXEquipo != null && repeaterEmailParticipanteXEquipo.Items.Count > 0)
            {
                for(int i = 0; i < repeaterEmailParticipanteXEquipo.Items.Count; i++)
                {
                    RepeaterItem itemRepeater = repeaterEmailParticipanteXEquipo.Items[i];
                    TextBox txtEmailParticipanteXEquipo = itemRepeater.FindControl("txtEmailParticipanteXEquipo") as TextBox;

                    if (txtEmailParticipanteXEquipo != null)
                    {
                        strBuilder.Append(txtEmailParticipanteXEquipo.Text.Trim());
                    }

                    if (i + 1 < repeaterEmailParticipanteXEquipo.Items.Count)
                    {
                        strBuilder.Append(";");
                    }
                }
            }

            return strBuilder.ToString();
        }


        private ParticipantesOBJ FillParticipanteOBJ()
        {
            ParticipantesOBJ objParticipante = new ParticipantesOBJ();
            objParticipante.Nombre = txtNombres.Text.Trim();
            objParticipante.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
            objParticipante.ApellidoMaterno = txtApellidoMaterno.Text.Trim();

            DateTime dFechaNacimiento = DateTime.Now;
            string strFecha = ddlDia.SelectedValue + "/" + ddlMes.SelectedValue + "/" + ddlAnio.SelectedValue;

            if (!DateTime.TryParse(strFecha, out dFechaNacimiento))
            {
                throw new Exception("La fecha no tiene un formato válido");
            }

            objParticipante.FechaNacimiento = dFechaNacimiento;

            //CultureInfo ci = CultureInfo.CreateSpecificCulture("es-MX");

            //if (DateTime.TryParse(datePickerEdad.Value.Trim(), ci.DateTimeFormat,DateTimeStyles.None, out dFechaNacimiento))
            //{
            //    objParticipante.FechaNacimiento = dFechaNacimiento;
            //}

            objParticipante.Domicilio = txtDomicilio.Text.Trim();

            if (!string.IsNullOrEmpty(txtSocio.Text.Trim()))
            {
                objParticipante.Socio = txtSocio.Text.Trim();
            }

            objParticipante.Invitado = txtInvitado.Text.Trim();

            int iNumeroAccion;
            int.TryParse(txtNoAccion.Text.Trim(), out iNumeroAccion);
            objParticipante.NumeroAccion = iNumeroAccion;

            if (!string.IsNullOrEmpty(txtTelefonoPersonal.Text.Trim()))
            {
                objParticipante.Telefono = txtTelefonoPersonal.Text.Trim();
            }

            objParticipante.Email = txtEmail.Text.Trim();

            if (!string.IsNullOrEmpty(txtTelefonoEmergencia.Text.Trim()))
            {
                objParticipante.TelefonoEmergencia = txtTelefonoEmergencia.Text.Trim();
            }

            ParticipanteXCarreraOBJ objParticipanteXCarreraOBJ = new ParticipanteXCarreraOBJ();
            objParticipanteXCarreraOBJ.IdCarrera = IdCarreraProperty;
            objParticipanteXCarreraOBJ.IdParticipante = -1;

            int IdRama;
            int.TryParse(rblRamas.SelectedValue, out IdRama);

            int IdCategoria;
            int.TryParse(rblCategoria.SelectedValue, out IdCategoria);

            int? IdRuta = null;

            if (!string.IsNullOrEmpty(txtClub.Text.Trim()))
            {
                objParticipante.Club = txtClub.Text.Trim();
            }

            objParticipanteXCarreraOBJ.IdCategoria = IdCategoria;
            objParticipanteXCarreraOBJ.IdRama = IdRama;
            objParticipanteXCarreraOBJ.IdRuta = IdRuta;

            objParticipante.ParticipanteXCarrera = objParticipanteXCarreraOBJ;

			//Asigna el folio correspondiente

			CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
			CarreraOBJ carreraOBJ = new CarreraOBJ();

			carreraOBJ = carreraBLL.SelectCarreraObject(IdCarreraProperty);

			//if(carreraOBJ != null)
			//{
			//	objParticipanteXCarreraOBJ.Folio = carreraOBJ.SiguienteFolio;

			//	carreraOBJ.SiguienteFolio++;

			//	carreraBLL.UpdateSiguienteFolio(carreraOBJ);
			//}

			// Fill 10 generic fields
			objParticipante.Generic01 = !string.IsNullOrEmpty(txtGeneric01.Text.Trim()) ? txtGeneric01.Text.Trim() : null;
            objParticipante.Generic02 = !string.IsNullOrEmpty(txtGeneric02.Text.Trim()) ? txtGeneric02.Text.Trim() : null;
            objParticipante.Generic03 = !string.IsNullOrEmpty(txtGeneric03.Text.Trim()) ? txtGeneric03.Text.Trim() : null;
            objParticipante.Generic04 = !string.IsNullOrEmpty(txtGeneric04.Text.Trim()) ? txtGeneric04.Text.Trim() : null;
            objParticipante.Generic05 = !string.IsNullOrEmpty(txtGeneric05.Text.Trim()) ? txtGeneric05.Text.Trim() : null;
            objParticipante.Generic06 = !string.IsNullOrEmpty(txtGeneric06.Text.Trim()) ? txtGeneric06.Text.Trim() : null;
            objParticipante.Generic07 = !string.IsNullOrEmpty(txtGeneric07.Text.Trim()) ? txtGeneric07.Text.Trim() : null;
            objParticipante.Generic08 = !string.IsNullOrEmpty(txtGeneric08.Text.Trim()) ? txtGeneric08.Text.Trim() : null;
            objParticipante.Generic09 = !string.IsNullOrEmpty(txtGeneric09.Text.Trim()) ? txtGeneric09.Text.Trim() : null;
            objParticipante.Generic10 = !string.IsNullOrEmpty(txtGeneric10.Text.Trim()) ? txtGeneric10.Text.Trim() : null;

			objParticipante.FechaRegistro = DateTime.Now;

			if (string.IsNullOrWhiteSpace(carreraOBJ.PayPalEmail))
			{
				objParticipanteXCarreraOBJ.FolioOffline = txtFolioOffline.Text;
			}

            return objParticipante;
        }

        [System.Web.Services.WebMethod]
        public static string WSGetPrecioCategoria(int IdCategoria)
        {
            CategoriaBLL objCategoriaBLL = new CategoriaBLL(HttpSecurity.CurrentSession);
            CategoriaOBJ objCategoriaOBJ = objCategoriaBLL.SelectCategoriaObject(IdCategoria);

            if (objCategoriaOBJ != null)
                return objCategoriaOBJ.Precio.ToString();

            return "";
        }

        public decimal GetPrecioXEquipo(int IdTipoEquipo)
        {
            TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(HttpSecurity.CurrentSession);
            TipoEquipoOBJ objTipoEquipo = objTipoEquipoBLL.SelectTipoEquipoObject(IdTipoEquipo);

            if (objTipoEquipo != null)
            {
                return objTipoEquipo.Precio;
            }

            return 0;
        }

        private void EliminaVariablesDeSession()
        {
            Session.Remove("lstParticipantesCache");
            Session.Remove("IdEquipo");
            Session.Remove("IdTipoEquipo");
            Session.Remove("objSessionPayPal");
            Session.Remove("IdEquipo");
        }

        private decimal GetPrecioXCategoria(int IdCategoria)
        {
            CategoriaBLL objCategoriaBLL = new CategoriaBLL(HttpSecurity.CurrentSession);
            CategoriaOBJ objCategoriaOBJ = objCategoriaBLL.SelectCategoriaObject(IdCategoria);
            if (objCategoriaOBJ != null)
            {
                return objCategoriaOBJ.Precio;
            }

            return 0;
        }

        private void PaypalDelegateForm(decimal p_Amount)
        {
            string strCustom = string.Empty;

            if (RegistroEnEquipo)
            {
                strCustom = "IdEquipo={0}";
                if (Session["IdEquipo"] != null)
                {
                    strCustom = string.Format(strCustom, Session["IdEquipo"]);
                }
            }
            else
            {
                strCustom = "IdParticipante={0}";
                strCustom = string.Format(strCustom, IdParticipanteVSProperty);
            }

            // Urls.Abs("~/Pages/PaymentProcess.aspx")
            //string strURLReturn = "http://localhost:61880/WebApplicationTemplate/PublicPages/PaymentProcess.aspx?IdCarrera={0}&IdParticipante={1}";
            string strURLReturn = Urls.Abs("~/PublicPages/PayPalRestAPI.aspx?IdCarrera={0}&IdParticipante={1}&IdEquipo={2}");

            strURLReturn = string.Format(strURLReturn, IdCarreraProperty, IdParticipanteVSProperty, IdEquipoPropertyVS);

            // Urls.Abs("~/Pages/RegistroParticipantes.aspx")
            // string strCancelURL = "http://localhost:61880/WebApplicationTemplate/PublicPages/RegistroParticipantes.aspx?IdCarrera={0}";

            /* Se borra esta linea para obtener la CancelURL desde la URL de 
             * Registro configurada en la tabla Carrera en la BD
             * 
             * string strCancelURL = Urls.Abs("~/PublicPages/RegistroParticipantes.aspx?IdCarrera={0}");
             * 
             * by Erik C
             */

            string strCancelURL = string.Empty;

            CarreraBLL objCarerraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
            CarreraOBJ objCarrera = objCarerraBLL.SelectCarreraObject(IdCarreraProperty);

            string strNombreCarrera = string.Empty;
            if (objCarrera != null)
            {
                strNombreCarrera = objCarrera.Nombre;
                strCancelURL = objCarrera.URLRegistro; //Se obtiene desde la BD, la URL de registro ya trae el IdCarrera o la URL que enmascara e identifica la carrera
            }
			Session.Remove("objSessionPayPal");
			SessionPayPal objSessionPayPal = new SessionPayPal()
            {
                IdCarrera = IdCarreraProperty
                , amount = p_Amount
                , custom = strCustom
                , item_name = strNombreCarrera
                , returnURL = strURLReturn
                , cancelURL = strCancelURL
				, IdParticipante = this.IdParticipanteVSProperty
            };

            Session.Add("objSessionPayPal", objSessionPayPal);

            string url = Urls.PayPalPage();
            
            // string s = "window.open('" + url + "', 'popup_window', 'width=500,height=800,left=100,top=100,resizable=yes');";
            // ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        

        private void UpdatePayment(string customVariable)
        {
            if (customVariable.Contains("IdParticipante"))
            {
                string strIdParticipante = customVariable.Replace("IdParticipante%3D", "");
                int IdParticipante;
                if (int.TryParse(strIdParticipante, out IdParticipante))
                {
                    ParticipanteXCarreraBLL objParticipanteXCarreraBLL = new ParticipanteXCarreraBLL(HttpSecurity.CurrentSession);
                    ParticipanteXCarreraOBJ objParticipanteXCarrera = objParticipanteXCarreraBLL.SelectParticipanteXCarreraByIdParticipante(IdParticipante);
                    if (objParticipanteXCarrera != null)
                    {
						//objParticipante.Pagado = true; Se elimina el campo Pagado by ECM T#14 
						objParticipanteXCarreraBLL.UpdateInfoPagoParticipante(objParticipanteXCarrera);
                    }
                }
            }
            else if(customVariable.Contains("IdEquipo"))
            {
                string strIdEquipo = customVariable.Replace("IdEquipo%3D", "");
                int IdEquipo;
                if (int.TryParse(strIdEquipo, out IdEquipo))
                {
                    ParticipanteXCarreraBLL objParticipanteXCarreraBLL = new ParticipanteXCarreraBLL(HttpSecurity.CurrentSession);
                    IList<ParticipanteXCarreraOBJ> lstParticipantes = objParticipanteXCarreraBLL.SelectParticipante(new ParticipanteXCarreraOBJ() { IdEquipo = IdEquipo });

                    foreach (ParticipanteXCarreraOBJ objParticipante in lstParticipantes)
                    {
                        //objParticipante.Pagado = true; Se elimina el campo Pagado by ECM T#14
                        objParticipanteXCarreraBLL.UpdateInfoPagoParticipante(objParticipante);
                    }
                }
            }
        }



        private void GuardarRegistroParticipante()
        {
            List<ParticipantesOBJ> lstParticipantes = new List<ParticipantesOBJ>();
            if (Session["lstParticipantesCache"] != null)
            {
                lstParticipantes = (List<ParticipantesOBJ>)Session["lstParticipantesCache"];
            }

            ParticipantesOBJ objParticipantesOBJ = FillParticipanteOBJ();
            lstParticipantes.Add(objParticipantesOBJ);

            lblNumParticipante.Text = "" + (lstParticipantes.Count + 1);


            //if (RegistroEnEquipo)
            //{
            //    ViewState.Add("IdCategoria", rblCategoriaSelectedValue);
            //    ViewState.Add("IdRama", rblRamas.SelectedValue);

            //    if (ViewState["IdCategoria"] != null)
            //    {
            //        int IdCategoria;
            //        if (int.TryParse(ViewState["IdCategoria"].ToString(), out IdCategoria))
            //        {
            //            rblCategoria.SelectedValue = ViewState["IdCategoria"].ToString();
            //        }
            //    }

            //    if (ViewState["IdRama"] != null)
            //    {
            //        int IdRama;
            //        if (int.TryParse(ViewState["IdRama"].ToString(), out IdRama))
            //        {
            //            rblRamas.SelectedValue = ViewState["IdRama"].ToString();
            //        }
            //    }
            //}
            rblTipoRegistro.Enabled = false; // una vez guardado un registro de un participante de equipo
                                             // no podrá cambiar a registro individual

            LimpiarCampos();

            Session.Add("lstParticipantesCache", lstParticipantes);

            ddlTipoEquipo.Enabled = false; // una vez comenzada la seleccion no se debe de poder seleccionar una cantidad diferente
            if (Session["IdTipoEquipo"] == null)
            {
                Session.Add("IdTipoEquipo", ddlTipoEquipo.SelectedValue);
            }
        }

        protected void ddlTipoEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdTipoEquipo;
            if (int.TryParse(ddlTipoEquipo.SelectedValue, out IdTipoEquipo))
            {
                LoadEmailParticipanteXEquipo(IdTipoEquipo);
            }
        }

        protected void rblCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdCategoria;
            int.TryParse(rblCategoria.SelectedValue, out IdCategoria);

            rblTipoRegistro.SelectedValue = "" +  (char)ETipoRegistro.Individual;
            phTipoEquipo.Visible = false;

            if (IdCategoria > 0)
            {
                LoadTipoEquipoDdl(IdCategoria);

                if (ddlTipoEquipo.Items.Count > 0)
                {
                    rblTipoRegistro.Enabled = true;
                }
                else
                {
                    rblTipoRegistro.Enabled = false;
                }

                if (!RegistroEnEquipo)
                {
                    lblTotal.InnerText = "" + GetPrecioXCategoria(IdCategoria);
                    CargarRutasByIdCategoria(IdCategoria);
                }
                else
                {
                    lblTotal.InnerText = "" + GetPrecioEquipoXCategoria();
                }
            }
        }

        private decimal GetPrecioEquipoXCategoria()
        {
            UserSession session = HttpSecurity.CurrentSession;

            int IdTipoEquipo;
            if (int.TryParse(ddlTipoEquipo.SelectedValue, out IdTipoEquipo))
            {
                TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
                IList<TipoEquipoOBJ> lstTiposEquipo = objTipoEquipoBLL.SelectTipoEquipo(
                    new TipoEquipoOBJ() { IdTipoEquipo = IdTipoEquipo, IdCategoria = IdCategoriaProperty, Activo = true });

                if (lstTiposEquipo.Count == 1)
                {
                    return lstTiposEquipo[0].Precio;
                }
            }

            return 0;
        }

        private void LoadValoresFechas()
        {
            List<ListItem> lstItemsDias = new List<ListItem>();
            for (int i = 1; i <= 31; i++)
            {
                // text, value
                lstItemsDias.Add(new ListItem(i.ToString("00"), i.ToString("00")));
            }

            ddlDia.Items.AddRange(lstItemsDias.ToArray());

            List<ListItem> lstItemsMeses = new List<ListItem>();
            for (int i = 1; i <= 12; i++)
            {
                // text, value
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i);
                lstItemsMeses.Add(new ListItem(monthName, i.ToString("00")));
            }
            ddlMes.Items.AddRange(lstItemsMeses.ToArray());

            List<ListItem> lstItemsAnios = new List<ListItem>();
            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 100; i--)
            {
                // text, value
                lstItemsAnios.Add(new ListItem(i.ToString(), i.ToString()));
            }

            ddlAnio.Items.AddRange(lstItemsAnios.ToArray());
        }

        protected void rptClasificacion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ClasificacionOBJ objClasificacion = e.Item.DataItem as ClasificacionOBJ;

                if (objClasificacion != null)
                {
                    HtmlGenericControl lblNombreClasificacion = e.Item.FindControl("lblNombreClasificacion") as HtmlGenericControl;
                    if (lblNombreClasificacion != null)
                    {
                        lblNombreClasificacion.InnerText = objClasificacion.Nombre;
                    }

                    RadioButtonList rblClasificacionItem = e.Item.FindControl("rblClasificacionItem") as RadioButtonList;
                    if (rblClasificacionItem != null)
                    {
                        UserSession session = Tools.HttpSecurity.CurrentSession;
                        ValorClasificacionBLL objClasificacionBLL = new ValorClasificacionBLL(session);
                        IList<ValorClasificacionOBJ> lstValorClasificacion = objClasificacionBLL.SelectValorClasificacion(new ValorClasificacionOBJ() { IdClasificacion = objClasificacion.IdClasificacion });
                        rblClasificacionItem.DataSource = lstValorClasificacion;
                        rblClasificacionItem.DataTextField = "Etiqueta";
                        rblClasificacionItem.DataValueField = "IdValorClasificacion";
                        if (lstValorClasificacion.Count > 0) // Para seleccionar el primer elemento
                        {
                            rblClasificacionItem.SelectedIndex = 0;
                        }

                        rblClasificacionItem.DataBind();
                    }
                }
            }
        }

		/// <summary>
		/// Revisa el modo de pago (Paypal o Offline), y oculta o muestra el txtFolioOffline.
		/// </summary>
		private void PagoOffline()
		{
			CarreraBLL carreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
			CarreraOBJ carreraOBJ = new CarreraOBJ();
			carreraOBJ = carreraBLL.SelectCarreraObject(IdCarreraProperty);

			if(carreraOBJ != null)
			{
				if (!string.IsNullOrWhiteSpace(carreraOBJ.PayPalEmail))
				{
					phFolioOffline.Visible = false;
				}
				else
				{
					phFolioOffline.Visible = true;
				}
			}
		}

		private void CargarRutasByIdCategoria(int idCategoria)
		{
			RutaBLL rutaBLL = new RutaBLL(HttpSecurity.CurrentSession);
			IList<RutaOBJ> lstRutas;

            RutaOBJ ruta = new RutaOBJ();
            ruta.IdCategoria = idCategoria;

			lstRutas = rutaBLL.SeleccionarRutasByIdCategoria(ruta);

			// lblRuta.Visible = false;

		    if(lstRutas != null && lstRutas.Count > 0)
			{
                lblRuta.Visible = true;
                phRuta.Visible = true;
                rblRuta.DataSource = lstRutas;
				rblRuta.DataTextField = "Nombre";
				rblRuta.DataValueField = "IdRuta";
				rblRuta.DataBind();
			}
			else
			{
				phRuta.Visible = false;
			}
		}

        private void LoadEmailParticipanteXEquipo(int IdTipoEquipo)
        {
            UserSession session = new UserSession();
            TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
            TipoEquipoOBJ objTipoEquipo = objTipoEquipoBLL.SelectTipoEquipoObject(IdTipoEquipo);

            if (objTipoEquipo != null)
            {
                int cantidad = objTipoEquipo.CantidadParticipantes;

                Dictionary<string, int> dictionaryData = new Dictionary<string, int>();

                for (int i = 0; i < cantidad; i++)
                {
                    dictionaryData.Add("Correo participante # " + (i + 1), i + 1);
                }

                repeaterEmailParticipanteXEquipo.Visible = true;
                repeaterEmailParticipanteXEquipo.DataSource = dictionaryData;
                repeaterEmailParticipanteXEquipo.DataBind();

                if (repeaterEmailParticipanteXEquipo.Items.Count > 0)
                {
                    TextBox txtEmailParticipanteXEquipo = repeaterEmailParticipanteXEquipo.Items[0].FindControl("txtEmailParticipanteXEquipo") as TextBox;
                    if (txtEmailParticipanteXEquipo != null)
                    {
                        txtEmailParticipanteXEquipo.Enabled = false;
                        txtEmailParticipanteXEquipo.Text = txtEmail.Text;
                    }
                }
            }
        }

        protected void rblTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsNullOrEmptyEmailParticipante())
            {
                rblTipoRegistro.SelectedValue = "I";
                cusErrorEquipo.ErrorMessage = "Debe indicar un correo antes de seleccionar el equipo";
                cusErrorEquipo.IsValid = false;
                return;
            }

            if (rblTipoRegistro.SelectedValue == "I")
            {
                phTipoEquipo.Visible = false;
                repeaterEmailParticipanteXEquipo.DataSource = null;
                repeaterEmailParticipanteXEquipo.DataBind();

                lblTotal.InnerText = "" + GetPrecioXCategoria(IdCategoriaProperty);
                CargarRutasByIdCategoria(IdCategoriaProperty);
            }
            else if (rblTipoRegistro.SelectedValue == "E")
            {
                phTipoEquipo.Visible = true;
                if (IdCategoriaProperty > 0)
                {
                    LoadTipoEquipoDdl(IdCategoriaProperty);
                }

                lblTotal.InnerText = "" + GetPrecioEquipoXCategoria();
            }
        }

        private bool IsNullOrEmptyEmailParticipante()
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                return true;
            }

            return false;
        }

        protected void txtNombreEquipo_TextChanged(object sender, EventArgs e)
        {
            TextBox txtNombreEquipo = sender as TextBox;

            if (!EsValidoNombreEquipo(txtNombreEquipo.Text.Trim(), IdCarreraProperty))
            {
                cusNombreEquipo.IsValid = false;
                cusNombreEquipo.ErrorMessage = "El nombre de equipo ya se encuentra registrado";
            }
        }

        private bool EsValidoNombreEquipo(string NombreEquipo, int IdCarrera)
        {
            UserSession session = Tools.HttpSecurity.CurrentSession;
            EquipoBLL objEquipoBLL = new EquipoBLL(session);

            IList<EquipoOBJ> lstEquipos = objEquipoBLL.SelectEquipos(new EquipoOBJ() { Nombre = NombreEquipo, IdCarrera = IdCarrera });

            if (lstEquipos.Count == 0)
                return true;

            return false;
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string strEmail = txtEmail.Text.Trim();

            if (!string.IsNullOrEmpty(strEmail))
            {
                LoadInformationEquipoByEmail(strEmail);
            }
        }

        private void LoadInformationEquipoByEmail(string emailParticipante)
        {
            UserSession session = Tools.HttpSecurity.CurrentSession;
            EquipoBLL objEquipoBLL = new EquipoBLL(session);

            IList<EquipoOBJ> lstEquipos = objEquipoBLL.SelectEquipos(
            new EquipoOBJ() { IdCarrera = IdCarreraProperty, EmailsParticipantes = emailParticipante });

            if (lstEquipos.Count == 1)
            {
                EquipoOBJ equipoOBJ = lstEquipos[0];

                // Se encontro el equipo al que pertenece el email.
                ParticipanteXCarreraBLL objParticipanteXCarreraBLL = new ParticipanteXCarreraBLL(session);
                IList<ParticipanteXCarreraOBJ> lstParticipanteXCarrera = objParticipanteXCarreraBLL.SelectParticipanteXCarrera(
                    new ParticipanteXCarreraOBJ() { Email = emailParticipante, IdCarrera = IdCarreraProperty, IdEquipo = equipoOBJ.IdEquipo });

                if (lstParticipanteXCarrera.Count == 0)
                {
                    // El participante aun no se encuentra registrado
                    TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
                    TipoEquipoOBJ objTipoEquipo = objTipoEquipoBLL.SelectTipoEquipoObject(equipoOBJ.IdTipoEquipo.Value);

                    phTipoEquipo.Visible = true;
                    divTipoEquipo.Visible = false;
                    txtNombreEquipo.Text = equipoOBJ.Nombre;
                    txtNombreEquipo.Enabled = false;

                    BloqueaCategoriaRbl(objTipoEquipo.IdCategoria);
                    BloqueaTipoRegistroRbl(ETipoRegistro.Equipo);
                    CargarRutasByIdCategoria(objTipoEquipo.IdCategoria);
                }
            }
        }

    }
}