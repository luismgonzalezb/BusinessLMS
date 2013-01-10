using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLMS.Models
{
    public class AlertIBO
    {
        [Key] public string AlertId { get; set; }
        public string IBONum { get; set; }
        public DateTime datetime { get; set; }
    }
}
