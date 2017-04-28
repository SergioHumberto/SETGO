using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class ControlXCarreraOBJ
    {
        public int IdControlXCarrera { get; set; }
        public int IdControl { get; set; }
        public int IdCarrera { get; set; }
        public string Etiqueta { get; set; }
        public bool Requerido { get; set; }
        public string EtiquetaRequerido { get; set; }
        public bool RegularExpression { get; set; }
        public string RegularErrorMessage { get; set; }
        public string ValidationExpression { get; set; }

        public string IdControlASP { get; set; }
    }
}
