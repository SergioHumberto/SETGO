using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public class ClasificacionDAL
    {
        public static IList<ClasificacionOBJ> SelectClasificacion(ClasificacionOBJ finder)
        {
            IList<ClasificacionOBJ> lstClasificacion = DAL.QueryForList<ClasificacionOBJ>("SelectClasificacion", finder);
            return lstClasificacion;
        }
    }
}
