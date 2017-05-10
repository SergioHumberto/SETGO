using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class ConfiguracionResultadosBLL
	{
		public void InsertarConfiguracionResultado(ConfiguracionResultadosOBJ crOBJ)
		{
			ConfiguracionResultadosMAP.InsertarConfiguracionResultado(crOBJ);
		}

		public bool VerificarConfiguracionDeCarrera(int idCarrera)
		{
			if(ConfiguracionResultadosMAP.VerificarConfiguracionDeCarrera(idCarrera) == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void ActualizarConfiguracion(ConfiguracionResultadosOBJ crOBJ)
		{
			ConfiguracionResultadosMAP.ActualizarConfiguracion(crOBJ);
		}
	}
}
