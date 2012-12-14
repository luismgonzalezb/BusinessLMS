using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class ZIPCode
    {
        [Required]
        [Display(Name = "ZIP Code")]
        public string ZIPCode1 { get; set; }

        [Display(Name = "Latitude")]
        [DataType(DataType.Text)]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        [DataType(DataType.Text)]
        public string Longitude { get; set; }

        [Display(Name = "Class")]
        [DataType(DataType.Text)]
        public string Class { get; set; }

        [Display(Name = "City")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Display(Name = "State Code")]
        [DataType(DataType.PostalCode)]
        public string StateCode { get; set; }


    }
}
