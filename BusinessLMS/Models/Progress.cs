using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class Progress
    {
        public long ProgressId { get; set; }
        public int GoalId { get; set; }
        public decimal progress { get; set; }
        public DateTime datetime { get; set; }
    }
}
