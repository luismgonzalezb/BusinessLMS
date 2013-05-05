using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Area
	{
		[Display(Name = "Area ID")]
		public int areaId { get; set; }

		[Display(Name = "Lenguage")]
		public int languageId { get; set; }

		[Required]
		[Display(Name = "Area Title")]
		[DataType(DataType.Text)]
		public string title { get; set; }
	}
}
