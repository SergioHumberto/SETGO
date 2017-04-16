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

        public IList<TipoEquipoOBJ> SelectTipoEquipos()
        {
            return TipoEquipoDAL.SelectTipoEquipo(new TipoEquipoOBJ());
        }

        public void InsertaTipoEquipo(TipoEquipoOBJ p_tipoEquipoOBJ)
        {
            TipoEquipoDAL.InsertaTipoEquipo(p_tipoEquipoOBJ);
        }

        public void UpdateTipoEquipo(TipoEquipoOBJ p_tipoEquipoOBJ)
        {
            TipoEquipoDAL.UpdateTipoEquipo(p_tipoEquipoOBJ);
        }

        public void DeleteTipoEquipo(TipoEquipoOBJ p_tipoEquipoOBJ)
        {
            TipoEquipoDAL.DeleteTipoEquipo(p_tipoEquipoOBJ);
        }
    }
}
