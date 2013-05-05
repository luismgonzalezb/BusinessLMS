using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class DreamsMV
	{
		[Display(Name = "dream MVID")]
		public int dreamMVId { get; set; }

		[Required]
		[Display(Name = "IBO Num")]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "Mission")]
		[DataType(DataType.Text)]
		public string mission { get; set; }

		[Required]
		[Display(Name = "Vision")]
		[DataType(DataType.Text)]
		public string vision { get; set; }

		[Required]
		[Display(Name = "Purpose")]
		[DataType(DataType.Text)]
		public string purpose { get; set; }
	}
}
