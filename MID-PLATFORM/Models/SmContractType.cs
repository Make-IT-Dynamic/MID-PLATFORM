using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class SmContractType
    {
        public SmContractType()
        {
            SmContractLimits = new HashSet<SmContractLimit>();
            SmContracts = new HashSet<SmContract>();
            SmTasks = new HashSet<SmTask>();
        }

        public int? ContractTypeId { get; set; }
        public string Description { get; set; } = null!;
        public bool AllowExeedHours { get; set; }
        public bool BillableExceedHours { get; set; }
        public bool? Active { get; set; }
        [Timestamp]
        public byte[]? Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User? UserNavigation { get; set; } = null!;
        public virtual ICollection<SmContractLimit> SmContractLimits { get; set; }
        public virtual ICollection<SmContract> SmContracts { get; set; }
        public virtual ICollection<SmTask> SmTasks { get; set; }
    }
}
