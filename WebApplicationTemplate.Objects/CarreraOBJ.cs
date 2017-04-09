using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class CarreraOBJ
    {
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public Decimal Precio { get; set; }
        public string CategoriaEvento { get; set; }
        public string PaginaWeb { get; set; }
        public string PalabrasClave { get; set; }
        public string URLMapa { get; set; }
        public string Ubicacion { get; set; }
        public string ContenidoHtml { get; set; }
    }
}
