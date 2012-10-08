using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Dream
    {
        public int dreamId { get; set; }
        public string IBONum { get; set; }
        public int timeframeId { get; set; }
        public int areaId { get; set; }
        [Required]
        public string dream1 { get; set; }
        public int dreamLevel { get; set; }
        public bool achieved { get; set; }
        public System.DateTime datetime { get; set; }
        public string picture { get; set; }
    }
}
