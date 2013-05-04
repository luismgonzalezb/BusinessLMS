
namespace BusinessLMS.Models
{
	public partial class Goal
	{
		public int goalId { get; set; }
		public int dreamId { get; set; }
		public int timeframeId { get; set; }
		public int toolId { get; set; }
		public decimal goal1 { get; set; }
		public int goalLevel { get; set; }
		public bool completed { get; set; }
		public System.DateTime datetime { get; set; }
		public string picture { get; set; }
	}
}
