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
    }
}
