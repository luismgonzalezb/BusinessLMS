using System.ComponentModel.DataAnnotations;


namespace BusinessLMSWeb.Models
{
    public class ContactType
    {
        [Display(Name = "Contact Type ID")]
        public int contactTypeId { get; set; }

        [Display(Name = "Type")]
        public string type { get; set; }
    }
}
