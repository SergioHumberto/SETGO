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
    }
}