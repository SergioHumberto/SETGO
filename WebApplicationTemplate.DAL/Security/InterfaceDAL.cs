using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects.Security;

namespace WebApplicationTemplate.DAL.Security
{
	public static class InterfaceDAL
	{
		public static IList<Interface> SearchInterfaces(String codeLike)
		{
			return DAL.Statement("SearchInterfaces")
				.AddParameter("codeLike", codeLike)
				.QueryForList<Interface>();
		}
	}
}
