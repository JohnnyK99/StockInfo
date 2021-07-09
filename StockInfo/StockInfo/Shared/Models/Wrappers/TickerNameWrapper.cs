using System.Collections.Generic;

namespace StockInfo.Shared.Models
{
    public class TickerNameWrapper
    {
        public List<TickerName> Results { get; set; }

        public string Next_Url { get; set; }
    }
}

