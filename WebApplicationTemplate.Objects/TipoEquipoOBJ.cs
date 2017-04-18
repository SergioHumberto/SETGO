using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class TipoEquipoOBJ
    {
        public TipoEquipoOBJ()
        {
            Activo = true;                        
        }
        public int IdTipoEquipo { get; set; }
        public int CantidadParticipantes { get; set; }
        public Decimal Precio { get; set; }
        public int IdCategoria { get; set; }        
        public bool Activo { get; set; }
    }
}
