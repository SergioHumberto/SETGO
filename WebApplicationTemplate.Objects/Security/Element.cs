using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects.Security
{
	public class Element
	{
		public int IdElement { get; set; }

		public int IdInterface { get; set; }

		public string Code { get; set; }

		public bool IsSelect { get; set; }

		public bool IsInsert { get; set; }

		public bool IsUpdate { get; set; }

		public bool IsDelete { get; set; }

		// TODO complete element data
	}
}

