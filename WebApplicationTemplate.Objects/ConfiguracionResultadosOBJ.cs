using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
	public class ConfiguracionResultadosOBJ
	{
		public int IdConfiguracionResultados { get; set; }
		public int IdCarrera { get; set; }
		public int? IdCategoria { get; set; }
		public bool Numero { get; set; }
		public bool Paterno { get; set; }
		public bool Materno { get; set; }
		public bool Nombres { get; set; }
		public bool Folio { get; set; }
        public bool Edad { get; set; }
        public bool Sexo { get; set; }
		public bool Categoria { get; set; }
		public bool Procedencia { get; set; }
		public bool Equipo { get; set; }
		public bool Telefono { get; set; }
        public bool T_Intermedio { get; set; }
        public bool T_Chip { get; set; }
		public bool T_Oficial { get; set; }
		public bool Lug_Cat { get; set; }
		public bool Lug_Rama { get; set; }
		public bool Vel { get; set; }
		public bool Lug_Gral { get; set; }
		public bool Rama { get; set; }                
        public bool Ruta { get; set; }
        public int IdCertificado { get; set; }
        public string ImgCertificado { get; set; }
        public bool T_5K { get; set; }
        public bool T_10K { get; set; }
        public bool T_15K { get; set; }
        public bool T_21K { get; set; }
        public bool T_25K { get; set; }
        public bool T_30K { get; set; }
        public bool T_35K { get; set; }
    }
}
