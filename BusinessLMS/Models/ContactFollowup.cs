using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class ContactFollowup
    {
        public int followupId { get; set; }
        public int contactId { get; set; }
        public string IBONum { get; set; }
        public string method { get; set; }
        public System.DateTime datetime { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual IBO IBO { get; set; }
    }
}
