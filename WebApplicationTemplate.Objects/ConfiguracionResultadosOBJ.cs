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
		public string NombreCampo { get; set; }
		public bool Visible { get; set; }
	}
}
