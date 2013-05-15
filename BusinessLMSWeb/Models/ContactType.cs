using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class ContactType
	{
		[Display(Name = "contactTypeId", ResourceType = typeof(TextResources.Businesslms))]
		public int contactTypeId { get; set; }

		[Display(Name = "languageId", ResourceType = typeof(TextResources.Businesslms))]
		public int languageId { get; set; }

		[Display(Name = "contactTypeId", ResourceType = typeof(TextResources.Businesslms))]
		public string type { get; set; }
	}
}
