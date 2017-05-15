using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public static class ControlDAL
    {
        public static IList<ControlOBJ> SelectControles()
        {
            return Mapper.Instance().QueryForList<ControlOBJ>("SelectControles",new ControlOBJ());
        }            
    }
}
