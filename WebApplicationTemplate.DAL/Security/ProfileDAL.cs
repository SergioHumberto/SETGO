using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects.Security;

namespace WebApplicationTemplate.DAL.Security
{
	public static class ProfileDAL
	{
		public static void InsertProfile(Profile profile)
		{
			DAL.Insert("InsertProfile", profile);
		}
	}
}
