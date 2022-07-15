using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MID_PLATFORM_CLIENT.Objects
{
    public class SM_ContractLimits
    {
        public List<SM_ContractLimit>? SM_contractLimits { get; set; }

    }
    public class SM_ContractLimit
    {
        public string? ContractLimitsId { get; set; }
        public string? Contract { get; set; }
        public string? Date { get; set; }
        public string? Quantity { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }
        public string? Document { get; set; }
        public string? Active { get; set; }
        public string? Timestamp { get; set; }
        public string? user { get; set; }
    }
}
