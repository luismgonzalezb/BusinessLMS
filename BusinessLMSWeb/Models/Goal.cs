using System.ComponentModel.DataAnnotations;


namespace BusinessLMSWeb.Models
{
    public class Goal
    {

        [Display(Name = "Goal Id")]
        public int goalId { get; set; }


        [Display(Name = "Dream Id")]
        public int dreamId { get; set; }


        [Display(Name = "Time Frame ID")]
        public int timeframeId { get; set; }


        [Display(Name = "Tool Id")]
        public int toolId { get; set; }


        [Required]
        [Display(Name = "Goal1")]
        public decimal goal1 { get; set; }


        [Required]
        [Display(Name = "Goal Level")]
        public int goalLevel { get; set; }


        [Required]
        [Display(Name = "Completed")]
        public bool completed { get; set; }


        [Required]
        [Display(Name = "Language")]
        [DataType(DataType.DateTime)]
        public System.DateTime datetime { get; set; }


        [Display(Name = "Picture")]
        public string picture { get; set; }
    }
}
