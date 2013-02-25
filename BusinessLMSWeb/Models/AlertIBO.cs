using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
	public class AlertIBO
	{
        [Display(Name = "Alert Id")]
        public string AlertId { get; set; }

        [Required]
        [Display(Name = "IBO Number")]
        public string IBONum { get; set; }

        [Required]
        [Display(Name = "Date and Time")]
        [DataType(DataType.DateTime)]
        public DateTime datetime { get; set; }
	}
}
