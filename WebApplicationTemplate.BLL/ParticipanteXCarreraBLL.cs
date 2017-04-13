using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class ParticipanteXCarreraBLL : BaseBLL
	{
        public ParticipanteXCarreraBLL(UserSession session) : base(session) { /* do nothing */ }

        public void InsertParticipanteXCarrera(ParticipanteXCarreraOBJ p_ParticipanteXCarrera)
        {
            ParticipanteXCarreraDAL.InsertParticipanteXCarrera(p_ParticipanteXCarrera);
        }
    }
}
