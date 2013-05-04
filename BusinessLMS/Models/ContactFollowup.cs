
namespace BusinessLMS.Models
{
	public partial class ContactFollowup
	{
		public int followupId { get; set; }
		public int contactId { get; set; }
		public string IBONum { get; set; }
		public string method { get; set; }
		public System.DateTime datetime { get; set; }
		public bool completed { get; set; }
	}
}
