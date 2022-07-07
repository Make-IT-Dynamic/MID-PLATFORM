using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmContractLimit
    {
        public int? ContractLimitsId { get; set; }
        public int? Contract { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public double Value { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        [Timestamp]
        public byte[]? Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual SmContractType? ContractNavigation { get; set; }
        public virtual User? UserNavigation { get; set; } = null!;
    }
}
