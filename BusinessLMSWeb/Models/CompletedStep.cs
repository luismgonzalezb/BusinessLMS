using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class CompletedStep
	{
		[Required]
		[Display(Name = "Ibo Num")]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "step Id")]
		public int stepId { get; set; }

		[Display(Name = "Date Time")]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }
	}
}
