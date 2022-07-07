using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmTaskStatus
    {
        public SmTaskStatus()
        {
            SmTasks = new HashSet<SmTask>();
        }

        public int? StatusId { get; set; }
        public string Description { get; set; } = null!;
        public bool Closed { get; set; }
        public bool? Active { get; set; }
        [Timestamp]
        public byte[]? Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User? UserNavigation { get; set; } = null!;
        public virtual ICollection<SmTask> SmTasks { get; set; }
    }
}
