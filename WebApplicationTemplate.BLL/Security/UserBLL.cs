using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.DAL.Security;
using WebApplicationTemplate.Objects.Security;

namespace WebApplicationTemplate.BLL.Security
{
	public class UserBLL : BaseBLL
	{
		public UserBLL(UserSession session) : base(session) { /* do nothing */ }
        
        /// <summary>
        /// Search an user by the username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user object</returns>
        public User SelectUserByUsername(string username)
        {
           Validations.ValidateUsername(username);
           return  UserDAL.SelectUserByUsername(username);
        }


        /// <summary>
        /// Search an user by the MacrolynkGUID
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>user object</returns>
        public User SelectUserByMacrolynkGUID(Guid macrolynkGUID)
        {
            return UserDAL.SelectUserByMacrolynkGUID(macrolynkGUID);
        }

        /// <summary>
        /// Search an user by the username and actual session token
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="sessionToken">actual session token from user</param>
        /// <returns>user object</returns>
        public User SelectUserByUsernameAndSessionToken(string username, byte[] sessionToken)
        {
            Validations.ValidateUsername(username);
            Validations.ValidateSessionToken(sessionToken);
            return UserDAL.SelectUserByUsernameAndSessionToken(username, sessionToken);
        }

        /// <summary>
        /// Update the session token to user
        /// </summary>
        /// <param name="user">user object</param>
        /// <returns>user id updated</returns>
        public int UpdateSessionToken(User user)
        {
            Validations.ValidateObjectNotNull(user, "User");
            Validations.ValidateSessionToken(user.SessionToken);
            return UserDAL.UpdateSessionToken(user.IdUser, user.SessionToken);
        }

        public void InsertUser(User user)
        {
            Validations.ValidateObjectNotNull(user, "User");
            Validations.ValidateUsername(user.Username);
            UserDAL.InsertUser(user);
        }

		public bool ExisteUsername(User user)
		{
			if (UserDAL.ExisteUsername(user) == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public IList<User> SeleccionarUsuarios()
		{
			return UserDAL.SeleccionarUsuarios();
		}

		public void EliminarUsuarioByIdUser(int idUser)
		{
			UserDAL.EliminarUsuarioByIdUser(idUser);
		}

		public User SeleccionarUsuarioByIdUser(int idUser)
		{
			return UserDAL.SeleccionarUsuarioByIdUser(idUser);
		}

		public void ModificarUsuario(User u)
		{
			UserDAL.ModificarUsuario(u);
		}

	}
}
