using System;
using System.Collections.Generic;

namespace MID_PLATFORM.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParentNavigation = new HashSet<Category>();
            SmContracts = new HashSet<SmContract>();
            SmTasks = new HashSet<SmTask>();
        }

        public int CategoryId { get; set; }
        public int? Parent { get; set; }
        public string Code { get; set; } = null!;
        public string LongCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string LongDescription { get; set; } = null!;
        public bool Title { get; set; }
        public bool? Active { get; set; }
        public byte[] Timestamp { get; set; } = null!;
        public string User { get; set; } = null!;

        public virtual Category? ParentNavigation { get; set; }
        public virtual User UserNavigation { get; set; } = null!;
        public virtual ICollection<Category> InverseParentNavigation { get; set; }
        public virtual ICollection<SmContract> SmContracts { get; set; }
        public virtual ICollection<SmTask> SmTasks { get; set; }
    }
}
