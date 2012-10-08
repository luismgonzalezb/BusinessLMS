using System;
using System.Collections.Generic;

namespace BusinessLMSWeb.Models
{
    public class Step
    {
        public int stepId { get; set; }
        public Nullable<int> parentStepId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int stepOrder { get; set; }
        public string iconClass { get; set; }
        public string action { get; set; }
        public string controller { get; set; }
    }
}
