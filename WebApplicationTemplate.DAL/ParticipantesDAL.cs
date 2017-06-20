using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
	public static class ParticipantesDAL
	{
        public static int InsertParticipante(ParticipantesOBJ participante)
        {
            DAL.Insert("InsertParticipante", participante);
            return participante.IdParticipante;
        }

        public static ParticipantesOBJ SelectParticipanteObject(int IdParticipante)
        {
            ParticipantesOBJ objParticipante = DAL.QueryForObject<ParticipantesOBJ>("SelectParticipanteObject", IdParticipante);
            return objParticipante;
        }
    }
}
