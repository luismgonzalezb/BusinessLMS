using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BusinessLMSWeb.Models
{
    public class Location
    {
        [Display(Name = "Location Id")]
        public int locationId { get; set; }

        [Display(Name = "IBO Id")]
        public string IBONum { get; set; }

        [Display(Name = "Country Id")]
        public int countryId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.PostalCode)]
        public string ZIPCode { get; set; }

        [Required]
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string address { get; set; }

        [Required]
        [Display(Name = "Address 2")]
        [DataType(DataType.Text)]
        public string address2 { get; set; }
    }
}
