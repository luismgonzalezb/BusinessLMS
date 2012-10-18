using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class IBO
    {
        public string IBONum { get; set; }
        public string UPLine { get; set; }
        public int languageId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string accesstoken { get; set; }
        public string email { get; set; }
        public string facebookid { get; set; }
        public string twitter { get; set; }
        public string picture { get; set; }
        public int UserId { get; set; }
        public System.DateTime datetime { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
    }
}
