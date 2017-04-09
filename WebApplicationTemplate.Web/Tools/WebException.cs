using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTemplate.Web.Tools
{
    public class WebException : Exception
	{
		public WebException(String message)
			: base(message)
		{
			/* do nothing */
		}

		public WebException(String message, Exception innerException)
			: base(message, innerException)
		{
			/* do nothing */
		}

        public WebException(Exception innerException)
			: base(innerException.Message ?? innerException.ToString(), innerException)
		{
			/* do nothing */
		}
	}
}