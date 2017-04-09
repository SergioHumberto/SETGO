using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Web.Tools;

namespace WebApplicationTemplate.Web.Pages
{
    public partial class RegistroParticipantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            InsertarParticipante();
        }

        private void InsertarParticipante()
        {
            ParticipantesOBJ objParticipante = FillParticipanteOBJ();
            ParticipantesBLL objParticipanteBLL = new ParticipantesBLL(HttpSecurity.CurrentSession);
            objParticipanteBLL.InsertParticipante(objParticipante);

        }

        private ParticipantesOBJ FillParticipanteOBJ()
        {
            ParticipantesOBJ objParticipante = new ParticipantesOBJ();
            objParticipante.Nombre = txtNombres.Text.Trim();
            objParticipante.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
            objParticipante.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
            objParticipante.Edad = int.Parse(txtEdad.Text.Trim());
            objParticipante.Domicilio = txtDomicilio.Text.Trim();
            // socio queda pendiente
            objParticipante.Invitado = txtInvitado.Text.Trim();
            objParticipante.NumeroAccion = int.Parse(txtNoAccion.Text.Trim());
            objParticipante.Telefono = txtTelefonoPersonal.Text.Trim();
            objParticipante.Email = txtEmail.Text.Trim();
            objParticipante.TelefonoEmergencia = txtTelefonoEmergencia.Text.Trim();

            return objParticipante;
        }
    }
}