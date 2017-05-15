using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class CategoriaBLL : BaseBLL
	{
        public CategoriaBLL(UserSession session) : base(session) { /* do nothing */ }

        public void InsertarCategoria(CategoriaOBJ categoriaOBJ)
		{
			CategoriaDAL.InsertarCategoria(categoriaOBJ);
		}

        public IList<CategoriaOBJ> SelectCategoria(CategoriaOBJ p_CategoriaOBJ)
        {
            return CategoriaDAL.SelectCategoria(p_CategoriaOBJ);
        }

        public CategoriaOBJ SelectCategoriaObject(int IdCategoria)
        {
            return CategoriaDAL.SelectCategoriaObject(IdCategoria);
        }

        public CategoriaOBJ SelectCategoriaByIdParticipante(int IdParticipante)
        {
            return CategoriaDAL.SelectCategoriaByIdParticipante(IdParticipante);
        }
        public void UpdateCategoria(CategoriaOBJ param)
        {
            CategoriaDAL.UpdateCategoria(param);
        }
        public void DeleteCategoria(CategoriaOBJ param)
        {
            CategoriaDAL.DeleteCategoria(param);
        }
    }
}
