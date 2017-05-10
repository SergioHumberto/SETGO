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

		public static int VerificarResultadoDeCarrera(int IdCarrera)
		{
			return Mapper.Instance().QueryForObject<int>("VerificarResultadoDeCarrera", IdCarrera);
		}

		public static void EliminarResultadosDeCarrera(int IdCarrera)
		{
			Mapper.Instance().Delete("EliminarResultadosDeCarrera", IdCarrera);
		}

	}
}
