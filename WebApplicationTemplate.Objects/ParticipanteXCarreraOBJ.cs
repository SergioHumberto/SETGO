using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class ParticipanteXCarreraOBJ
    {
        public int IdParticipanteXCarrera { get; set; }
        public int? IdParticipante { get; set; }
        public int? IdCarrera { get; set; }
        public int? IdRama { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdRuta { get; set; }
    }
}
