using System;

namespace StockInfo.Shared.Models.DTOs
{
    public class StockValueDto
    {
        public DateTime Date { get; set; }

        public double O { get; set; }

        public double L { get; set; }

        public double C { get; set; }

        public double H { get; set; }

        public double V { get; set; }

        public long T { get; set; }
    }
}

