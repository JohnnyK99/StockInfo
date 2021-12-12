namespace StockInfo.Shared.Models.DTOs
{
    public class StockInfoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public string Exchange { get; set; }
        public string Currency { get; set; }
        public bool IsSaved { get; set; }

        //public bool HasNullProperties()
        //{
        //    return Name == null && Logo == null && Ceo == null && Url == null;
        //}
    }
}

