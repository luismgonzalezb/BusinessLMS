
namespace BusinessLMS.Models
{
	public partial class Dream
	{

		public int dreamId { get; set; }
		public string IBONum { get; set; }
		public int timeframeId { get; set; }
		public int areaId { get; set; }
		public string dream1 { get; set; }
		public int dreamLevel { get; set; }
		public bool achieved { get; set; }
		public System.DateTime datetime { get; set; }
		public string picture { get; set; }
	}
}
