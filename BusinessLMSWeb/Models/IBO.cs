using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{

	[Serializable]
	public class IBO
	{
		[Required]
		public string IBONum { get; set; }
		public string UPLine { get; set; }
		[Required]
		public int languageId { get; set; }
		[Required]
		public string firstName { get; set; }
		[Required]
		public string lastName { get; set; }
		public string accesstoken { get; set; }
		[Required]
		public string email { get; set; }
		public string facebookid { get; set; }
		public string twitter { get; set; }
		public string picture { get; set; }
		public int UserId { get; set; }
		public System.DateTime datetime { get; set; }
		[Required]
		public Nullable<System.DateTime> birthday { get; set; }
		public string phone { get; set; }
		public int level { get; set; }
		[Required]
		[Display(Name = "I Would you like to receive Ibovirtual.com Newsletter.")]
		public bool newsletteroptin { get; set; }
	}
}
