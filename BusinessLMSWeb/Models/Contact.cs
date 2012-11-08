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
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string cell { get; set; }

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
