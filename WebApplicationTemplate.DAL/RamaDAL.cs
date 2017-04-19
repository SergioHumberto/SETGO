using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
	public static class RamaDAL
	{
        public static IList<RamaOBJ> SelectRama(RamaOBJ p_RamaOBJ)
        {
            IList<RamaOBJ> lstRamas = Mapper.Instance().QueryForList<RamaOBJ>("SelectRama", p_RamaOBJ);
            return lstRamas;
        }

		public static RamaOBJ SelectRamaByIdParticipante(int IdParticipante)
		{
			return Mapper.Instance().QueryForObject<RamaOBJ>("SelectRamaByIdParticipante", IdParticipante);
		}
	}
}
