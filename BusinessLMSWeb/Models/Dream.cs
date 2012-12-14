using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Dream
    {
        
        [Display(Name = "Dreams ID")]
        public int dreamId { get; set; }

        [Display(Name = "Dreams ID")]
        public string IBONum { get; set; }

        [Display(Name = "Dreams ID")]
        public int timeframeId { get; set; }

        [Display(Name = "Area ID")]
        public int areaId { get; set; }


        [Required]
        [Display(Name = "Dreams")]
        [DataType(DataType.Text)]
        public string dream1 { get; set; }

         [Required]
        [Display(Name = "Dram Level")]
        public int dreamLevel { get; set; }

         [Required]
         [Display(Name = "Contact Id")]
        public bool achieved { get; set; }

         [Required]
         [Display(Name = "Date Time")]
         [DataType(DataType.DateTime)]
        public System.DateTime datetime { get; set; }

         [Display(Name = "Dreams")]
         [DataType(DataType.Text)]
        public string picture { get; set; }
    }
}
