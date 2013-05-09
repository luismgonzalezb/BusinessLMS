using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMS.Models
{
	public partial class IBO
	{
		[Required]
		public string IBONum { get; set; }

		[Display(Name = "UpLine", ResourceType = typeof(TextResources.Businesslms))]
		public string UPLine { get; set; }

		[Required]
		[Display(Name = "Languaje", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[Display(Name = "FirstName", ResourceType = typeof(TextResources.Businesslms))]
		public string firstName { get; set; }

		[Required]
		[Display(Name = "LastName", ResourceType = typeof(TextResources.Businesslms))]
		public string lastName { get; set; }

		public string accesstoken { get; set; }

		[Required]
		public string email { get; set; }

		public string facebookid { get; set; }

		public string twitter { get; set; }

		public System.DateTime datetime { get; set; }

		public string picture { get; set; }

		public int UserId { get; set; }

		[Required]
		[Display(Name = "Birthday", ResourceType = typeof(TextResources.Businesslms))]
		public Nullable<System.DateTime> birthday { get; set; }

		[Display(Name = "Phone", ResourceType = typeof(TextResources.Businesslms))]
		public string phone { get; set; }

		[Display(Name = "Level", ResourceType = typeof(TextResources.Businesslms))]
		public int level { get; set; }

		[Required]
        [Display(Name = "NewsLetter", ResourceType = typeof(TextResources.Businesslms))]
		public bool newsletteroptin { get; set; }
	}
}
