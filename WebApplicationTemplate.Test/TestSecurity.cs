using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Test.Properties;

namespace WebApplicationTemplate.Test
{
    public static class TestSecurity
    {
        private static UserSession currentSession;

        public static UserSession CurrentSession
        {
            get
            {
                if (currentSession == null)
                {
                    currentSession = new UserSession();

                    currentSession.Authenticate(Settings.Default.Username, Settings.Default.Password);
                }

                return currentSession;
            }
        }
    }
}
