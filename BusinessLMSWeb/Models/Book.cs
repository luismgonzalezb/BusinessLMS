using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMSWeb.Models
{
	public class Book
	{

        [Display(Name = "Book Id")]
        public string BookId { get; set; }

        [Required]
        [Display(Name = "IBONum")]
        public string IBONum { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Display(Name = "Buy Link")]
        public string Link1 { get; set; }

        [Display(Name = "Buy Link")]
        public string Link2 { get; set; }

        [Display(Name = "Buy Link")]
        public string Link3 { get; set; }

        [Display(Name = "Priority")]
        public int priority { get; set; }

        [Display(Name = "Count")]
        public int Count { get; set; }
	}
}
