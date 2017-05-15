using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public class ValorClasificacionDAL
    {
        public static IList<ValorClasificacionOBJ> SelectValorClasificacion(ValorClasificacionOBJ finder)
        {
            IList<ValorClasificacionOBJ> lstValorClasificacion = DAL.QueryForList<ValorClasificacionOBJ>("SelectValorClasificacion", finder);
            return lstValorClasificacion;
        }
    }
}
