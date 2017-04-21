using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
    public class SMTPConfigBLL
    {
        public SMTPConfigOBJ SelectSMTPConfig()
        {
            IList<SMTPConfigOBJ> lstSMTPConfig = SMTPConfigDAL.SelectSMTPConfig(new SMTPConfigOBJ());
            if(lstSMTPConfig.Count == 1)
            {
                return lstSMTPConfig[0];
            }
            else
                return null;
        }
    }
}
