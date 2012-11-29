using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Contact
    {
        public int contactId { get; set; }

        [Required]
        public string IBONum { get; set; }

        [Required]
        public int contactTypeId { get; set; }

        [Required]
        public int languageId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [Display(Name = "Cell Number")]
        [DataType(DataType.PhoneNumber)]
        public string cell { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string zipcode { get; set; }

        [Required]
        public string preferred { get; set; }

        [Required]
        public string contactLevel { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime datetime { get; set; }

        [Required]
        public bool isPublic { get; set; }

        public System.DateTime birthday { get; set; }
    }
}
