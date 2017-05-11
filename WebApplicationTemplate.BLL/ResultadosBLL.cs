using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class ResultadosBLL
	{
		public void InsertarCarrera(ResultadosOBJ resultadosOBJ)
		{
			ResultadosDAL.InsertarResultado(resultadosOBJ);
		}

		public bool VerificarResultadoDeCarrera(int IdCarrera)
		{
			if(ResultadosDAL.VerificarResultadoDeCarrera(IdCarrera) == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public void EliminarResultadosDeCarrera(int IdCarrera)
		{
			ResultadosDAL.EliminarResultadosDeCarrera(IdCarrera);
		}

		public IList<ResultadosOBJ> SeleccionarResultadosByIdCarrera(int idCarrera)
		{
			return ResultadosDAL.SeleccionarResultadosByIdCarrera(idCarrera);
		}
	}
}
