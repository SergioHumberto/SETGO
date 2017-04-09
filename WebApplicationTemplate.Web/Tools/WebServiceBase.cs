using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using WebApplicationTemplate.BLL;

namespace WebApplicationTemplate.Web.Tools
{
    abstract public class WebServiceBase : System.Web.Services.WebService
    {
        protected const String CREDENTIALS = "Credentials";

        public WebServiceCredentials Credentials;

        private UserSession session;

        protected UserSession CurrentSession
        {
            get
            {
                if (Credentials == null)
                {
                    throw new WebException(Resources.Global.MissingSoapHeader);
                }

                if (session == null)
                {
                    session = new UserSession();
                    session.Authenticate(Credentials.Username, Credentials.Password);
                }

                return session;
            }
        }
    }
}