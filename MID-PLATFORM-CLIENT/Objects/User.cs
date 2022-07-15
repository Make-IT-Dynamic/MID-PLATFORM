using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MID_PLATFORM_CLIENT.Objects
{
    public class Users
    {
        public List<User>? users { get; set; }

    }
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Active { get; set; }
        public string? Timestamp { get; set; }
        public string? user { get; set; }
    }
}