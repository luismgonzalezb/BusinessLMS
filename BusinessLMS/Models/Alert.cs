using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class Alert
    {
        public string AlertId { get; set; }
        public string AlertMsg { get; set; }
        public string action { get; set; }
        public DateTime datetime { get; set; }
    }
}
