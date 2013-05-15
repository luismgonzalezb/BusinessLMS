using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMS.Models
{
	public partial class Book
	{
		[Display(Name = "Book Id")]
		public int BookId { get; set; }

		[Required]
		[Display(Name = "IBONum")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "Title")]
		public string Autor { get; set; }

		[Required]
		[Display(Name = "IBONum", ResourceType = typeof(TextResources.Businesslms))]
		public string IBONum { get; set; }

		[Display(Name = "Buy Link")]
		public string Link1 { get; set; }

		[Display(Name = "Buy Link")]
		public string Link2 { get; set; }

		[Display(Name = "Buy Link")]
		public string Link3 { get; set; }

		[Display(Name = "Priority")]
		public Nullable<int> Priority { get; set; }

		[Display(Name = "Count")]
		public Nullable<int> Count { get; set; }
	}
}
