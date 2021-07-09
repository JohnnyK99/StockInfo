using Newtonsoft.Json;

namespace StockInfo.Shared.Models
{
    public class Article
    {
        public string Title { get; set; }
        public string Article_url { get; set; }
        public string Image_url { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string DescriptionBrief
        {
            get
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return null;
                }

                if (Title.Length > 100)
                {
                    return Description.Substring(0, 100) + "...";
                }
                else
                {
                    return Description;
                }
            }
        }
    }
}

