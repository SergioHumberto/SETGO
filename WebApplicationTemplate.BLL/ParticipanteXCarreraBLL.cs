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

        public ParticipanteXCarreraOBJ SelectParticipanteXCarrera(int IdParticipanteXCarrera)
        {
            return ParticipanteXCarreraDAL.SelectParticipanteXCarrera(IdParticipanteXCarrera);
        }

		public ParticipanteXCarreraOBJ SelectParticipanteXCarreraByIdParticipante(int IdParticipante)
		{
			return ParticipanteXCarreraDAL.SelectParticipanteXCarreraByIdParticipante(IdParticipante);
		}

		public void UpdateInfoPagoParticipante(ParticipanteXCarreraOBJ PxC)
		{
			ParticipanteXCarreraDAL.UpdateInfoPagoParticipante(PxC);
		}

		public IList<ParticipanteXCarreraOBJ> SelectParticipante(ParticipanteXCarreraOBJ p_ParticipanteOBJ)
		{
			return ParticipanteXCarreraDAL.SelectParticipante(p_ParticipanteOBJ);
		}
	}
}
