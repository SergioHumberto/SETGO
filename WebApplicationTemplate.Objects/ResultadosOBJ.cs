using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class ResultadosOBJ
    {
        public int IdResultados { get; set; }
        public int? IdParticipante { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Genero { get; set; }
        public string Tiempo { get; set; }
        public int? PosicionGeneral { get; set; }
        public int? PosicionCategoria { get; set; }
        public int? PosicionRama { get; set; }
        public string Velocidad { get; set; }
        public int? Folio { get; set; }
        public int? Dorsal { get; set; }
        public int? Chip { get; set; }
        public string Grupo { get; set; }
    }
}
