using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
    public class CarreraBLL : BaseBLL
    {
        public CarreraBLL(UserSession session) : base(session) { /* do nothing */ }

        public int InsertarCarrera(CarreraOBJ carreraOBJ)
        {
			return CarreraDAL.InsertarCarrera(carreraOBJ);
        }

        public IList<CarreraOBJ> SelectCarrera(CarreraOBJ p_CarreraOBJ)
        {
            return CarreraDAL.SelectCarrera(p_CarreraOBJ);
        }

        public CarreraOBJ SelectCarreraObject(int IdCarrera)
        {
            return CarreraDAL.SelectCarreraObject(IdCarrera);
        }

		public void UpdateSiguienteFolio(CarreraOBJ carreraOBJ)
		{
			CarreraDAL.UpdateSiguienteFolio(carreraOBJ);
		}
        public void UpdateActivo(CarreraOBJ carreraOBJ)
        {
            CarreraDAL.UpdateActivo(carreraOBJ);
        }
        public void UpdateCarrera(CarreraOBJ carreraOBJ)
        {
            CarreraDAL.UpdateCarrera(carreraOBJ);
        }

    }
}
