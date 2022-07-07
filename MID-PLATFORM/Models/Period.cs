using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class Period
    {
        public int? PeriodId { get; set; }
        public string Code { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? ActiveForSm { get; set; }
        public bool Active { get; set; }
        [Timestamp]
        public byte[]? Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User? UserNavigation { get; set; } = null!;
    }
}
