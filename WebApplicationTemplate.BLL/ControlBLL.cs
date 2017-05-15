using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
    public class ControlBLL : BaseBLL
    {
        public ControlBLL(UserSession session) :base (session) {/* do nothing */}

        public IList<ControlOBJ> SelectControles()
        {
            return ControlDAL.SelectControles();
        }
    }
}
