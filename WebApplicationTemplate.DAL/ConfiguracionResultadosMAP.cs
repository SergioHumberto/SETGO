using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
	public static class ConfiguracionResultadosMAP
	{
		public static void InsertarConfiguracionResultado(ConfiguracionResultadosOBJ crOBJ)
		{
			Mapper.Instance().Insert("InsertarConfiguracionResultado", crOBJ);
		}

		public static int VerificarConfiguracionDeCarrera(ConfiguracionResultadosOBJ crOBJ)
		{
			return Mapper.Instance().QueryForObject<int>("VerificarConfiguracionDeCarrera", crOBJ);
		}

		public static void ActualizarConfiguracion(ConfiguracionResultadosOBJ crOBJ)
		{
			Mapper.Instance().Update("ActualizarConfiguracion", crOBJ);
		}

		public static ConfiguracionResultadosOBJ SeleccionarConfiguracionByIdCarreraIdCategoria(ConfiguracionResultadosOBJ crOBJ)
		{
			return Mapper.Instance().QueryForObject<ConfiguracionResultadosOBJ>("SeleccionarConfiguracionByIdCarreraIdCategoria", crOBJ);
		}

		public static void EliminarConfiguracionByIdCarreraIdCategoria(ConfiguracionResultadosOBJ crOBJ)
		{
			Mapper.Instance().Delete("EliminarConfiguracionByIdCarreraIdCategoria", crOBJ);
		}
	}
}
