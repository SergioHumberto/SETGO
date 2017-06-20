using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
	public static class ParticipanteXCarreraDAL
	{
        public static void InsertParticipanteXCarrera(ParticipanteXCarreraOBJ p_ParticipanteXCarrera)
        {
            Mapper.Instance().Insert("InsertParticipanteXCarrera", p_ParticipanteXCarrera);
        }

        public static ParticipanteXCarreraOBJ SelectParticipanteXCarrera(int IdParticipanteXCarrera)
        {
            ParticipanteXCarreraOBJ objParticipanteXCarreraOBJ = Mapper.Instance().QueryForObject<ParticipanteXCarreraOBJ>("SelectParticipanteXCarrera", IdParticipanteXCarrera);
            return objParticipanteXCarreraOBJ;
        }
		
		public static ParticipanteXCarreraOBJ SelectParticipanteXCarreraByIdParticipante(int IdParticipante)
		{
			ParticipanteXCarreraOBJ objParticipanteXCarreraOBJ = Mapper.Instance().QueryForObject<ParticipanteXCarreraOBJ>("SelectParticipanteXCarreraByIdParticipante", IdParticipante);
			return objParticipanteXCarreraOBJ;
		}

		public static void UpdateInfoPagoParticipante(ParticipanteXCarreraOBJ PxC)
		{
			Mapper.Instance().Update("UpdateInfoPagoParticipante", PxC);
		}

		public static IList<ParticipanteXCarreraOBJ> SelectParticipante(ParticipanteXCarreraOBJ p_ParticipanteOBJ)
		{
			IList<ParticipanteXCarreraOBJ> lstParticipantes = DAL.QueryForList<ParticipanteXCarreraOBJ>("SelectParticipante", p_ParticipanteOBJ);
			return lstParticipantes;
		}
	}
}
