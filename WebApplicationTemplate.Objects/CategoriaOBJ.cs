using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class CategoriaOBJ
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public Decimal Precio { get; set; }
        public int IdCarrera { get; set; }
    }
}
