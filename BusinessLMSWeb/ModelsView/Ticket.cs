using Newtonsoft.Json;
namespace BusinessLMSWeb.Models
{

	public class Ticket
	{
		public string Title { get; set; }
		public string email { get; set; }
		public int PriorityLevelID { get; set; }
	}

	public class PriorityLevel
	{
		[JsonProperty("ID")]
		public int ID { get; set; }

		[JsonProperty("Value")]
		public string Name { get; set; }
	}

}
