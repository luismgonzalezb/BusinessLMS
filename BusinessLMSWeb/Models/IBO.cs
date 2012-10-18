using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class IBO
    {
        [Required]
        public string IBONum { get; set; }
        public string UPLine { get; set; }
        [Required]
        public int languageId { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string accesstoken { get; set; }
        [Required]
        public string email { get; set; }
        public string facebookid { get; set; }
        public string twitter { get; set; }
        public string picture { get; set; }
        public int UserId { get; set; }
        public System.DateTime datetime { get; set; }
        [Required]
        public Nullable<System.DateTime> birthday { get; set; }
    }
}
