using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class ContactFollowup
    {
        [Display(Name = "Followup Id")]
        [DataType(DataType.Text)]
        public int followupId { get; set; }

        [Display(Name = "Contact ID")]
        [DataType(DataType.Text)]
        public int contactId { get; set; }

        [Required]
        [Display(Name = "Ibo Num")]
        [DataType(DataType.Text)]
        public string IBONum { get; set; }

        [Required]
        [Display(Name = "Method")]
        [DataType(DataType.Text)]
        public string method { get; set; }

        [Required]
        [Display(Name = "Completed")]
        [DataType(DataType.Text)]
        public bool completed { get; set; }

        [Required]
        [Display(Name = "Follow up Date")]
        [DataType(DataType.DateTime)]
        public System.DateTime datetime { get; set; }
    }
}
