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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IdCarreraProperty > 0)
                {
                    LoadCarrera(IdCarreraProperty);

                    if (Request.QueryString["tx"] != null)
                    {
                        ProcesaRespuestaPayPal();
                    }
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
                // lblMessage.Text = "Se guardó la informacion del participante";
                // LimpiarCampos();    
                PaypalDelegateForm();
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
            int.TryParse(rblCarrera.SelectedValue, out IdCategoria);

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

        protected void ddlTipoRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void PaypalDelegateForm()
        {
            string strCustom = "IdParticipante={0}";
            strCustom = string.Format(strCustom, IdParticipanteVSProperty);

            // Urls.Abs("~/Pages/RegistroParticipantes.aspx")
            string strURLReturn = "http://localhost:61880/WebApplicationTemplate/Pages/RegistroParticipantes.aspx?IdCarrera={0}";
            strURLReturn = string.Format(strURLReturn, IdCarreraProperty);

            ParticipanteXCarreraBLL objPxCBLL = new ParticipanteXCarreraBLL(HttpSecurity.CurrentSession);
            ParticipanteXCarreraOBJ objPxC = objPxCBLL.SelectParticipanteXCarrera(IdParticipanteXCarreraProperty);

            decimal dAmount = 0;
            if (objPxC != null)
            {
                if (objPxC.IdCategoria.HasValue)
                {
                    dAmount = GetPrecioXCategoria(objPxC.IdCategoria.Value);
                }
            }

            // Urls.Abs("~/Pages/RegistroParticipantes.aspx")
            string strCancelURL = "http://localhost:61880/WebApplicationTemplate/Pages/RegistroParticipantes.aspx?IdCarrera={0}";
            strCancelURL = string.Format(strCancelURL, IdCarreraProperty);

            SessionPayPal objSessionPayPal = new SessionPayPal()
            {
                IdCarrera = IdCarreraProperty
                , amount = dAmount
                , custom = strCustom
                , item_name = "Carrera"
                , returnURL = strURLReturn
                , cancelURL = strCancelURL
            };

            Session.Add("objSessionPayPal", objSessionPayPal);

            string url = Urls.Abs("~/Pages/PayPal.aspx");
            
            // string s = "window.open('" + url + "', 'popup_window', 'width=500,height=800,left=100,top=100,resizable=yes');";
            // ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            Response.Redirect(url);
        }

        private void ProcesaRespuestaPayPal()
        {
            CarreraBLL objCarreraBLL = new CarreraBLL(HttpSecurity.CurrentSession);
            CarreraOBJ objCarreraOBJ = objCarreraBLL.SelectCarreraObject(IdCarreraProperty);

            string authToken = string.Empty;
            if (objCarreraOBJ != null)
            {
                // ejemplo: "wyws0SQYueHY3xZJte9l9nr4h1OT7FGixDL0a3bJwqwY0ABJbKoZkxzibR4"
                authToken = objCarreraOBJ.TokenPaypalTDP;
            }

            string txToken = Request.QueryString["tx"];
            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;

            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";

            //ServicePointManager.ServerCertificateValidationCallback =
            //    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //    { return true; };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.ServerCertificateValidationCallback +=
                new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(query);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            Dictionary<string, string> results = new Dictionary<string, string>();
            if (strResponse != "")
            {
                divRespuestaPaypal.Visible = true;

                StringReader reader = new StringReader(strResponse);
                string line = reader.ReadLine();

                if (line == "SUCCESS")
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        results.Add(line.Split('=')[0], line.Split('=')[1]);

                    }

                    lblTituloRespuesta.Text = "Tu orden ha sido recibida";
                    lblNombre.Text = results["first_name"] + " " + results["last_name"];
                    lblItem.Text = results["item_name"];

                    // Response.Write("<li>Amount: " + results["payment_gross"] + "</li>");
                    // Response.Write("<hr>");
                }
                else if (line == "FAIL")
                {
                    // Log for manual investigation
                    lblTituloRespuesta.Text = "No se pudo recibir detalles de la transaccion";
                }
            }
            else
            {
                //unknown error
                Response.Write("ERROR");
            }
        }
    }
}