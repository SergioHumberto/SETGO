﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class ParticipantesOBJ
    {
        public int IdParticipante { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        public string Domicilio { get; set; }
        public string Invitado { get; set; }
        public int NumeroAccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string TelefonoEmergencia { get; set; }
        public int? IdEquipo { get; set; }
        public string Socio { get; set; }
        //public string TransactionNumber { get; set; }
        //public string StatusPaypal { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Club { get; set; }

        // only saves relations
        public ParticipanteXCarreraOBJ ParticipanteXCarrera { get; set; }
        public int IdTipoEquipo { get; set; }

		//public int Folio { get; set; }
        public string Generic01 { get; set; }
        public string Generic02 { get; set; }
        public string Generic03 { get; set; }
        public string Generic04 { get; set; }
        public string Generic05 { get; set; }
        public string Generic06 { get; set; }
        public string Generic07 { get; set; }
        public string Generic08 { get; set; }
        public string Generic09 { get; set; }
        public string Generic10 { get; set; }

		public DateTime FechaRegistro { get; set; }
		//public DateTime? FechaPago { get; set; }

		//public string FolioOffline { get; set; }
	}
}
