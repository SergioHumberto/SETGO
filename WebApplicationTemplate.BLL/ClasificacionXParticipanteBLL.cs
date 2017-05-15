using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
    public class ClasificacionXParticipanteBLL : BaseBLL
    {
        public ClasificacionXParticipanteBLL(UserSession session) : base(session) { }

        public IList<ClasificacionXParticipanteOBJ> SelectClasificacionXParticipante(ClasificacionXParticipanteOBJ finder)
        {
            return ClasificacionXParticipanteDAL.SelectClasificacionXParticipante(finder);
        }

        public int InsertClasificacionXParticipante(ClasificacionXParticipanteOBJ param)
        {
            return ClasificacionXParticipanteDAL.InsertClasificacionXParticipante(param);
        }
    }
}
