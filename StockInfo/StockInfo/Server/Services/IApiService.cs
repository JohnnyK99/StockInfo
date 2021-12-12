using StockInfo.Shared.Models;
using StockInfo.Shared.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockInfo.Server.Services
{
    public interface IApiService
    {
        Task<List<TickerName>> GetFilteredTickersAsync(string filter); 
        Task<StockInfoDto> GetStockInfoAsync(string ticker);
        Task<IEnumerable<StockValueDto>> GetChartDataAsync(string ticker);
    }
}