using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.BLL.Security;
using WebApplicationTemplate.BLL.Properties;
using WebApplicationTemplate.Objects.Security;
using WebApplicationTemplate.Objects.WebService;
using WebApplicationTemplate.Web.Tools;

namespace WebApplicationTemplate.Web.Services
{
    /// <summary>
    /// Web service to perform actions related to users.
    /// </summary>
	[WebService(Namespace = "http://www.macrolynk.com/ns/WebApplicationTemplate")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Users : WebServiceBase
    {
        [WebMethod, SoapHeader(CREDENTIALS)]
        public string RegisterUser(UserInfo userInfo)
        {
            UserBLL bllUser = new UserBLL(CurrentSession);

			// prepare object to insert
			User objUser = new User();
			objUser.Username = userInfo.Username;
			//objUser.MacrolynkGUID = userInfo.MacrolynkGUID;
			
			// insert new user
            bllUser.InsertUser(objUser);
            
            return String.Format(Resources.Global.UserCreatedSuccessfully,objUser.Username);
        }
    }
}
