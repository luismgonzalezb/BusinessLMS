using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Timeframe
	{
		[Required]
		[Display(Name = "timeframeId", ResourceType = typeof(TextResources.Businesslms))]
		public int timeframeId { get; set; }

		[Required]
		[Display(Name = "languageId", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "timeframeId", ResourceType = typeof(TextResources.Businesslms))]
		public string title { get; set; }

		[Required]
		[Display(Name = "days", ResourceType = typeof(TextResources.Businesslms))]
		public int days { get; set; }

		[Required]
		public int timeLevel { get; set; }
	}
}
