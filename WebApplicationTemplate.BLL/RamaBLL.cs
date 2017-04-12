using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
	public class RamaBLL : BaseBLL
	{
        public RamaBLL(UserSession session) : base(session) { /* do nothing */ }

        public IList<RamaOBJ> SelectRama(RamaOBJ p_RamaOBJ)
        {
            return RamaDAL.SelectRama(p_RamaOBJ);
        }
    }
}
