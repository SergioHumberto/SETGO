using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class ResultadosOBJ
    {
        public int IdResultado { get; set; }
        public int? IdParticipante { get; set; }
		public int IdConfiguracionResultados { get; set; }
		public int? Numero { get; set; }
		public string Paterno { get; set; }
		public string Materno { get; set; }
		public string Nombres { get; set; }
		public int? Folio { get; set; }
		public string Sexo { get; set; }
		public string Categoria { get; set; }
		public string Procedencia { get; set; }
		public string Equipo { get; set; }
		public string Telefono { get; set; }
		public string T_Chip { get; set; }
		public string T_Oficial { get; set; }
		public int? Lug_Cat { get; set; }
		public int? Lug_Rama { get; set; }
		public string Vel { get; set; }
		public int? Lug_Gral { get; set; }
		public string Rama { get; set; }
        public int Edad { get; set; }
        public string T_Intermedio { get; set; }
        public string Ruta { get; set; }
	}
}
