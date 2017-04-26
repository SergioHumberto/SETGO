using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Reflection;
using System.Xml;

using System.Configuration;

namespace WebApplicationTemplate.Web.Tools
{
    /// <summary>
    /// Static class to retrieve the settings defined in the WebSettings.config file.
    /// </summary>
    public static class WebSettings
    {
        private static String GetValue(String key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Returns the URL of the MACROLYNK web service for validating the authentication.
        /// </summary>
        /// <returns></returns>
        public static String MLWSUserValidations
        {
            get
            {
                return GetValue("MLWSUserValidations");
            }
        }

        /// <summary>
        /// Returns the URL for displaying an error message to MACROLYNK users in APPS ACCESS.
        /// </summary>
        public static String MLAppAccessError
        {
            get
            {
                return GetValue("MLAppAccessError");
            }
        }

        /// <summary>
        /// Regresa la URL del root de la aplicacion en web
        /// </summary>
        public static String URLApplicationPath
        {
            get { return GetValue("URLApplicationPath"); }
        }

        public static String ClientIDPayPalRestAPI
        {
            get { return GetValue("ClientIDPayPalRestAPI"); }
        }

        public static String ClientSecretPayPalRestAPI
        {
            get { return GetValue("ClientSecretPayPalRestAPI"); }
        }

        public static String ModePayPayRestAPI
        {
            get { return GetValue("ModePayPayRestAPI"); }
        }

        public static String DefaultRegistrationURL
        {
            get { return GetValue("DefaultRegistrationURL"); }
        }

        public static String ModePayPalClassic
        {
            get { return GetValue("ModePayPalClassic"); }
        }

        public static String ApiUsername
        {
            get { return GetValue("ApiUsername"); }
        }

        public static String ApiPassword
        {
            get { return GetValue("ApiPassword"); }
        }

        public static String ApiSignature
        {
            get { return GetValue("ApiSignature"); }
        }

        public static String PAYPAL_REDIRECT_URL
        {
            get { return GetValue("PAYPAL_REDIRECT_URL"); }
        }

    }
}