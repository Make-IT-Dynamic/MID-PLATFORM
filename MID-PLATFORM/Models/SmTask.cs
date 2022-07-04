using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmTask
    {
        public SmTask()
        {
            SmWorkRecords = new HashSet<SmWorkRecord>();
        }

        public int TaskId { get; set; }
        public int? Contract { get; set; }
        public int? Type { get; set; }
        public int Requester { get; set; }
        public string? CreatedBy { get; set; }
        public int AssignedTo { get; set; }
        public string Subject { get; set; } = null!;
        public string? Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public int? Category { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ReplyDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public double TotalHoursEstimated { get; set; }
        public double RemainingHoursEstimaded { get; set; }
        public bool Canceled { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual SmAgent AssignedToNavigation { get; set; } = null!;
        public virtual Category? CategoryNavigation { get; set; }
        public virtual SmContractType? ContractNavigation { get; set; }
        public virtual User? CreatedByNavigation { get; set; }
        public virtual SmPriority PriorityNavigation { get; set; } = null!;
        public virtual Person RequesterNavigation { get; set; } = null!;
        public virtual SmTaskStatus StatusNavigation { get; set; } = null!;
        public virtual SmTaskType? TypeNavigation { get; set; }
        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<SmWorkRecord> SmWorkRecords { get; set; }
    }
}
