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
    public partial class BuscaCarrera : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDataToCarrerasGridView(getDataCarreras());
                lnkShowInactive.Text = getTextlnkShowInactive();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            BindDataToCarrerasGridView(getDataCarreras());
        }

        protected void lnkShowInactive_Click(object sender, EventArgs e)
        {
            if (ViewState["ShowInactive"] == null)
            {
                ViewState.Add("ShowInactive", true);
            }
            else
            {
                ViewState.Add("ShowInactive", !(bool)ViewState["ShowInactive"]);
            }
            lnkShowInactive.Text = getTextlnkShowInactive();
            BindDataToCarrerasGridView(getDataCarreras());
        }

        protected IList<CarreraOBJ> getDataCarreras()
        {
            UserSession session = HttpSecurity.CurrentSession;
            CarreraBLL carreraBLL = new CarreraBLL(session);

            CarreraOBJ carreraObj = new CarreraOBJ();
            carreraObj.Activo = (ViewState["ShowInactive"] != null && (bool)ViewState["ShowInactive"]) ? false : true;
            carreraObj.Nombre = "%" + txtNombreCarrera.Text + "%";
            CultureInfo ci = CultureInfo.CreateSpecificCulture("es-MX");

            DateTime fechaIni = new DateTime(1753, 1, 1);
            if(txtDesde.Text != string.Empty)
                DateTime.TryParse(txtDesde.Text, ci.DateTimeFormat, DateTimeStyles.None, out fechaIni);
            carreraObj.FechaIniForQuery = fechaIni;
            if (fechaIni < new DateTime(1753, 1, 1))
            {
                lblError.InnerText = "La fecha inicial debe ser mayor o igual al 01/01/1753";
                lblError.Visible = true;
                return null;
            }                

            DateTime fechaFin = new DateTime(1753, 1, 1);
            if (txtHasta.Text != string.Empty)
                DateTime.TryParse(txtHasta.Text, ci.DateTimeFormat, DateTimeStyles.None, out fechaFin);
            carreraObj.FechaFinForQuery= fechaFin;
            if (fechaFin < new DateTime(1753, 1, 1))
            {
                lblError.InnerText = "La fecha final debe ser mayor o igual al 01/01/1753";
                lblError.Visible = true;
                return null;
            }

            return carreraBLL.SelectCarrera(carreraObj);
        }
       
        protected void grdCarreras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //Buscamos el control con el valor de ID de la Carrera
                LinkButton lnkButton = (LinkButton)e.CommandSource;
                GridViewRow grdViewRow = (GridViewRow)lnkButton.NamingContainer;
                Label lblIdCarrera = grdViewRow.FindControl("lblIdCarrera") as Label;
                int idCarrera;
                int.TryParse(lblIdCarrera.Text, out idCarrera);
                
                CarreraOBJ carreraObj = new CarreraOBJ();
                carreraObj.IdCarrera = idCarrera;
                //Si existe o está activa la bandera "ShowInactive" significa que está Inactiva se debe Activar la carrera seleccionada
                carreraObj.Activo = (ViewState["ShowInactive"] != null && (bool)ViewState["ShowInactive"]) ? true : false;

                UserSession session = HttpSecurity.CurrentSession;
                CarreraBLL carreraBLL = new CarreraBLL(session);
                carreraBLL.UpdateActivo(carreraObj);

                BindDataToCarrerasGridView(getDataCarreras());
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void grdCarreras_SelectedIndexChanged(object sender, EventArgs e)
        {            
            Label lblIdCarrera = grdCarreras.SelectedRow.FindControl("lblIdCarrera") as Label;
            int idCarrera;
            int.TryParse(lblIdCarrera.Text, out idCarrera);

            string targetURL = "~/Pages/ConfiguraCarrera.aspx?IdCarrera={0}";

            targetURL = string.Format(targetURL, idCarrera);

            Response.Redirect(targetURL);
        }

        protected void grdCarreras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkbutton = e.Row.FindControl("btnActivarDesactivar") as LinkButton;
                    lnkbutton.Text = getTextBtnActivarDesactivar();
                }
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected string getTextlnkShowInactive()
        {
            return (ViewState["ShowInactive"] != null && (bool)ViewState["ShowInactive"]) ? "Mostrar Activos" : "Mostrar Inactivos";
        }

        protected string getTextBtnActivarDesactivar()
        {
            return (ViewState["ShowInactive"] != null && (bool)ViewState["ShowInactive"]) ? "Activar" : "Desactivar";
        }

        protected void BindDataToCarrerasGridView(IList<CarreraOBJ> lstCarreras)
        {
            grdCarreras.DataSource = lstCarreras;
            grdCarreras.DataBind();
        }

        protected void lnkLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreCarrera.Text = string.Empty;
            txtDesde.Text = string.Empty;
            txtHasta.Text = string.Empty;
            ViewState.Remove("ShowInactive");
            BindDataToCarrerasGridView(getDataCarreras());
        }
    }
}