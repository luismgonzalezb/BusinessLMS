using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class DreamsMV
	{
		[Display(Name = "dream MVID")]
		public int dreamMVId { get; set; }

		[Required]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "MissionTitle", ResourceType = typeof(TextResources.BusinesslmsHome))]
		public string mission { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "VisionTitle", ResourceType = typeof(TextResources.BusinesslmsHome))]
		public string vision { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "PurposeTitle", ResourceType = typeof(TextResources.BusinesslmsHome))]
		public string purpose { get; set; }
	}
}
