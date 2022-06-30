using System;
using System.Collections.Generic;

namespace MID_PLATFORM.Models
{
    public partial class SmContract
    {
        public int ContractId { get; set; }
        public string Code { get; set; } = null!;
        public int Instance { get; set; }
        public int Type { get; set; }
        public int Company { get; set; }
        public int? ContactPerson { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; } = null!;
        public int Category { get; set; }
        public string? Description { get; set; }
        public bool AllowExceededHours { get; set; }
        public bool BillableExceededHours { get; set; }
        public int Status { get; set; }
        public bool? Active { get; set; }
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual Category CategoryNavigation { get; set; } = null!;
        public virtual Company CompanyNavigation { get; set; } = null!;
        public virtual Person? ContactPersonNavigation { get; set; }
        public virtual SmContractStatus StatusNavigation { get; set; } = null!;
        public virtual SmContractType TypeNavigation { get; set; } = null!;
        public virtual User UserNavigation { get; set; } = null!;
    }
}
