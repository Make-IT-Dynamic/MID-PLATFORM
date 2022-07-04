using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MID_PLATFORM.Models
{
    public partial class Country
    {
        public Country()
        {
            Companies = new HashSet<Company>();
        }

        public int CountryId { get; set; }
        public string CountryCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }
        [Timestamp]
        public byte[]? Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual User? UserNavigation { get; set; } = null!;
        public virtual ICollection<Company> Companies { get; set; }
    }
}
