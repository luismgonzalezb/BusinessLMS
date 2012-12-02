using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BusinessLMSWeb.Models
{
    public class Step
    {
      
        [Display(Name = "Step ID")]
        public int stepId { get; set; }


        [Display(Name = "Parent Step Id")]
        public Nullable<int> parentStepId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string description { get; set; }

     
        [Display(Name = "Step Order")]
        [DataType(DataType.Text)]
        public int stepOrder { get; set; }

        [Display(Name = "IconClass")]
        [DataType(DataType.Text)]
        public string iconClass { get; set; }

        [Display(Name = "Action")]
        [DataType(DataType.Text)]
        public string action { get; set; }

        [Display(Name = "Controller")]
        public string controller { get; set; }
    }
}
