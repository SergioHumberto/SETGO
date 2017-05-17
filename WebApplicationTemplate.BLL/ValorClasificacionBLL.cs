using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
    public class ValorClasificacionBLL : BaseBLL
    {
        public ValorClasificacionBLL(UserSession session) : base(session) { }

        public IList<ValorClasificacionOBJ> SelectValorClasificacion(ValorClasificacionOBJ finder)
        {
            return ValorClasificacionDAL.SelectValorClasificacion(finder);
        }
        public void InsertarValorClasificacion(ValorClasificacionOBJ param)
        {
            ValorClasificacionDAL.InsertarValorClasificacion(param);
        }
        public void UpdateValorClasificacion(ValorClasificacionOBJ param)
        {
            ValorClasificacionDAL.UpdateValorClasificacion(param);
        }
        public void DeleteValorClasificacion(ValorClasificacionOBJ param)
        {
            ValorClasificacionDAL.DeleteValorClasificacion(param);
        }
    }
}
