using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Ticket
    {
        public int ticketId { get; set; }
        public int ticketType { get; set; }
        public string IBONum { get; set; }
        [Required]
        public string description { get; set; }
        public DateTime datetime { get; set; }
        public int priority { get; set; }
        public bool solved { get; set; }
        public string developer { get; set; }
        public string impact { get; set; }
    }
}
