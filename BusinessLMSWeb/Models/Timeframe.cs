using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BusinessLMSWeb.Models
{
    public class Timeframe
    {
       
        [Display(Name = "Time Frame Id")]
        public int timeframeId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string title { get; set; }

        [Required]
        [Display(Name = "Days")]
        [DataType(DataType.DateTime)]
        public int days { get; set; }

        [Required]
        [Display(Name = "Time Level")]
        public int timeLevel { get; set; }
    }
}
