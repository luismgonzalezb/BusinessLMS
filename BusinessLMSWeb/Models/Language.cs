
using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Language
	{
		[Required]
		[Display(Name = "Languaje", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[Display(Name = "Languaje", ResourceType = typeof(TextResources.Businesslms))]
		[DataType(DataType.Text)]
		public string language1 { get; set; }


	}
}
