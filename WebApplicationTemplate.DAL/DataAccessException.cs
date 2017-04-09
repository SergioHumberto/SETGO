using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.DAL
{
	public class DataAccessException : Exception
	{
		public DataAccessException(String message)
			: base(message)
		{
			/* do nothing */
		}

		public DataAccessException(String message, Exception innerException)
			: base(message, innerException)
		{
			/* do nothing */
		}

		public DataAccessException(Exception innerException)
			: base(innerException.Message ?? innerException.ToString(), innerException)
		{
			/* do nothing */
		}
	}
}
