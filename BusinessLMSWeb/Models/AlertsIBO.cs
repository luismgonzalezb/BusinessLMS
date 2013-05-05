using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class AlertsIBO
	{
		[Display(Name = "Alert Id")]
		public string AlertId { get; set; }

		[Required]
		[Display(Name = "IBO Number")]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "Date and Time")]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }
	}
}
