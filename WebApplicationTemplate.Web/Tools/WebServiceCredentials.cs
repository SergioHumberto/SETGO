using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace WebApplicationTemplate.Web.Tools
{
    public class WebServiceCredentials : SoapHeader
    {
        public string Username;
        public string Password;
    }
}