using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
    public class Tool
    {
        
        [Display(Name = "Tool ID")]
        public int toolId { get; set; }

        [Required]
        [Display(Name = "ToolName", ResourceType = typeof(TextResources.Businesslms))]
        [DataType(DataType.Text)]
        public string name { get; set; }

        [Required]
        [Display(Name = "def")]
        public bool def { get; set; }


    }
}
