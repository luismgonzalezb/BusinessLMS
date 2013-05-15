using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMS.Models
{
	public partial class IBO
	{
		[Required]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Display(Name = "UpLine", ResourceType = typeof(TextResources.Businesslms))]
		public string UPLine { get; set; }

		[Required]
		[Display(Name = "languageId", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[Display(Name = "firstName", ResourceType = typeof(TextResources.Businesslms))]
		public string firstName { get; set; }

		[Required]
		[Display(Name = "lastName", ResourceType = typeof(TextResources.Businesslms))]
		public string lastName { get; set; }

		public string accesstoken { get; set; }

		[Required]
		[Display(Name = "email", ResourceType = typeof(TextResources.Businesslms))]
		public string email { get; set; }

		[Display(Name = "facebookid", ResourceType = typeof(TextResources.Businesslms))]
		public string facebookid { get; set; }

		[Display(Name = "twitter", ResourceType = typeof(TextResources.Businesslms))]
		public string twitter { get; set; }

		public System.DateTime datetime { get; set; }

		[Display(Name = "picture", ResourceType = typeof(TextResources.Businesslms))]
		public string picture { get; set; }

		public int UserId { get; set; }

		[Required]
		[Display(Name = "birthday", ResourceType = typeof(TextResources.Businesslms))]
		public Nullable<System.DateTime> birthday { get; set; }

		[Display(Name = "phone", ResourceType = typeof(TextResources.Businesslms))]
		public string phone { get; set; }

		[Display(Name = "level", ResourceType = typeof(TextResources.Businesslms))]
		public int level { get; set; }

		[Required]
		[Display(Name = "newsletteroptin", ResourceType = typeof(TextResources.Businesslms))]
		public bool newsletteroptin { get; set; }
	}
}
