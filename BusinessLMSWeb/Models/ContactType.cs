using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class ContactType
	{
		[Display(Name = "Contact Type ID")]
		public int contactTypeId { get; set; }

		[Display(Name = "Lenguage")]
		public int languageId { get; set; }

		[Display(Name = "Type")]
		public string type { get; set; }
	}
}
