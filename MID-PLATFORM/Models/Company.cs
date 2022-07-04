using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class Company
    {
        public Company()
        {
            People = new HashSet<Person>();
            SmContracts = new HashSet<SmContract>();
        }

        public int? CompanyId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public bool? Active { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual Country? CountryNavigation { get; set; } = null!;
        public virtual User? UserNavigation { get; set; } = null!;
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<SmContract> SmContracts { get; set; }
    }
}
