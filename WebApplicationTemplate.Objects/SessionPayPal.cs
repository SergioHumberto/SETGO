using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplicationTemplate.Objects
{
    public class SessionPayPal
    {
        public int IdCarrera { get; set; }
        public string item_name { get; set; }
        public decimal amount { get; set; }
        public string custom { get; set; }
        public string returnURL { get; set; }
        public string cancelURL { get; set; }
    }
}
