using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MID_PLATFORM_CLIENT.Objects
{
    public class SM_Contracts
    {
        public List<SM_Contract>? SM_contracts { get; set; }

    }
    public class SM_Contract
    {
        public string? ContractId { get; set; }
        public string? Code { get; set; }
        public string? Instance { get; set; }
        public string? Type { get; set; }
        public string? Company { get; set; }
        public string? ContactPerson { get; set; }
        public string? Date { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? AllowExceededHours { get; set; }
        public string? BillableExceededHours { get; set; }
        public string? Status { get; set; }
        public string? Active { get; set; }
        public string? Timestamp { get; set; }
        public string? user { get; set; }
    }
}
