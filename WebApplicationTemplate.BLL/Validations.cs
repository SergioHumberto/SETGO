using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTemplate.BLL.Properties;

namespace WebApplicationTemplate.BLL
{
   public static class Validations
    {
       public static void ValidateUsername(string username)
       {
           if (string.IsNullOrWhiteSpace(username))
           {
               throw new BusinessLogicException(Resources.InvalidUsername);
           }
       }

       public static void ValidatePassword(string password)
       {
           if (string.IsNullOrWhiteSpace(password))
           {
               throw new BusinessLogicException(Resources.InvalidPassword);
           }
       }

       public static void ValidateSession(UserSession session)
       {
           if (session == null)
           {
               throw new BusinessLogicException(Resources.SessionNotAvailable);
           }
       }

       public static void ValidateSessionToken(byte[] sessionToken)
       {
           if (sessionToken == null || sessionToken.Length != AppSettings.SessionTokenLenght)
           {
               throw new BusinessLogicException(Resources.InvalidSessionToken);
           }
       }

       public static void ValidateObjectNotNull(Object obj, string objectName)
       {
           if (obj == null)
           {
               throw new BusinessLogicException(string.Format(Resources.ObjectNotAvailable,objectName));
           }
       }

       public static void ValidateElementCode(string code)
       {
           if (string.IsNullOrWhiteSpace(code))
           {
               throw new BusinessLogicException(Resources.InvalidElementCode);
           }
       }
    }
}
