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

		public bool VerificarResultadoDeCarrera(int idConfiguracionResultados)
		{
			if(ResultadosDAL.VerificarResultadoDeCarrera(idConfiguracionResultados) == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public void EliminarResultadosByIdConfiguracionResultados(int IdConfiguracionResultados)
		{
			ResultadosDAL.EliminarResultadosByIdConfiguracionResultados(IdConfiguracionResultados);
		}

		public IList<ResultadosOBJ> SeleccionarResultadosByConfiguracionResultados(int idConfiguracionResultados)
		{
			return ResultadosDAL.SeleccionarResultadosByConfiguracionResultados(idConfiguracionResultados);
		}
	}
}
