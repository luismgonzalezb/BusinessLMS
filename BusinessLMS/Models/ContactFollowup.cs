
namespace BusinessLMS.Models
{
    public class ContactFollowup
    {
        public int followupId { get; set; }
        public int contactId { get; set; }
        public string IBONum { get; set; }
        public string method { get; set; }
        public bool completed { get; set; }
        public System.DateTime datetime { get; set; }
    }
}
