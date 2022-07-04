using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmPriority
    {
        public SmPriority()
        {
            SmTasks = new HashSet<SmTask>();
        }

        public int PriorityId { get; set; }
        public string Description { get; set; } = null!;
        public bool? Active { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<SmTask> SmTasks { get; set; }
    }
}
