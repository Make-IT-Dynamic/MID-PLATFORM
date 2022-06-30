using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MID_PLATFORM.Models;
using Newtonsoft.Json;

namespace MID_PLATFORM.Models
{
    public partial class User
    {
        public User()
        {
            Categories = new HashSet<Category>();
            Companies = new HashSet<Company>();
            Countries = new HashSet<Country>();
            InverseUser1Navigation = new HashSet<User>();
            People = new HashSet<Person>();
            Periods = new HashSet<Period>();
            SmAgents = new HashSet<SmAgent>();
            SmContractLimits = new HashSet<SmContractLimit>();
            SmContractStatuses = new HashSet<SmContractStatus>();
            SmContractTypes = new HashSet<SmContractType>();
            SmContracts = new HashSet<SmContract>();
            SmPriorities = new HashSet<SmPriority>();
            SmTaskCreatedByNavigations = new HashSet<SmTask>();
            SmTaskStatuses = new HashSet<SmTaskStatus>();
            SmTaskTypes = new HashSet<SmTaskType>();
            SmTaskUserNavigations = new HashSet<SmTask>();
            SmWorkRecordTypes = new HashSet<SmWorkRecordType>();
            SmWorkRecords = new HashSet<SmWorkRecord>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool? Active { get; set; }
        public byte[]? Timestamp { get; set; }// = null!;
        public string User1 { get; set; } = null!;

        public virtual User? User1Navigation { get; set; }// = null!;
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<User> InverseUser1Navigation { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
        public virtual ICollection<SmAgent> SmAgents { get; set; }
        public virtual ICollection<SmContractLimit> SmContractLimits { get; set; }
        public virtual ICollection<SmContractStatus> SmContractStatuses { get; set; }
        public virtual ICollection<SmContractType> SmContractTypes { get; set; }
        public virtual ICollection<SmContract> SmContracts { get; set; }
        public virtual ICollection<SmPriority> SmPriorities { get; set; }
        public virtual ICollection<SmTask> SmTaskCreatedByNavigations { get; set; }
        public virtual ICollection<SmTaskStatus> SmTaskStatuses { get; set; }
        public virtual ICollection<SmTaskType> SmTaskTypes { get; set; }
        public virtual ICollection<SmTask> SmTaskUserNavigations { get; set; }
        public virtual ICollection<SmWorkRecordType> SmWorkRecordTypes { get; set; }
        public virtual ICollection<SmWorkRecord> SmWorkRecords { get; set; }
	
    }
}
