using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects.Security
{
	public class User
	{
		public int IdUser { get; set; }

        public string Username { get; set; }

        public byte[] SessionToken { get; set; }

        public bool IsSuperUser { get; set; }

        //public Nullable<Guid> MacrolynkGUID { get; set; }
		public string Password { get; set; }

		public string Nombre { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string Email { get; set; }
	}
}
