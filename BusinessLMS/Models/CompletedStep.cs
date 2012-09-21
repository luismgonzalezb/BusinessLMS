using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class CompletedStep
    {
        public string IBONum { get; set; }
        public int stepId { get; set; }
        public System.DateTime datetime { get; set; }
        public virtual IBO IBO { get; set; }
        public virtual Step Step { get; set; }
    }
}
