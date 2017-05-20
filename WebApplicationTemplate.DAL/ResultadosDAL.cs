using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
	public static class ResultadosDAL
	{
		public static void InsertarResultado(ResultadosOBJ resultadosOBJ)
		{
			Mapper.Instance().Insert("InsertarResultado", resultadosOBJ);
		}

		public static int VerificarResultadoDeCarrera(int idConfiguracionResultados)
		{
			return Mapper.Instance().QueryForObject<int>("VerificarResultadoDeCarrera", idConfiguracionResultados);
		}

		public static void EliminarResultadosByIdConfiguracionResultados(int IdConfiguracionResultados)
		{
			Mapper.Instance().Delete("EliminarResultadosByIdConfiguracionResultados", IdConfiguracionResultados);
		}

		public static IList<ResultadosOBJ> SeleccionarResultadosByConfiguracionResultados(int idConfiguracionResultados)
		{
			return Mapper.Instance().QueryForList<ResultadosOBJ>("SeleccionarResultadosByConfiguracionResultados", idConfiguracionResultados);
		}

	}
}
