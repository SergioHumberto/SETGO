using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.DAL;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.BLL
{
    public class ControlXCarreraBLL : BaseBLL
    {
        public ControlXCarreraBLL(UserSession session) : base(session) { /* do nothing */ }

        public int InsertControlXCarrera(ControlXCarreraOBJ param)
        {
            return ControlXCarreraDAL.InsertControlXCarrera(param);
        }

        public ControlXCarreraOBJ SelectControlXCarreraObject(int IdControlXCarrera)
        {
            return ControlXCarreraDAL.SelectControlXCarreraObject(IdControlXCarrera);
        }

        public IList<ControlXCarreraOBJ> SelectControlXCarrera(ControlXCarreraOBJ param)
        {
            return ControlXCarreraDAL.SelectControlXCarrera(param);
        }
        public void UpdateControlXCarrera(ControlXCarreraOBJ controlXCarrera)
        {
            ControlXCarreraDAL.UpdateControlXCarrera(controlXCarrera);
        }
        public void DeleteControlXCarrera(ControlXCarreraOBJ controlXCarrera)
        {
            ControlXCarreraDAL.DeleteControlXCarrera(controlXCarrera);
        }

    }
}
