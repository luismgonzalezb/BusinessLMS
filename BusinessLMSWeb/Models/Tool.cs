
using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Tool
	{
		[Required]
		[Display(Name = "ToolName", ResourceType = typeof(TextResources.Businesslms))]
		public int toolId { get; set; }

		[Required]
		[Display(Name = "Languaje", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "ToolName", ResourceType = typeof(TextResources.Businesslms))]
		public string name { get; set; }

		[Required]
		[Display(Name = "def", ResourceType = typeof(TextResources.Businesslms))]
		public bool def { get; set; }
	}
}
