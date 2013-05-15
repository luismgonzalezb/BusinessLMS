using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class GoalProgress
	{
		[Required]
		[Display(Name = "progressId", ResourceType = typeof(TextResources.Businesslms))]
		public long progressId { get; set; }

		[Required]
		[Display(Name = "goalId", ResourceType = typeof(TextResources.Businesslms))]
		public int goalId { get; set; }

		[Required]
		[Display(Name = "progressId", ResourceType = typeof(TextResources.Businesslms))]
		public decimal progress { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }
	}
}
