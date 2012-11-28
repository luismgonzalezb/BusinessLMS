using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int ticketType { get; set; }
        public string IBONum { get; set; }
        public string description { get; set; }
        public DateTime datetime { get; set; }
        public int priority { get; set; }
        public bool solved { get; set; }
        public string developer { get; set; }
        public string impact { get; set; }
    }
}
