using System.ComponentModel.DataAnnotations;
namespace BusinessLMS.Models
{
	public partial class Dream
	{

		[Display(Name = "dreamId", ResourceType = typeof(TextResources.Businesslms))]
		public int dreamId { get; set; }

		[Required]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "timeframeId", ResourceType = typeof(TextResources.Businesslms))]
		public int timeframeId { get; set; }

		[Required]
		[Display(Name = "areaId", ResourceType = typeof(TextResources.Businesslms))]
		public int areaId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "dreamId", ResourceType = typeof(TextResources.Businesslms))]
		public string dream1 { get; set; }

		[Required]
		public int dreamLevel { get; set; }

		[Required]
		[Display(Name = "achieved", ResourceType = typeof(TextResources.Businesslms))]
		public bool achieved { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public System.DateTime datetime { get; set; }

		[DataType(DataType.Text)]
		[Display(Name = "picture", ResourceType = typeof(TextResources.Businesslms))]
		public string picture { get; set; }
	}
}
