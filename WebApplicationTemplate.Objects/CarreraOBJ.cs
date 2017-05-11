using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class CarreraOBJ
    {
        public CarreraOBJ()
        {
            Activo = true;
            FechaIniForQuery = new DateTime(1753, 1, 1);
            FechaFinForQuery = new DateTime(1753, 1, 1);
        }
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
        public string PayPalEmail { get; set; }
        public string TokenPaypalTDP { get; set; }
        public string DescripcionPoliticas { get; set; }
        public bool Activo { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string URLRegistro { get; set; }    
        public int FolioInicial { get; set; }
        public int SiguienteFolio { get; set; }
        public DateTime FechaIniForQuery { get; set; }
        public DateTime FechaFinForQuery { get; set; }
    }
}
