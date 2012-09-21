using System;
using System.Collections.Generic;

namespace BusinessLMS.Models
{
    public class Timeframe
    {
        public int timeframeId { get; set; }
        public byte[] title { get; set; }
        public int days { get; set; }
    }
}
