using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Alert
	{
		[Display(Name = "AlertId", ResourceType = typeof(TextResources.Businesslms))]
		public string AlertId { get; set; }

		[Required]
		[Display(Name = "AlertMsg", ResourceType = typeof(TextResources.Businesslms))]
		public string AlertMsg { get; set; }

		[Display(Name = "action", ResourceType = typeof(TextResources.Businesslms))]
		public string action { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }
	}
}
