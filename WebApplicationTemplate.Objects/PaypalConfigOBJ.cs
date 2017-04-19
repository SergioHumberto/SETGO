using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
	public class PaypalConfigOBJ
	{
		public int IdPaypalConfig { get; set; }
		public string PaypalURL { get; set; }
		public string Descripcion { get; set; }
		public bool Activo { get; set; }
	}
}
