using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.DAL.Security;

using WebApplicationTemplate.Objects.Security;

namespace WebApplicationTemplate.BLL.Security
{
	public class ElementBLL : BaseBLL
	{
		public ElementBLL(UserSession session) : base(session) { /* do nothing */ }
        
        /// <summary>
        /// Check if the user have access to an element
        /// </summary>
        /// <param name="code">element code</param>
        /// <param name="idUser">user id</param>
        /// <returns>true: access granted, false: denied</returns>
		public bool HasElementByUser(string code, int idUser)
		{
            Validations.ValidateElementCode(code);
            User user = UserDAL.SelectUserById(idUser);
            if (user != null && user.IsSuperUser)
            {
                return true;
            }
			return ElementDAL.HasElementByUser(code, idUser);
		}
	}
}
