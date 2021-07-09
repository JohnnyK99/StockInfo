using StockInfo.Shared.Models.DTOs;
using System.Collections.Generic;

namespace StockInfo.Shared.Models
{
    public class StockValueWrapper
    {
        public string Ticker { get; set; }
        public List<StockValueDto> Results { get; set; }
    }
}