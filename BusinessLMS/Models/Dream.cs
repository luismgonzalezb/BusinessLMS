using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class Dream
    {
        public int dreamId { get; set; }
        public string IBONum { get; set; }
        public int timeframeId { get; set; }
        public int areaId { get; set; }
        public string dream1 { get; set; }
        public byte dreamLevel { get; set; }
        public bool achieved { get; set; }
        public System.DateTime datetime { get; set; }
        public virtual Area Area { get; set; }
        public virtual IBO IBO { get; set; }
        public virtual Timeframe Timeframe { get; set; }
    }
}
