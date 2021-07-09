namespace StockInfo.Shared.Models.DTOs
{
    public class StockInfoDto
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Ceo { get; set; }
        public string Url { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }

        public bool HasNullProperties()
        {
            return Name == null && Logo == null && Ceo == null && Url == null;
        }
    }
}

