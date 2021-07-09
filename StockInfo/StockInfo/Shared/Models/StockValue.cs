using System;
using System.Text.Json.Serialization;

namespace StockInfo.Shared.Models
{
    public class StockValue
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public double O { get; set; }

        public double L { get; set; }

        public double C { get; set; }

        public double H { get; set; }

        public double V { get; set; }

        [JsonIgnore]
        public StockDetails Stock { get; set; }
    }
}

