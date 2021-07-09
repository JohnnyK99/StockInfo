using Newtonsoft.Json;

namespace StockInfo.Shared.Models
{
    public class SavedStock
    {
        public string Username { get; set; }
        public string Ticker { get; set; }

        [JsonIgnore]
        public StockDetails Stock { get; set; }
    }
}
