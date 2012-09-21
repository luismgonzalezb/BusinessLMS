using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class Step
    {
        public int stepId { get; set; }
        public Nullable<int> parentStepId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int stepOrder { get; set; }
    }
}
