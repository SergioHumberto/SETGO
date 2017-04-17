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
            get { return ddlTipoRegistro.SelectedValue == "Equipo"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EliminaVariablesDeSession();
                if (IdCarreraProperty > 0)
                {
                    LoadCarrera(IdCarreraProperty);
                }
                else
                {
                    cusError.ErrorMessage = "No ha seleccionado una carrera";
                    cusError.IsValid = false;
                    btnEnviar.Visible = false;
                }
            }
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
                LoadTipoEquipoDdl();

                lblPoliticas.Text = objCarrera.DescripcionPoliticas;
            }
        }

        private void LoadTipoEquipoDdl()
        {
            UserSession session = HttpSecurity.CurrentSession;
            TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
            IList<TipoEquipoOBJ> lstTipoEquipo = objTipoEquipoBLL.SelectTipoEquipo(new TipoEquipoOBJ() { }); // Todos los tipos de equipo
            ddlTipoEquipo.DataSource = lstTipoEquipo;
            ddlTipoEquipo.DataTextField = "CantidadParticipantes";
            ddlTipoEquipo.DataValueField = "IdTipoEquipo";
            ddlTipoEquipo.DataBind();
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
            
            rblCategoria.DataSource = GetListConcatCategoriasConPrecio(lstCategorias);
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


        public bool EstaEquipoCompleto()
        {
            if (Session["lstParticipantesCache"] != null)
            {
                int cantidadEquipo;
                int.TryParse(ddlTipoEquipo.SelectedItem.Text, out cantidadEquipo);

                List<ParticipantesOBJ> lstParticipantes = (List<ParticipantesOBJ>)Session["lstParticipantesCache"];

                if (cantidadEquipo == lstParticipantes.Count)
                {
                    return true;
                }
            }

            return false;
        }

        private bool EsUltimoParticipanteEnEquipo()
        {
            if (Session["lstParticipantesCache"] != null)
            {
                int cantidadEquipo;
                int.TryParse(ddlTipoEquipo.SelectedItem.Text, out cantidadEquipo);

                List<ParticipantesOBJ> lstParticipantes = (List<ParticipantesOBJ>)Session["lstParticipantesCache"];

                if (lstParticipantes.Count == cantidadEquipo)
                {
                    return true;
                }
            }

            return false;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (RegistroEnEquipo)
            {
                FillRamaCategoriaRutaDeEquipo();
            }

            if (!RegistroEnEquipo || EstaEquipoCompleto())
            {
                try
                {
                    if (!RegistroEnEquipo)
                    {
                        InsertarParticipante();
                    }
                    else
                    {
                        InsertarEquipo();
                    }

                    // lblMessage.Text = "Se guardó la informacion del participante";
                    // LimpiarCampos();    
                    decimal Amount = 0;
                    if (RegistroEnEquipo)
                    {
                        Amount = GetPrecioEquipoXCategoria();
                        //int IdTipoEquipo;
                        //if (Session["IdTipoEquipo"] != null)
                        //{
                        //    int.TryParse(Session["IdTipoEquipo"].ToString(), out IdTipoEquipo);
                        //    Amount = GetPrecioXEquipo(IdTipoEquipo);
                        //}
                    }
                    else
                    {
                        ParticipanteXCarreraBLL objpxcbll = new ParticipanteXCarreraBLL(HttpSecurity.CurrentSession);
                        ParticipanteXCarreraOBJ objpxc = objpxcbll.SelectParticipanteXCarrera(IdParticipanteXCarreraProperty);

                        if (objpxc != null)
                        {
                            if (objpxc.IdCategoria.HasValue)
                            {
                                Amount = GetPrecioXCategoria(objpxc.IdCategoria.Value);
                            }
                        }
                    }

                    PaypalDelegateForm(Amount);
                }
                catch (Exception ex)
                {
                    cusError.ErrorMessage = ex.Message;
                    cusError.IsValid = false;
                }
            }
        }

        private void FillRamaCategoriaRutaDeEquipo()
        {
            int IdRama;
            int.TryParse(rblRamas.SelectedValue, out IdRama);

            int IdCategoria;
            int.TryParse(rblCategoria.SelectedValue, out IdCategoria);

            int? IdRuta = null;

            List<ParticipantesOBJ> lstParticipantes = new List<ParticipantesOBJ>();

            if (Session["lstParticipantesCache"] != null)
            {
                lstParticipantes = (List<ParticipantesOBJ>)Session["lstParticipantesCache"];

                foreach (ParticipantesOBJ objParticipante in lstParticipantes)
                {
                    objParticipante.ParticipanteXCarrera.IdRama = IdRama;
                    objParticipante.ParticipanteXCarrera.IdCategoria = IdCategoria;
                    objParticipante.ParticipanteXCarrera.IdRuta = IdRuta;
                }

                Session.Add("lstParticipantesCache", lstParticipantes);
            }
        }

        private void LimpiarCampos()
        {
            txtNombres.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtDomicilio.Text = string.Empty;
            txtSocio.Text = string.Empty;
            txtNoAccion.Text = string.Empty;
            txtTelefonoPersonal.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefonoEmergencia.Text = string.Empty;
            rblCategoria.ClearSelection();
            rblRamas.ClearSelection();
            chkAcepto.Checked = false;
        }

        private void InsertarEquipo()
        {
            try
            {
                UserSession session = HttpSecurity.CurrentSession;

                if (Session["lstParticipantesCache"] != null)
                {
                    List<ParticipantesOBJ> lstParticipantes = (List<ParticipantesOBJ>)Session["lstParticipantesCache"];

                    DAL.DAL.BeginTransaction();

                    EquipoBLL objEquipoBLL = new EquipoBLL(session);

                    int IdTipoEquipo = -1;
                    if (Session["IdTipoEquipo"] != null)
                    {
                        int.TryParse(Session["IdTipoEquipo"].ToString(), out IdTipoEquipo);
                    }

                    int IdEquipo = objEquipoBLL.InsertEquipo(new EquipoOBJ() { IdTipoEquipo = IdTipoEquipo });
                    Session.Add("IdEquipo", IdEquipo);

                    foreach (ParticipantesOBJ objParticipante in lstParticipantes)
                    {
                        ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(session);
                        objParticipante.IdEquipo = IdEquipo;
                        objParticipanteBLL.InsertParticipanteConCarrera(objParticipante);
                    }
                    DAL.DAL.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                DAL.DAL.RollbackTransaction();
                throw new Exception("Hubo un error al registrar equipo");
            }
        }

        private void InsertarParticipante()
        {
            try
            {
                UserSession session = HttpSecurity.CurrentSession;
                ParticipantesOBJ objParticipante = FillParticipanteOBJ();
                ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(session);

                DAL.DAL.BeginTransaction();
                objParticipanteBLL.InsertParticipante(objParticipante);

                InsertaParticipanteXCarrera(objParticipante.IdParticipante, IdCarreraProperty);
                DAL.DAL.CommitTransaction();

                IdParticipanteVSProperty = objParticipante.IdParticipante;
            }
            catch (Exception ex)
            {
                DAL.DAL.RollbackTransaction();
                throw new Exception("Hubo un error al guardar participante");
            }
        }

        private void InsertaParticipanteXCarrera(int IdParticipante, int IdCarrera)
        {
            UserSession session = HttpSecurity.CurrentSession;
            ParticipanteXCarreraBLL objPxCBLL = new ParticipanteXCarreraBLL(session);
            ParticipanteXCarreraOBJ objParticipanteXCarreraOBJ = new ParticipanteXCarreraOBJ();
            objParticipanteXCarreraOBJ.IdCarrera = IdCarrera;
            objParticipanteXCarreraOBJ.IdParticipante = IdParticipante;

            int IdRama;
            int.TryParse(rblRamas.SelectedValue, out IdRama);

            int IdCategoria;
            int.TryParse(rblCategoria.SelectedValue, out IdCategoria);

            int? IdRuta = null;

            objParticipanteXCarreraOBJ.IdCategoria = IdCategoria;
            objParticipanteXCarreraOBJ.IdRama = IdRama;
            objParticipanteXCarreraOBJ.IdRuta = IdRuta;

            objPxCBLL.InsertParticipanteXCarrera(objParticipanteXCarreraOBJ);

            IdParticipanteXCarreraProperty = objParticipanteXCarreraOBJ.IdParticipanteXCarrera;
        }

        private ParticipantesOBJ FillParticipanteOBJ()
        {
            ParticipantesOBJ objParticipante = new ParticipantesOBJ();
            objParticipante.Nombre = txtNombres.Text.Trim();
            objParticipante.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
            objParticipante.ApellidoMaterno = txtApellidoMaterno.Text.Trim();

            int iEdad;
            int.TryParse(txtEdad.Text.Trim(), out iEdad);
            objParticipante.Edad = iEdad;

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

            objParticipanteXCarreraOBJ.IdCategoria = IdCategoria;
            objParticipanteXCarreraOBJ.IdRama = IdRama;
            objParticipanteXCarreraOBJ.IdRuta = IdRuta;

            objParticipante.ParticipanteXCarrera = objParticipanteXCarreraOBJ;

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

        protected void ddlTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!RegistroEnEquipo) // individual
            {
                divNumParticipante.Visible = false;
                lblNumParticipante.Text = "1"; // Cuando seleccione otra vez individual entonces se pierden los registros gruardados de equipos
                divTipoEquipo.Visible = false;

                if (ddlTipoEquipo.Items.Count > 0)
                {
                    ddlTipoEquipo.SelectedIndex = 0;
                }

                EliminaVariablesDeSession();

                btnGuardarParticipante.Visible = false;
                btnEnviar.Visible = true;
                phRamaCategoriaRuta.Visible = true;
            }
            else // equipo
            {
                divNumParticipante.Visible = true;
                lblNumParticipante.Text = "1";
                divTipoEquipo.Visible = true;

                phRamaCategoriaRuta.Visible = false;
                btnGuardarParticipante.Visible = true;
                btnEnviar.Visible = false;
            }

            lblTotal.InnerText = "0.00";
            LoadCategoriasRbl(IdCarreraProperty);
        }

        private void EliminaVariablesDeSession()
        {
            Session.Remove("lstParticipantesCache");
            Session.Remove("IdEquipo");
            Session.Remove("IdTipoEquipo");
            Session.Remove("objSessionPayPal");
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
            string strURLReturn = "http://localhost:61880/WebApplicationTemplate/PublicPages/PaymentProcess.aspx?IdCarrera={0}&IdParticipante={1}";
            strURLReturn = string.Format(strURLReturn, IdCarreraProperty, IdParticipanteVSProperty);

            // Urls.Abs("~/Pages/RegistroParticipantes.aspx")
            string strCancelURL = "http://localhost:61880/WebApplicationTemplate/PublicPages/RegistroParticipantes.aspx?IdCarrera={0}";
            strCancelURL = string.Format(strCancelURL, IdCarreraProperty);

            CarreraBLL objCarerraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
            CarreraOBJ objCarrera = objCarerraBLL.SelectCarreraObject(IdCarreraProperty);

            string strNombreCarrera = string.Empty;
            if (objCarrera != null)
            {
                strNombreCarrera = objCarrera.Nombre;
            }

            SessionPayPal objSessionPayPal = new SessionPayPal()
            {
                IdCarrera = IdCarreraProperty
                , amount = p_Amount
                , custom = strCustom
                , item_name = strNombreCarrera
                , returnURL = strURLReturn
                , cancelURL = strCancelURL
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
                    ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(HttpSecurity.CurrentSession);
                    ParticipantesOBJ objParticipante = objParticipanteBLL.SelectParticipanteObject(IdParticipante);
                    if (objParticipante != null)
                    {
                        objParticipante.Pagado = true;
                        objParticipanteBLL.UpdateParticipante(objParticipante);
                    }
                }
            }
            else if(customVariable.Contains("IdEquipo"))
            {
                string strIdEquipo = customVariable.Replace("IdEquipo%3D", "");
                int IdEquipo;
                if (int.TryParse(strIdEquipo, out IdEquipo))
                {
                    ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(HttpSecurity.CurrentSession);
                    IList<ParticipantesOBJ> lstParticipantes = objParticipanteBLL.SelectParticipante(new ParticipantesOBJ() { IdEquipo = IdEquipo });

                    foreach (ParticipantesOBJ objParticipante in lstParticipantes)
                    {
                        objParticipante.Pagado = true;
                        objParticipanteBLL.UpdateParticipante(objParticipante);
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
            ddlTipoRegistro.Enabled = false; // una vez guardado un registro de un participante de equipo
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
            
        }

        protected void btnGuardarParticipante_Click(object sender, EventArgs e)
        {
            GuardarRegistroParticipante();
            if (EsUltimoParticipanteEnEquipo()) 
            {
                btnEnviar.Visible = true;
                btnGuardarParticipante.Visible = false;
                phInformacionPersonal.Visible = false;
                phRamaCategoriaRuta.Visible = true;
                LoadCategoriasRbl(IdCarreraProperty);
            }
        }

        protected void rblCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!RegistroEnEquipo)
            {
                int IdCategoria;
                if (int.TryParse(rblCategoria.SelectedValue, out IdCategoria))
                {
                    lblTotal.InnerText = "" + GetPrecioXCategoria(IdCategoria);
                }
            }
            else
            {
                lblTotal.InnerText = "" + GetPrecioEquipoXCategoria();
            }
        }

        private decimal GetPrecioEquipoXCategoria()
        {
            UserSession session = HttpSecurity.CurrentSession;

            int IdTipoEquipo;
            if (int.TryParse(ddlTipoEquipo.SelectedValue, out IdTipoEquipo))
            {
                int IdCategoria;
                if (int.TryParse(rblCategoria.SelectedValue, out IdCategoria))
                {
                    TipoEquipoBLL objTipoEquipoBLL = new TipoEquipoBLL(session);
                    IList<TipoEquipoOBJ> lstTiposEquipo = objTipoEquipoBLL.SelectTipoEquipo(
                        new TipoEquipoOBJ() { IdTipoEquipo = IdTipoEquipo, IdCategoria = IdCategoria });

                    if (lstTiposEquipo.Count == 1)
                    {
                        return lstTiposEquipo[0].Precio;
                    }
                }
            }

            return 0;
        }

    }
}