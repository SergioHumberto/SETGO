using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class CategoriaBLL
	{
		public void InsertarCategoria(CategoriaOBJ categoriaOBJ)
		{
			CategoriaDAL.InsertarCategoria(categoriaOBJ);
		}
	}
}
