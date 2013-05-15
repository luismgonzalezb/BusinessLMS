
using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Language
	{
		[Required]
		[Display(Name = "languageId", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "languageId", ResourceType = typeof(TextResources.Businesslms))]
		public string language1 { get; set; }

	}
}
