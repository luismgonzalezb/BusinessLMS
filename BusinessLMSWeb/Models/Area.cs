using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Area
	{
		[Required]
		[Display(Name = "areaId", ResourceType = typeof(TextResources.Businesslms))]
		public int areaId { get; set; }

		[Required]
		[Display(Name = "languageId", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "areaId", ResourceType = typeof(TextResources.Businesslms))]
		public string title { get; set; }
	}
}
