using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Country
	{
		[Display(Name = "CountryId")]
		public int countryId { get; set; }

		[Required]
		[Display(Name = "Country Name")]
		[DataType(DataType.Text)]
		public string country1 { get; set; }
	}
}
