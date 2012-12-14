using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class State
    {
       
        [Display(Name = "Satate Code")]
        [DataType(DataType.PostalCode)]
        public string StateCode { get; set; }

       
        [Display(Name = "State Abbreviation")]
        [DataType(DataType.Text)]
        public string StateAbbreviation { get; set; }

        [Required]
        [Display(Name = "State Name")]
        [DataType(DataType.Text)]
        public string StateName { get; set; }
    }
}
