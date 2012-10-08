using System;
using System.Collections.Generic;

namespace BusinessLMSWeb.Models
{
    public class Location
    {
        public int locationId { get; set; }
        public string IBONum { get; set; }
        public int countryId { get; set; }
        public string ZIPCode { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
    }
}
