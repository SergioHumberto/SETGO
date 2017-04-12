using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public static class CategoriaDAL
    {
        public static void InsertarCategoria(CategoriaOBJ categoriaOBJ)
        {
            Mapper.Instance().Insert("InsertarCategoria", categoriaOBJ);
        }

        public static IList<CategoriaOBJ> SelectCategoria(CategoriaOBJ p_CategoriaOBJ)
        {
            IList<CategoriaOBJ> lstCategorias = Mapper.Instance().QueryForList<CategoriaOBJ>("SelectCategoria", p_CategoriaOBJ);
            return lstCategorias;
        }

        public static CategoriaOBJ SelectCategoriaObject(int IdCategoria)
        {
            CategoriaOBJ objCategoria = Mapper.Instance().QueryForObject<CategoriaOBJ>("SelectCategoriaObject", IdCategoria);
            return objCategoria;
        }
    }
}
