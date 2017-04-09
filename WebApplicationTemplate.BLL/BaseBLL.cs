using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.BLL
{
	public class BaseBLL
	{
		protected readonly UserSession session;

		public BaseBLL(UserSession session)
		{
            Validations.ValidateSession(session);

			this.session = session;
		}
	}
}
