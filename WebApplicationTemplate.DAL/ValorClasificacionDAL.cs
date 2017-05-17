using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;
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
        public static void InsertarValorClasificacion(ValorClasificacionOBJ param)
        {
            Mapper.Instance().Insert("InsertarValorClasificacion", param);
        }
        public static void UpdateValorClasificacion(ValorClasificacionOBJ param)
        {
            DAL.Update("UpdateValorClasificacion", param);
        }
        public static void DeleteValorClasificacion(ValorClasificacionOBJ param)
        {
            DAL.Delete("DeleteValorClasificacion", param);
        }
    }
}
