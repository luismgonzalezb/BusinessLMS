
namespace BusinessLMS.Models
{
	public partial class GoalProgress
	{
		public long progressId { get; set; }
		public int goalId { get; set; }
		public decimal progress { get; set; }
		public System.DateTime datetime { get; set; }
	}
}
