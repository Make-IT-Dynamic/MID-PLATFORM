using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmWorkRecordType
    {
        public SmWorkRecordType()
        {
            SmWorkRecords = new HashSet<SmWorkRecord>();
        }

        public int WorkRecordTypeId { get; set; }
        public string Description { get; set; } = null!;
        public bool? Billable { get; set; }
        public bool? Active { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<SmWorkRecord> SmWorkRecords { get; set; }
    }
}
