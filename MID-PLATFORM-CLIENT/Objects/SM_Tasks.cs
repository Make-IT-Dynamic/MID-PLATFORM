using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MID_PLATFORM_CLIENT.Objects
{
    public class SM_Tasks
    {
        public List<SM_Task>? SM_tasks { get; set; }

    }
    public class SM_Task
    {
        public string? TaskId { get; set; }
        public string? Contract { get; set; }
        public string? Type { get; set; }
        public string? Requester { get; set; }
        public string? CreatedBy { get; set; }
        public string? AssignedTo { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? CreationDate { get; set; }
        public string? ReplyDate { get; set; }
        public string? ClosedDate { get; set; }
        public string? TotalHoursEstimated { get; set; }
        public string? RemainingHoursEstimaded { get; set; }
        public string? Active { get; set; }
        public string? Timestamp { get; set; }
        public string? user { get; set; }
    }
}
