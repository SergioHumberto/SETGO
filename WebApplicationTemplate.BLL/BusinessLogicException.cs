using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.BLL
{
   public class BusinessLogicException : Exception
	{
		public BusinessLogicException(String message)
			: base(message)
		{
			/* do nothing */
		}

		public BusinessLogicException(String message, Exception innerException)
			: base(message, innerException)
		{
			/* do nothing */
		}

        public BusinessLogicException(Exception innerException)
			: base(innerException.Message ?? innerException.ToString(), innerException)
		{
			/* do nothing */
		}
	}
}
