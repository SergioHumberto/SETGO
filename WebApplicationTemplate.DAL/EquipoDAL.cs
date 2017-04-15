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
    }
}
