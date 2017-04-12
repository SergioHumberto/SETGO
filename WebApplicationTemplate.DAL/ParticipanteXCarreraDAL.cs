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
	}
}
