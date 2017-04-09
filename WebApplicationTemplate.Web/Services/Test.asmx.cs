using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplicationTemplate.Web.Tools;
using System.Web.Services.Protocols;

namespace WebApplicationTemplate.Web.Services
{
    /// <summary>
    /// Descripción breve de Test
    /// </summary>
    [WebService(Namespace = "http://www.macrolynk.com/ns/WebApplicationTemplate")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Test : WebServiceBase
    {

        [WebMethod, SoapHeader(CREDENTIALS)]
        public string WebMethodTest(string parameterTest)
        {
            return String.Format(Resources.Global.WebServiceTest, CurrentSession.IsAuthenticated, parameterTest);
        }

    }
}
