using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class GoalProgress
	{
		[Display(Name = "Progress Id")]
		public long progressId { get; set; }

		[Required]
		[Display(Name = "Goal Id")]
		public int goalId { get; set; }

		[Required]
		[Display(Name = "Progress")]
		public decimal progress { get; set; }

		[Required]
		[Display(Name = "Date and Time")]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }
	}
}
