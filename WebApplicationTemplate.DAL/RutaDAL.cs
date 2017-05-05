using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
	public static class RutaDAL
	{
        public static RutaOBJ SelectRutaByIdParticipante(int IdParticipante)
        {
            RutaOBJ objRuta = Mapper.Instance().QueryForObject<RutaOBJ>("SelectRutaByIdParticipante", IdParticipante);
            return objRuta;
        }
    }
}
