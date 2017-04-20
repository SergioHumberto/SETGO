using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.DAL;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public class ControlXCarreraDAL
    {
        public static int InsertControlXCarrera(ControlXCarreraOBJ param)
        {
            DAL.Insert("InsertControlXCarrera", param);
            return param.IdControlXCarrera;
        }

        public static ControlXCarreraOBJ SelectControlXCarreraObject(int IdControlXCarrera)
        {
            ControlXCarreraOBJ controlXCarrera = DAL.QueryForObject<ControlXCarreraOBJ>("SelectControlXCarreraObject", IdControlXCarrera);
            return controlXCarrera;
        }

        public static IList<ControlXCarreraOBJ> SelectControlXCarrera(ControlXCarreraOBJ controlXCarrera)
        {
            IList<ControlXCarreraOBJ> lstControlXCarrera = DAL.QueryForList<ControlXCarreraOBJ>("SelectControlXCarrera", controlXCarrera);
            return lstControlXCarrera;
        }
    }
}
