using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class Person
    {
        public Person()
        {
            SmContracts = new HashSet<SmContract>();
            SmTasks = new HashSet<SmTask>();
        }

        public int PersonId { get; set; }
        public int Company { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool? Active { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual Company CompanyNavigation { get; set; } = null!;
        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<SmContract> SmContracts { get; set; }
        public virtual ICollection<SmTask> SmTasks { get; set; }
    }
}
