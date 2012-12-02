using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Ticket
    {
        [Display(Name = "Ticket Id")]
        public int ticketId { get; set; }

        [Display(Name = "Ticket Type")]
        public int ticketType { get; set; }

        [Display(Name = "IBO Num")]
        public string IBONum { get; set; }


        [Required]
        [Display(Name = "Desciption of the Problem")]
        [DataType(DataType.Text)]
        public string description { get; set; }

        [Required]
        [Display(Name = "Date and Time of the problem")]
        [DataType(DataType.Text)]
        public DateTime datetime { get; set; }

        [Required]
        [Display(Name = "Desciption of the Problem")]
        public int priority { get; set; }

        [Required]
        [Display(Name = "Solved Problem")]
        public bool solved { get; set; }

        [Required]
        [Display(Name = "Name Developer")]
        [DataType(DataType.Text)]
        public string developer { get; set; }

        [Required]
        [Display(Name = "Impact of the Problem")]
        [DataType(DataType.Text)]
        public string impact { get; set; }
    }
}
