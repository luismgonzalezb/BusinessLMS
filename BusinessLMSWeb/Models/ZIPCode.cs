using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class ZIPCode
    {
        [Required]
        [Display(Name = "Zipcode", ResourceType = typeof(TextResources.Businesslms))]
        public string ZIPCode1 { get; set; }

        [Display(Name = "Latitude", ResourceType = typeof(TextResources.Businesslms))]
        [DataType(DataType.Text)]
        public string Latitude { get; set; }

        [Display(Name = "Longitude", ResourceType = typeof(TextResources.Businesslms))]
        [DataType(DataType.Text)]
        public string Longitude { get; set; }

         [Display(Name = "Class", ResourceType = typeof(TextResources.Businesslms))]
        [DataType(DataType.Text)]
        public string Class { get; set; }

         [Display(Name = "City", ResourceType = typeof(TextResources.Businesslms))]
        [DataType(DataType.Text)]
        public string City { get; set; }

         [Display(Name = "Statecode", ResourceType = typeof(TextResources.Businesslms))]
        [DataType(DataType.PostalCode)]
        public string StateCode { get; set; }


    }
}
