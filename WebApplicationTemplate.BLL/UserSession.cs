using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebApplicationTemplate.BLL.Security;
using WebApplicationTemplate.Objects.Security;
using WebApplicationTemplate.BLL.Properties;

using System.Security.Cryptography;

namespace WebApplicationTemplate.BLL
{
    public class UserSession
    {
        private int? idUser;

        /// <summary>
        /// Validate user data and generate the session token
        /// </summary>
        /// <param name="username">user username</param>
        /// <param name="password">user password</param>
        /// <returns>Session token</returns>
        public byte[] Authenticate(string username, string password)
        {
            Validations.ValidateUsername(username);
            Validations.ValidatePassword(password);

            UserBLL objUserBLL = new UserBLL(this);
            User objUser = objUserBLL.SelectUserByUsername(username);

			if(objUser == null || !(objUser.Username == username && MD5(password) == objUser.Password))
			{
				throw new BusinessLogicException(Resources.InvalidCredentials);
			}

            // TODO add sign-in logic for password
            //if ( ... )
            //{
            //    throw new BusinessLogicException(Resources.InvalidCredentials);
            //}

            return Authenticate(objUser);
        }

        /// <summary>
        /// Validate user data and generate the session token
        /// </summary>
        /// <param name="username">user username</param>
        /// <param name="password">Macrolynk Guid</param>
        /// <returns>Session token</returns>
        public byte[] Authenticate(User objUser)
        {
            Validations.ValidateObjectNotNull(objUser, "User");

            objUser.SessionToken = new byte[AppSettings.SessionTokenLenght];

            Random random = new Random();

            random.NextBytes(objUser.SessionToken);

            UserBLL objUserBLL = new UserBLL(this);

            objUserBLL.UpdateSessionToken(objUser);

            idUser = objUser.IdUser;

            return objUser.SessionToken;
        }

        /// <summary>
        /// Validate than actual session corresponds to the actual user
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="sessionToken">session token</param>
        public void Authenticate(string userName, byte[] sessionToken)
        {
            UserBLL objUserBLL = new UserBLL(this);
            
            User objUser = objUserBLL.SelectUserByUsernameAndSessionToken(userName, sessionToken);

            if (objUser == null)
            {
                throw new BusinessLogicException(Resources.InvalidUsernameOrSessionToken);
            }

            idUser = objUser.IdUser;
        }

		private string MD5(string word)
		{
			MD5 md5 = MD5CryptoServiceProvider.Create();
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] stream = null;
			StringBuilder sb = new StringBuilder();
			stream = md5.ComputeHash(encoding.GetBytes(word));
			for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
			return sb.ToString();
		}

		/// <summary>
		/// Deauthenticate user
		/// </summary>
		public void Deauthenticate()
        {
            idUser = null;
        }

        /// <summary>
        /// Check if user is authenticated
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return idUser != null;
            }
        }

        public int IdUser
        {
            get
            {
                if (idUser == null)
                {
                    throw new BusinessLogicException(Resources.UserNotAuthenticated);
                }

                return idUser.Value;
            }
            set
            {
                idUser = value;
            }
        }

    }
}
