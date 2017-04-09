using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects.Security;

namespace WebApplicationTemplate.DAL.Security
{
	public static class ElementDAL
	{
		public static bool HasElementByUser(string code, int idUser)
		{
			return DAL.Statement("HasElementByUser")
				.AddParameter("code", code)
				.AddParameter("idUser", idUser)
				.QueryForObject<bool>();
		}
	}
}
