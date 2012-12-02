using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BusinessLMSWeb.Models
{
    public class Language
    {
        [Display(Name = "Language Id")]
        public int languageId { get; set; }

        [Required]
        [Display(Name = "Language")]
        [DataType(DataType.Text)]
        public string language1 { get; set; }
    }
}
