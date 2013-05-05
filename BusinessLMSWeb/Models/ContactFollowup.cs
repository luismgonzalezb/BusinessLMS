using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class ContactFollowup
	{

		[Display(Name = "Followup Id")]
		[DataType(DataType.Text)]
		public int followupId { get; set; }

		[Required]
		[Display(Name = "Contact ID")]
		[DataType(DataType.Text)]
		public int contactId { get; set; }

		[Required]
		[Display(Name = "Ibo Num")]
		[DataType(DataType.Text)]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "Method")]
		[DataType(DataType.Text)]
		public string method { get; set; }

		[Required]
		[Display(Name = "Completed")]
		[DataType(DataType.Text)]
		public System.DateTime datetime { get; set; }

		[Required]
		[Display(Name = "Follow up Date")]
		[DataType(DataType.DateTime)]
		public bool completed { get; set; }
	}
}
