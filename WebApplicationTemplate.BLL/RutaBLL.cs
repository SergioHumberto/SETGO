using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class RutaBLL : BaseBLL
    {
        public RutaBLL(UserSession session) : base(session) { /* do nothing */ }

        public RutaOBJ SelectRutaByIdParticipante(int IdParticipante)
        {
            return RutaDAL.SelectRutaByIdParticipante(IdParticipante);
        }
    }
}
