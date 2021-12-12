using Newtonsoft.Json;
using System;

namespace StockInfo.Shared.Models.DTOs
{
    public class StockValueDto
    {
        public DateTime Date { get; set; }

        public double Open { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public double High { get; set; }

        public double Volume { get; set; }
    }
}

