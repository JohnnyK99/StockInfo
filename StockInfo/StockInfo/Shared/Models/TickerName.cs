using Newtonsoft.Json;

namespace StockInfo.Shared.Models
{
    public class TickerName
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string NameBrief
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return null;
                }

                if (Name.Length > 40)
                {
                    return Name.Substring(0, 40) + "...";
                }
                else
                {
                    return Name;
                }
            }
        }

        public override string ToString()
        {
            return Symbol + " | " + NameBrief;
        }
    }
}

