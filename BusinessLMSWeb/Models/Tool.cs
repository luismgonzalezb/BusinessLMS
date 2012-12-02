using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Tool
    {
        
        [Display(Name = "Tool ID")]
        public int toolId { get; set; }

        [Required]
        [Display(Name = "Name Tool")]
        [DataType(DataType.Text)]
        public string name { get; set; }

        [Required]
        [Display(Name = "def")]
        public bool def { get; set; }


    }
}
