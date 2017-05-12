using System;
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
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["IdCarrera"] != null)
                    {
                        int idCarrera;
                        int.TryParse(Request.QueryString["IdCarrera"], out idCarrera);

                        UserSession session = HttpSecurity.CurrentSession;
                        CarreraBLL carreraBLL = new CarreraBLL(session);

                        CarreraOBJ carreraObj = carreraBLL.SelectCarreraObject(idCarrera);
                        BindDataToControls(carreraObj);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void BindDataToControls(CarreraOBJ carreraObj)
        {
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
                int idCarrera = -1;
                if (Request.QueryString["IdCarrera"] != null)
                {

                    int.TryParse(Request.QueryString["IdCarrera"], out idCarrera);
                }
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

                    carreraBLL.UpdateCarrera(carreraObj);
                }
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }

        }
    }
}