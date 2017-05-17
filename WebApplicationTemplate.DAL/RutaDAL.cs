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
        public static void InsertarRuta(RutaOBJ param)
        {
            Mapper.Instance().Insert("InsertarRuta", param);
        }
        public static void UpdateRuta(RutaOBJ param)
        {
            DAL.Update("UpdateRuta", param);
        }
        public static void DeleteRuta(RutaOBJ param)
        {
            DAL.Delete("DeleteRuta", param);
        }

		public static IList<RutaOBJ> SeleccionarRutasByIdCategoria(int idCategoria)
		{
			return Mapper.Instance().QueryForList<RutaOBJ>("SeleccionarRutasByIdCategoria", idCategoria);
		}

	}
}
