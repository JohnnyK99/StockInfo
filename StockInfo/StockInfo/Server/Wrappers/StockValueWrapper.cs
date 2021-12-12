using Newtonsoft.Json;
using StockInfo.Shared.Models.DTOs;
using System;
using System.Collections.Generic;

namespace StockInfo.Shared.Models
{
    public class StockValueWrapper
    {
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<DateTime, StockValueHelper> Values { get; set; }
    }

    public class StockValueHelper
    {
        [JsonProperty("1. open")]
        public double Open { get; set; }

        [JsonProperty("3. low")]
        public double Low { get; set; }

        [JsonProperty("4. close")]
        public double Close { get; set; }

        [JsonProperty("2. high")]
        public double High { get; set; }

        [JsonProperty("6. volume")]
        public double Volume { get; set; }
    }
}