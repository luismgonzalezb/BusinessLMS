using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Area
    {
        [Display(Name = "Ibo Num")]
        public int areaId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string title { get; set; }
    }
}
