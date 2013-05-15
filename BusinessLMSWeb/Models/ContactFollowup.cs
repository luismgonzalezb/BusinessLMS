using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class ContactFollowup
	{

		[DataType(DataType.Text)]
		[Display(Name = "followupId", ResourceType = typeof(TextResources.Businesslms))]
		public int followupId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "contactId", ResourceType = typeof(TextResources.Businesslms))]
		public int contactId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "method", ResourceType = typeof(TextResources.Businesslms))]
		public string method { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "datetimeFollowUp", ResourceType = typeof(TextResources.Businesslms))]
		public System.DateTime datetime { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name = "completed", ResourceType = typeof(TextResources.Businesslms))]
		public bool completed { get; set; }
	}
}
