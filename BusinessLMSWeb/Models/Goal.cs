using System;
using System.Collections.Generic;

namespace BusinessLMSWeb.Models
{
    public class Goal
    {
        public int goalId { get; set; }
        public int dreamId { get; set; }
        public int timeframeId { get; set; }
        public int toolId { get; set; }
        public decimal goal1 { get; set; }
        public bool completed { get; set; }
        public System.DateTime datetime { get; set; }
    }
}
