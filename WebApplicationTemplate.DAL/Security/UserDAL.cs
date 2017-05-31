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

		public static int ExisteUsername(User user)
		{
			return Mapper.Instance().QueryForObject<int>("ExisteUsername", user);
		}


		public static IList<User> SeleccionarUsuarios()
		{
			return Mapper.Instance().QueryForList<User>("SeleccionarUsuarios", null);
		}

		public static void EliminarUsuarioByIdUser(int idUser)
		{
			Mapper.Instance().Delete("EliminarUsuarioByIdUser",idUser);
		}

		public static User SeleccionarUsuarioByIdUser(int idUser)
		{
			return Mapper.Instance().QueryForObject<User>("SeleccionarUsuarioByIdUser", idUser);
		}

		public static void ModificarUsuario(User u)
		{
			Mapper.Instance().Update("ModificarUsuario", u);
		}
	}
}
