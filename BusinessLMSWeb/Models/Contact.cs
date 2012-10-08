using System;
using System.Collections.Generic;

namespace BusinessLMSWeb.Models
{
    public class Contact
    {
        public int contactId { get; set; }
        public string IBONum { get; set; }
        public int contactTypeId { get; set; }
        public int languageId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string preferred { get; set; }
        public string contactLevel { get; set; }
        public System.DateTime datetime { get; set; }
        public bool isPublic { get; set; }
    }
}
