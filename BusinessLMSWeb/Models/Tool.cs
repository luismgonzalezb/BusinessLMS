
using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Tool
	{
		[Display(Name = "Tool ID")]
		public int toolId { get; set; }

		[Required]
		[Display(Name = "Languaje", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[Display(Name = "ToolName", ResourceType = typeof(TextResources.Businesslms))]
		[DataType(DataType.Text)]
		public string name { get; set; }

		[Required]
		[Display(Name = "def")]
		public bool def { get; set; }
	}
}
