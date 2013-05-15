using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMS.Models
{
	public partial class Book
	{
		[Display(Name = "BookId", ResourceType = typeof(TextResources.Businesslms))]
		public int BookId { get; set; }

		[Required]
		[Display(Name = "Title", ResourceType = typeof(TextResources.Businesslms))]
		public string Title { get; set; }

		[Required]
		[Display(Name = "Autor", ResourceType = typeof(TextResources.Businesslms))]
		public string Autor { get; set; }

		[Required]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Required]
		[Display(Name = "Link", ResourceType = typeof(TextResources.Businesslms))]
		public string Link1 { get; set; }

		[Display(Name = "Link", ResourceType = typeof(TextResources.Businesslms))]
		public string Link2 { get; set; }

		[Display(Name = "Link", ResourceType = typeof(TextResources.Businesslms))]
		public string Link3 { get; set; }

		[Display(Name = "Priority", ResourceType = typeof(TextResources.Businesslms))]
		public Nullable<int> Priority { get; set; }

		[Display(Name = "Count")]
		public Nullable<int> Count { get; set; }
	}
}
