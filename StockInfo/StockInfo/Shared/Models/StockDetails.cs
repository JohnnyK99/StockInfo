using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StockInfo.Shared.Models
{
    public class StockDetails
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public string Exchange { get; set; }
        public string Currency { get; set; }
        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        public ICollection<StockValue> Values { get; set; }

        [JsonIgnore]
        public ICollection<SavedStock> Saved { get; set; }
    }
}

