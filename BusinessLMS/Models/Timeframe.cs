
namespace BusinessLMS.Models
{
	public partial class Timeframe
	{
		public int timeframeId { get; set; }
		public int languageId { get; set; }
		public string title { get; set; }
		public int days { get; set; }
		public int timeLevel { get; set; }
	}
}
