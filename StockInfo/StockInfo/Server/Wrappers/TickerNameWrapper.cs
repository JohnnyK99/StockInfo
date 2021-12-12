using Newtonsoft.Json;
using System.Collections.Generic;

namespace StockInfo.Shared.Models
{
    //used only for deserializing data from the external API
    public class TickerNameWrapper
    {
        [JsonProperty("bestMatches")]
        public List<TickerNameHelper> BestMatches { get; set; }
    }

    public class TickerNameHelper
    {
        [JsonProperty("1. symbol")]
        public string Symbol { get; set; }

        [JsonProperty("2. name")]
        public string Name { get; set; }
    }
}

