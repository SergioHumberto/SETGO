using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class ParticipantesBLL : BaseBLL
    {
        public ParticipantesBLL(UserSession session) : base(session) { /* do nothing */ }

        public void InsertParticipante(ParticipantesOBJ participante)
        {
            ParticipantesDAL.InsertParticipante(participante);
        }
    }
}
