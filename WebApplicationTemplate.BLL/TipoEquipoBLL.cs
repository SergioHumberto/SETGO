using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class TipoEquipoBLL : BaseBLL
	{
        public TipoEquipoBLL(UserSession session) : base(session) { /* do nothing */ }

        public IList<TipoEquipoOBJ> SelectTipoEquipo(TipoEquipoOBJ p_TipoEquipo)
        {
            return TipoEquipoDAL.SelectTipoEquipo(p_TipoEquipo);
        }

        public TipoEquipoOBJ SelectTipoEquipoObject(int IdTipoEquipo)
        {
            return TipoEquipoDAL.SelectTipoEquipoObject(IdTipoEquipo);
        }
    }
}
