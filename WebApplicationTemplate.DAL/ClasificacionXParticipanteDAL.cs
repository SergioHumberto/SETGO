using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public class ClasificacionXParticipanteDAL
    {
        public static IList<ClasificacionXParticipanteOBJ> SelectClasificacionXParticipante(ClasificacionXParticipanteOBJ finder)
        {
            IList<ClasificacionXParticipanteOBJ> lstClasificacionXParticipante = DAL.QueryForList<ClasificacionXParticipanteOBJ>("SelectClasificacionXParticipante", finder);
            return lstClasificacionXParticipante;
        }

        public static int InsertClasificacionXParticipante(ClasificacionXParticipanteOBJ param)
        {
            DAL.Insert("InsertClasificacionXParticipante", param);
            return param.IdClasificacionXParticipante;
        }

    }
}
