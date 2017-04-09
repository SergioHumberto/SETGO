using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationTemplate.Objects.Security;
using WebApplicationTemplate.BLL.Security;
using WebApplicationTemplate.BLL;
using System.Web.Security;

namespace WebApplicationTemplate.Web.Tools
{
    public static class HttpSecurity
    {
        /// <summary>
        /// Get the current user session 
        /// </summary>
        public static UserSession CurrentSession
        {
            get
            {
				HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);

                if ((cookie != null && String.IsNullOrEmpty(cookie.Value) == false))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                    byte[] token = Convert.FromBase64String(ticket.UserData);

                    UserSession objUserSession = new UserSession();

                    objUserSession.Authenticate(ticket.Name, token);

                    return objUserSession;
                }
                else
                {
                    return new UserSession();
                }
            }
        }

        /// <summary>
        /// Authenticate user session
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        public static void SignIn(String username, String password)
        {
            UserSession objUserSession = new UserSession();

            byte[] token = objUserSession.Authenticate(username, password);

            // Create the authentication ticket with custom user data.              
            CreateAuthenticationTicket(username, token);
        }

        /// <summary>
        /// Authenticate user session
        /// </summary>
        /// <param name="username">macrolynkGuid</param>
        public static void SignIn(User objUser)
        {
            Validations.ValidateObjectNotNull(objUser, "User");

            UserSession objUserSession = new UserSession();
            
            byte[] token = objUserSession.Authenticate(objUser);

            // Create the authentication ticket with custom user data.              
            CreateAuthenticationTicket(objUser.Username, token);
        }

        /// <summary>
        /// Create the authentication ticket with custom user data. 
        /// </summary>
        /// <param name="username">User Name</param>
        /// <param name="token">Token</param>
        private static void CreateAuthenticationTicket(String username, byte[] token)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    username,
                    DateTime.Now,
                    DateTime.Now.AddDays(AppSettings.FormsCookieExpirationDays),
                    false,
                    Convert.ToBase64String(token),
                    FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            // TODO add culture settings for the user
        }

        /// <summary>
        /// Deauthenticate user session 
        /// </summary>
        public static void SignOut()
        {
            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            // Clear authentication cookie.
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie frmCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                frmCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(frmCookie);
            }
        }

        /// <summary>
        /// Check if user have access to an element 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool HasElement(string code)
        {
            ElementBLL elementBLL = new ElementBLL(CurrentSession);

            if (CurrentSession.IsAuthenticated)
            {
                return elementBLL.HasElementByUser(code, CurrentSession.IdUser);
            }

            return false;
        }
    }
}