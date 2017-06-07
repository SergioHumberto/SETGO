using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public static class EquipoDAL
    {
        public static int InsertEquipo(EquipoOBJ p_Equipo)
        {
            DAL.Insert("InsertEquipo", p_Equipo);
            return p_Equipo.IdEquipo;
        }

        public static IList<EquipoOBJ> SelectEquipos(EquipoOBJ equipoFinder)
        {
            IList<EquipoOBJ> lstEquipos = DAL.QueryForList<EquipoOBJ>("SelectEquipos", equipoFinder);
            return lstEquipos;
        }

        public static void UpdateEquipo(EquipoOBJ p_Equipo)
        {
            DAL.Update("UpdateEquipo", p_Equipo);
        }

        public static EquipoOBJ SelectEquipoObject(int IdEquipo)
        {
            EquipoOBJ equipoResult = DAL.QueryForObject<EquipoOBJ>("SelectEquipoObject", IdEquipo);
            return equipoResult;
        }
    }
}
