using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmWorkRecord
    {
        public int? WorkRecordId { get; set; }
        public int Task { get; set; }
        public int Type { get; set; }
        public int Agent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double WorkedHours { get; set; }
        public double BillableHours { get; set; }
        public double NonBillableHours { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
        [Timestamp]
        public byte[]? Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual SmAgent? AgentNavigation { get; set; } = null!;
        public virtual SmTask? TaskNavigation { get; set; } = null!;
        public virtual SmWorkRecordType? TypeNavigation { get; set; } = null!;
        public virtual User? UserNavigation { get; set; } = null!;
    }
}
