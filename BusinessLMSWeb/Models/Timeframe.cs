using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Timeframe
	{
		[Display(Name = "Time Frame Id")]
		public int timeframeId { get; set; }

		[Required]
		[Display(Name = "Languaje", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[Display(Name = "Title")]
		[DataType(DataType.Text)]
		public string title { get; set; }

		[Required]
		[Display(Name = "Days")]
		public int days { get; set; }

		[Required]
		[Display(Name = "Time Level")]
		public int timeLevel { get; set; }
	}
}
