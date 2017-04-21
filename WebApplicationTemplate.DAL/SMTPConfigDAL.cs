using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
    public static class SMTPConfigDAL
    {
        public static IList<SMTPConfigOBJ> SelectSMTPConfig(SMTPConfigOBJ p_SMTPConfigOBJ)
        {
            IList<SMTPConfigOBJ> lstSMTPConfig = Mapper.Instance().QueryForList<SMTPConfigOBJ>("SelectSMTPConfig", p_SMTPConfigOBJ);
            return lstSMTPConfig;
        }
    }
}
