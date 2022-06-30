using System;
using System.Collections.Generic;

namespace MID_PLATFORM.Models
{
    public partial class SmContractStatus
    {
        public SmContractStatus()
        {
            SmContracts = new HashSet<SmContract>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; } = null!;
        public bool Closed { get; set; }
        public bool? Active { get; set; }
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<SmContract> SmContracts { get; set; }
    }
}
