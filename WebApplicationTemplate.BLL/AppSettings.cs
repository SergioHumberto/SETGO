using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.BLL
{
   public static class AppSettings
    {
       /// <summary>
       /// Session token Lenght
       /// </summary>
       public static int SessionTokenLenght
       {
           get
           {
               return 50;
           }
       }
       /// <summary>
       /// Expiration days for Cookie forms
       /// </summary>
       public static double FormsCookieExpirationDays
       {
           get
           {
               return 30d;
           }
       }
    }
}
