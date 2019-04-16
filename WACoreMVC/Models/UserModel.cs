using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WACoreMVC.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Expire { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
    }
}
