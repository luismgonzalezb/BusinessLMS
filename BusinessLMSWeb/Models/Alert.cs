using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Alert
    {
        [Display(Name = "Alert Id")]
        public string AlertId { get; set; }

        [Required]
        [Display(Name = "Alert Message")]
        public string AlertMsg { get; set; }

        [Display(Name = "Action")]
        public string action { get; set; }

        [Required]
        [Display(Name = "Date and Time of the alert")]
        [DataType(DataType.DateTime)]
        public DateTime datetime { get; set; }
    }
}
