using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Alert
	{
		[Display(Name = "Alert Id")]
		public string AlertId { get; set; }

		[Required]
		[Display(Name = "Alert Message")]
		public string AlertMsg { get; set; }

		[Display(Name = "Action")]
		public string action { get; set; }

		[Required]
		[Display(Name = "Date and Time of the alert")]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }
	}
}
