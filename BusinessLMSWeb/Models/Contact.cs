using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMS.Models
{
	public partial class Contact
	{

		[Required]
		[Display(Name = "contactId", ResourceType = typeof(TextResources.Businesslms))]
		public int contactId { get; set; }

		[Required]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		[DataType(DataType.Text)]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "contactTypeId", ResourceType = typeof(TextResources.Businesslms))]
		public int contactTypeId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "languageId", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "firstName", ResourceType = typeof(TextResources.Businesslms))]
		public string firstName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "lastName", ResourceType = typeof(TextResources.Businesslms))]
		public string lastName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "email", ResourceType = typeof(TextResources.Businesslms))]
		public string email { get; set; }

		[DataType(DataType.PhoneNumber)]
		[Display(Name = "phone", ResourceType = typeof(TextResources.Businesslms))]
		public string phone { get; set; }

		[DataType(DataType.PhoneNumber)]
		[Display(Name = "cell", ResourceType = typeof(TextResources.Businesslms))]
		public string cell { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "address", ResourceType = typeof(TextResources.Businesslms))]
		public string address { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "state", ResourceType = typeof(TextResources.Businesslms))]
		public string state { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "city", ResourceType = typeof(TextResources.Businesslms))]
		public string city { get; set; }

		[DataType(DataType.PostalCode)]
		[Display(Name = "zipcode", ResourceType = typeof(TextResources.Businesslms))]
		public string zipcode { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "preferred", ResourceType = typeof(TextResources.Businesslms))]
		public string preferred { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "contactLevel", ResourceType = typeof(TextResources.Businesslms))]
		public string contactLevel { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }

		[Required]
		[Display(Name = "isPublic", ResourceType = typeof(TextResources.Businesslms))]
		public bool isPublic { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "birthday", ResourceType = typeof(TextResources.Businesslms))]
		public Nullable<System.DateTime> birthday { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "preferedTime", ResourceType = typeof(TextResources.Businesslms))]
		public string preferedTime { get; set; }

		[Required]
		[Display(Name = "newsletteroptin", ResourceType = typeof(TextResources.Businesslms))]
		public Nullable<bool> newsletteroptin { get; set; }

		public string GetFullName()
		{
			return string.Concat(this.firstName, " ", this.lastName);
		}

	}
}
