using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Goal
	{

		[Required]
		[Display(Name = "goalId", ResourceType = typeof(TextResources.Businesslms))]
		public int goalId { get; set; }

		[Required]
		[Display(Name = "dreamId", ResourceType = typeof(TextResources.Businesslms))]
		public int dreamId { get; set; }

		[Required]
		[Display(Name = "timeframeId", ResourceType = typeof(TextResources.Businesslms))]
		public int timeframeId { get; set; }

		[Required]
		[Display(Name = "ToolName", ResourceType = typeof(TextResources.Businesslms))]
		public int toolId { get; set; }

		[Required]
		[Display(Name = "goalId", ResourceType = typeof(TextResources.Businesslms))]
		public decimal goal1 { get; set; }

		[Required]
		public int goalLevel { get; set; }

		[Required]
		[Display(Name = "completedGoal", ResourceType = typeof(TextResources.Businesslms))]
		public bool completed { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }

		[Display(Name = "picture", ResourceType = typeof(TextResources.Businesslms))]
		public string picture { get; set; }
	}
}
