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

        public string URLWSGetPrecioCategoria
        {
            get
            {
                string URL = Urls.Abs("~/Pages/RegistroParticipantes.aspx/WSGetPrecioCategoria");
                return URL;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IdCarreraProperty > 0)
                {
                    LoadCarrera(IdCarreraProperty);
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

                CategoriaBLL objCategoriaBLL = new CategoriaBLL(session);
                IList<CategoriaOBJ> lstCategorias = objCategoriaBLL.SelectCategoria(new CategoriaOBJ() { IdCarrera = IdCarrera });

                rblCarrera.DataSource = GetListConcatCategoriasConPrecio(lstCategorias);
                rblCarrera.DataTextField = "Nombre";
                rblCarrera.DataValueField = "IdCategoria";
                rblCarrera.DataBind();
            }
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
                lblMessage.Text = "Se guardó la informacion del participante";
                LimpiarCampos();
            }
            catch(Exception ex)
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
            txtEdad.Text = string.Empty;
            txtDomicilio.Text = string.Empty;
            txtSocio.Text = string.Empty;
            txtNoAccion.Text = string.Empty;
            txtTelefonoPersonal.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefonoEmergencia.Text = string.Empty;
            rblCarrera.ClearSelection();
            rblRamas.ClearSelection();
            chkAcepto.Checked = false;
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
            int.TryParse(rblCarrera.SelectedValue, out IdCategoria);

            int? IdRuta = null;

            objParticipanteXCarreraOBJ.IdCategoria = IdCategoria;
            objParticipanteXCarreraOBJ.IdRama = IdRama;
            objParticipanteXCarreraOBJ.IdRuta = IdRuta;

            objPxCBLL.InsertParticipanteXCarrera(objParticipanteXCarreraOBJ);
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
    }
}