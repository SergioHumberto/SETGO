using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class SMTPConfigOBJ
    {
        public int IdSMTPConfig { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
