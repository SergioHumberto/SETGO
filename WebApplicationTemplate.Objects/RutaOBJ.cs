using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class RutaOBJ
    {
        public RutaOBJ()
        {
            Activo = true;
        }
        public int IdRuta { get; set; }
        public string Nombre { get; set; }
        public Decimal DistanciaKM { get; set; }
        public bool Activo { get; set; }
        public int IdCategoria { get; set; }
    }
}
