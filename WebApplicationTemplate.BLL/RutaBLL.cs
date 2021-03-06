﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class RutaBLL : BaseBLL
    {
        public RutaBLL(UserSession session) : base(session) { /* do nothing */ }

        public RutaOBJ SelectRutaByIdParticipante(int IdParticipante)
        {
            return RutaDAL.SelectRutaByIdParticipante(IdParticipante);
        }
        public void InsertarRuta(RutaOBJ param)
        {
            RutaDAL.InsertarRuta(param);
        }
        public void UpdateRuta(RutaOBJ param)
        {
            RutaDAL.UpdateRuta(param);
        }
        public void DeleteRuta(RutaOBJ param)
        {
            RutaDAL.DeleteRuta(param);
        }

		public IList<RutaOBJ> SeleccionarRutasByIdCategoria(RutaOBJ param)
		{
			return RutaDAL.SeleccionarRutasByIdCategoria(param);
		}

        public IList<RutaOBJ> SeleccionarRutasByIdCarrera(RutaOBJ param)
        {
            return RutaDAL.SeleccionarRutasByIdCarrera(param);
        }
    }
}
