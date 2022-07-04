using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmAgent
    {
        public SmAgent()
        {
            SmTasks = new HashSet<SmTask>();
            SmWorkRecords = new HashSet<SmWorkRecord>();
        }

        public int AgentId { get; set; }
        public string Code { get; set; } = null!;
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public double HourCost { get; set; }
        public bool? Active { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<SmTask> SmTasks { get; set; }
        public virtual ICollection<SmWorkRecord> SmWorkRecords { get; set; }
    }
}
