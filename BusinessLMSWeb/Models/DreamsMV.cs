using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class DreamsMV
	{
		public int dreamMVId { get; set; }

		[Required]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "mission", ResourceType = typeof(TextResources.Businesslms))]
		public string mission { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "vision", ResourceType = typeof(TextResources.Businesslms))]
		public string vision { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "purpose", ResourceType = typeof(TextResources.Businesslms))]
		public string purpose { get; set; }
	}
}
