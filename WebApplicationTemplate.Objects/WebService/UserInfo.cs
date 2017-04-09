using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects.WebService
{
	public class UserInfo
	{
		public string Username { get; set; }

		public Nullable<Guid> MacrolynkGUID { get; set; }

        public string PublicName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Culture { get; set; }
	}
}
