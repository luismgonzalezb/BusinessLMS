using System;

namespace BusinessLMSWeb.Models
{
	public class Book
	{
		public int BookId { get; set; }
		public string IBONum { get; set; }
		public string Title { get; set; }
        public string Autor { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
        public string Link3 { get; set; }
        public int priority { get; set; }
        public int Count { get; set; }
	}
}
