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

        public int InsertParticipante(ParticipantesOBJ participante)
        {
            return ParticipantesDAL.InsertParticipante(participante);
        }

        public void InsertParticipanteConCarrera(ParticipantesOBJ participante)
        {
            participante.ParticipanteXCarrera.IdParticipante = InsertParticipante(participante);
            ParticipanteXCarreraBLL objPxCBLL = new ParticipanteXCarreraBLL(session);
            objPxCBLL.InsertParticipanteXCarrera(participante.ParticipanteXCarrera);
        }

        public ParticipantesOBJ SelectParticipanteObject(int IdParticipante)
        {
            return ParticipantesDAL.SelectParticipanteObject(IdParticipante);
        }
    }
}
