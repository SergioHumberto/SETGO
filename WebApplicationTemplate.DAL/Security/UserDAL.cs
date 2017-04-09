using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using IBatisNet.DataMapper;

using WebApplicationTemplate.Objects.Security;

namespace WebApplicationTemplate.DAL.Security
{
	public static class UserDAL
	{
		public static User SelectUserByUsername(String username)
		{
            return DAL.QueryForObject<User>("SelectUserByUsername", username);
		}

        public static User SelectUserByMacrolynkGUID(Guid macrolynkGUID)
        {
            return DAL.QueryForObject<User>("SelectUserByMacrolynkGUID", macrolynkGUID);
        }

        public static User SelectUserById(int idUser)
        {
            return DAL.QueryForObject<User>("SelectUserById", idUser);
        }

        public static User SelectUserByUsernameAndSessionToken(String username, byte[] sessionToken)
        {
            return DAL.Statement("SelectUserByUsernameAndSessionToken")
                     .AddParameter("username", username)
                     .AddParameter("sessionToken", sessionToken)
                     .QueryForObject<User>();
        }

        public static int UpdateSessionToken(int idUser, byte[] sessionToken)
        {
            return DAL.Statement("UpdateSessionToken")
                     .AddParameter("idUser", idUser)
                     .AddParameter("sessionToken", sessionToken)
                     .Update();
        }

        public static void InsertUser(User objUser)
        {
			DAL.Insert("InsertUser", objUser);
        }

    }
}
