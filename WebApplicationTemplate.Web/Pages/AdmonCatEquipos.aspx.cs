using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Web.Tools;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindDataToEquiposGridView(getDataTipoEquipos());
        }

        protected IList<TipoEquipoOBJ> getDataTipoEquipos()
        {
            UserSession session = HttpSecurity.CurrentSession;
            TipoEquipoBLL objTipoEquipoBll = new TipoEquipoBLL(session);

            return objTipoEquipoBll.SelectTipoEquipos();
        }

        protected void BindDataToEquiposGridView(IList<TipoEquipoOBJ> lstTipoEquipos)
        {
            if (lstTipoEquipos.Count > 0)
            {
                grdEquipos.DataSource = lstTipoEquipos;
                grdEquipos.DataBind();
            }
        }

        protected void LimpiaErrores()
        {
            lblError.InnerText = "";
            lblError.Visible = false;
        }

        protected void grdEquipos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LimpiaErrores();
            try
            {
                grdEquipos.EditIndex = e.NewEditIndex;
                if (ViewState["Nuevo"] != null && (bool)ViewState["Nuevo"])
                {
                    IList<TipoEquipoOBJ> lstTipoEquipos = getDataTipoEquipos();

                    lstTipoEquipos.Add(new TipoEquipoOBJ());
                    BindDataToEquiposGridView(lstTipoEquipos);
                }
                else
                    BindDataToEquiposGridView(getDataTipoEquipos());
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
            LimpiaErrores();
            try
            {
                GridViewRow grdRow = (GridViewRow)grdEquipos.Rows[e.RowIndex];

                int IdTipoEquipo;
                int.TryParse(grdRow.Cells[0].Text, out IdTipoEquipo);
                int CantidadParticipantes;
                int.TryParse(e.NewValues["CantidadParticipantes"].ToString(), out CantidadParticipantes);
                decimal precio;
                decimal.TryParse(e.NewValues["Precio"].ToString(), out precio);
                int IdCategoria;
                int.TryParse(e.NewValues["IdCategoria"].ToString(), out IdCategoria);

                TipoEquipoOBJ tipoEquipoObj = new TipoEquipoOBJ();
                tipoEquipoObj.IdTipoEquipo = IdTipoEquipo;
                tipoEquipoObj.CantidadParticipantes = CantidadParticipantes;
                tipoEquipoObj.Precio = precio;
                tipoEquipoObj.IdCategoria = IdCategoria;

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
                BindDataToEquiposGridView(getDataTipoEquipos());
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void grdEquipos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LimpiaErrores();
            try
            {
                UserSession session = HttpSecurity.CurrentSession;
                TipoEquipoBLL objTipoEquipoBll = new TipoEquipoBLL(session);

                int IdTipoEquipo;
                int.TryParse(e.Values["IdTipoEquipo"].ToString(), out IdTipoEquipo);

                TipoEquipoOBJ tipoEquipoObj = new TipoEquipoOBJ();
                tipoEquipoObj.IdTipoEquipo = IdTipoEquipo;

                objTipoEquipoBll.DeleteTipoEquipo(tipoEquipoObj);
                BindDataToEquiposGridView(getDataTipoEquipos());
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void grdEquipos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LimpiaErrores();
            try
            {
                grdEquipos.EditIndex = -1;
                ViewState.Remove("Nuevo");
                BindDataToEquiposGridView(getDataTipoEquipos());
            }
            catch (Exception ex)
            {
                lblError.InnerText = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnAddNewRow_Click(object sender, EventArgs e)
        {
            LimpiaErrores();
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
    }
}