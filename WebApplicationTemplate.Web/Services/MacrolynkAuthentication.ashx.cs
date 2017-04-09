using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using WebApplicationTemplate.Objects.Security;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.BLL.Security;
using WebApplicationTemplate.Web.Tools;
using WebApplicationTemplate.Web.MLWSUserValidations;

namespace WebApplicationTemplate.Web.Services
{
    /// <summary>
    /// Descripción breve de WebAuthentication
    /// </summary>
    public class MacrolynkAuthentication : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String returnUrl;

            try
            {
                String macrolynkGuid = context.Request.QueryString["GD"];
                String isSession = context.Request.QueryString["IS"];
                String culture = context.Request.QueryString["CU"]; //TODO Add logic for the culture

                // create binding and endpoint for connecting to the WS
                BasicHttpBinding binding = HttpTool.CreateBinding(WebSettings.MLWSUserValidations);
                EndpointAddress endPoint = HttpTool.CreateEndpointAddress(WebSettings.MLWSUserValidations);

                Boolean authenticatedML;

                // create web service client using the binding and end point
                using (UserValidationsSoapClient WSUser = new UserValidationsSoapClient(binding, endPoint))
                {
                    // perform the WS call
                    authenticatedML = WSUser.ValidateGuid(macrolynkGuid, isSession);
                }

                // if macrolynk says the user is authenticated
                if (authenticatedML)
                {
                    Guid objGuid = new Guid(macrolynkGuid);
                    UserBLL bllUser = new UserBLL(HttpSecurity.CurrentSession);
                    User objUser = bllUser.SelectUserByMacrolynkGUID(objGuid);

                    // check user exsits
                    if (objUser == null)
                    {
                        throw new BusinessLogicException(Resources.Global.MacrolynkUserNotExist);
                    }

                    // sign-in user
                    HttpSecurity.SignIn(objUser);
                    
                    returnUrl = Urls.Home();
                }
                else
                {
                    throw new WebException(Resources.Global.MacrolynkUserNotAuthenticated);
                }
            }
            catch (Exception ex)
            {
                returnUrl = Urls.Format(WebSettings.MLAppAccessError, ex.Message);
            }
            
            HttpTool.Redirect(returnUrl);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}