using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class EquipoBLL : BaseBLL
	{
        public EquipoBLL(UserSession session) : base(session) { }

        public int InsertEquipo(EquipoOBJ p_Equipo)
        {
            return EquipoDAL.InsertEquipo(p_Equipo);
        }

        public IList<EquipoOBJ> SelectEquipos(EquipoOBJ equipoFinder)
        {
            return EquipoDAL.SelectEquipos(equipoFinder);
        }

        public void UpdateEquipo(EquipoOBJ p_Equipo)
        {
            EquipoDAL.UpdateEquipo(p_Equipo);
        }

        public EquipoOBJ SelectEquipoObject(int IdEquipo)
        {
            return EquipoDAL.SelectEquipoObject(IdEquipo);
        }


    }
}
