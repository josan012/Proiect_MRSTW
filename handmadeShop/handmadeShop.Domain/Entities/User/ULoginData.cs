using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handmadeShop.Domain.Entities.User
{
    public class ULoginData
    {
        public int Credential { get; set; }
        public int Password { get; set; }
        public int LoginIp { get; set; }
        public int LoginDataTime { get; set; }
    }
}
