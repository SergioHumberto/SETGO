using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
    public class ClasificacionBLL : BaseBLL
    {
        public ClasificacionBLL(UserSession session) : base(session) { }

        public IList<ClasificacionOBJ> SelectClasificacion(ClasificacionOBJ finder)
        {
            return ClasificacionDAL.SelectClasificacion(finder);
        }
        public void InsertarClasificacion(ClasificacionOBJ param)
        {
            ClasificacionDAL.InsertarClasificacion(param);
        }
        public void UpdateClasificacion(ClasificacionOBJ param)
        {
            ClasificacionDAL.UpdateClasificacion(param);
        }
        public void DeleteClasificacion(ClasificacionOBJ param)
        {
            ClasificacionDAL.DeleteClasificacion(param);
        }
    }
}
