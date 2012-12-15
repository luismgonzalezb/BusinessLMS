using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.ModelsView
{
    public class FollowupView
    {
        public int followupId { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "Contact Method")]
        public string Method { get; set; }

        [Display(Name = "Appoitment Date")]
        public System.DateTime datetime { get; set; }
    }
}