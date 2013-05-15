using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class AlertsIBO
	{
		[Required]
		[Display(Name = "AlertId", ResourceType = typeof(TextResources.Businesslms))]
		public string AlertId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "Date and Time")]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }
	}
}
