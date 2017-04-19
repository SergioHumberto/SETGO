using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class RamaOBJ
    {
        public RamaOBJ()
        {
            Activo = true;
        }
        public int IdRama { get; set; }
        public string Nombre { get; set; }
        public int IdCarrera { get; set; }
        public bool Activo { get; set; }
    }
}
