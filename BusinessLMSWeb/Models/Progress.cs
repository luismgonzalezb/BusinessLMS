using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Progress
    {
        [Display(Name = "Progress Id")]
        public long ProgressId { get; set; }

        [Required]
        [Display(Name = "Goal Id")]
        public int GoalId { get; set; }

        [Required]
        [Display(Name = "Progress")]
        public double progress { get; set; }

        [Required]
        [Display(Name = "Date and Time")]
        [DataType(DataType.DateTime)]
        public DateTime datetime { get; set; }
    }
}
