using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public static class CarreraDAL
    {
        public static void InsertarCarrera(CarreraOBJ carreraOBJ)
        {
            Mapper.Instance().Insert("InsertarCarrera", carreraOBJ);
        }

        public static IList<CarreraOBJ> SelectCarrera(CarreraOBJ p_CarreraOBJ)
        {
            IList<CarreraOBJ> lstCarreras = Mapper.Instance().QueryForList<CarreraOBJ>("SelectCarrera", p_CarreraOBJ);
            return lstCarreras;
        }

        public static CarreraOBJ SelectCarreraObject(int IdCarrera)
        {
            CarreraOBJ objCarreraResult = Mapper.Instance().QueryForObject<CarreraOBJ>("SelectCarreraObject", IdCarrera);
            return objCarreraResult;
        }

		public static void UpdateSiguienteFolio(CarreraOBJ carreraOBJ)
		{
			DAL.Update("UpdateSiguienteFolio", carreraOBJ);
		}
        public static void UpdateActivo(CarreraOBJ carreraOBJ)
        {
            DAL.Update("UpdateActivo", carreraOBJ);
        }
        public static void UpdateCarrera(CarreraOBJ carreraOBJ)
        {
            DAL.Update("UpdateCarrera", carreraOBJ);
        }
    }
}
