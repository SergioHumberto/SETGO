using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public static class TipoEquipoDAL
    {
        public static IList<TipoEquipoOBJ> SelectTipoEquipo(TipoEquipoOBJ p_TipoEquipo)
        {
            IList<TipoEquipoOBJ> lstTipoEquipo = DAL.QueryForList<TipoEquipoOBJ>("SelectTipoEquipo", p_TipoEquipo);
            return lstTipoEquipo;
        }

        public static TipoEquipoOBJ SelectTipoEquipoObject(int idTipoEquipo)
        {
            TipoEquipoOBJ objTipoEquipo = DAL.QueryForObject<TipoEquipoOBJ>("SelectTipoEquipoObject", idTipoEquipo);
            return objTipoEquipo;
        }
    }
}
