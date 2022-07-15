using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MID_PLATFORM_CLIENT.Objects
{
    public class Companies
    {
        public List<Company>? companies { get; set; }

    }
    public class Company
    {
        public string? CompanyId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Active { get; set; }
        public string? Timestamp { get; set; }
        public string? User { get; set; }
    }
}
