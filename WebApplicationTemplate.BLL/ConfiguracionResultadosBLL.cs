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

		public bool VerificarConfiguracionDeCarrera(ConfiguracionResultadosOBJ crOBJ)
		{
			if(ConfiguracionResultadosMAP.VerificarConfiguracionDeCarrera(crOBJ) == 1)
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

		public ConfiguracionResultadosOBJ SeleccionarConfiguracionByIdCarreraIdCategoria(ConfiguracionResultadosOBJ crOBJ)
		{
			return ConfiguracionResultadosMAP.SeleccionarConfiguracionByIdCarreraIdCategoria(crOBJ);
		}

		public void EliminarConfiguracionByIdCarreraIdCategoria(ConfiguracionResultadosOBJ crOBJ)
		{
			ConfiguracionResultadosMAP.EliminarConfiguracionByIdCarreraIdCategoria(crOBJ);
		}

        public ConfiguracionResultadosOBJ SelectConfiguracionResultadosObject(int IdConfiguracionResultados)
        {
            return ConfiguracionResultadosMAP.SelectConfiguracionResultadosObject(IdConfiguracionResultados);
        }

    }
}
