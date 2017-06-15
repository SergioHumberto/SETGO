using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class EquipoOBJ
    {
        public int IdEquipo { get; set; }
        public int? IdTipoEquipo { get; set; }
        public string EmailsParticipantes { get; set; }
        public int? IdCarrera { get; set; }
        public int? CantidadRegistrados { get; set; }
        public Guid Guid { get; set; }
        public string Nombre { get; set; }
    }
}
