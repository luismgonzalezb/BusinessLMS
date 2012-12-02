using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Contact
    {

        [Display(Name = "Contact Id")]
        public int contactId { get; set; }

        [Required]
        [Display(Name = "Ibo Num")]
        [DataType(DataType.Text)]
        public string IBONum { get; set; }

        [Required]
        [Display(Name = "Contact Level")]
        public int contactTypeId { get; set; }

        [Required]
        [Display(Name = "Lenguage")]
        [DataType(DataType.Text)]
        public int languageId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
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
        [DataType(DataType.MultilineText)]
        public string address { get; set; }

        [Display(Name = "State")]
        [DataType(DataType.Text)]
        public string state { get; set; }

        [Display(Name= "City")]
        [DataType(DataType.Text)]
        public string city { get; set; }

        [Display(Name = "zipcode")]
        [DataType(DataType.PostalCode)]
        public string zipcode { get; set; }

        [Required]
        [Display(Name = "Preferred Contact Method")]
        [DataType(DataType.Text)]
        public string preferred { get; set; }

        [Required]
        [Display(Name = "Contact Level")]
        [DataType(DataType.Text)]
        public string contactLevel { get; set; }

        [Required]
        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        public System.DateTime datetime { get; set; }

        [Required]
        public bool isPublic { get; set; }

        [Display(Name = "Birthdat")]
        [DataType(DataType.DateTime)]
        public System.DateTime birthday { get; set; }
    }
}
