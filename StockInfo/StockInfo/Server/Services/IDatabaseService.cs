using StockInfo.Shared.Models;
using StockInfo.Shared.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockInfo.Server.Services
{
    public interface IDatabaseService
    {
        Task<bool> ExistsStockAsync(string ticker);
        Task<bool> HasStockUpToDateInfoAsync(string ticker);
        Task AddStockInfoAsync(StockDetails newInfo);
        Task UpdateStockInfoAsync(StockDetails newInfo);
        Task<StockInfoDto> GetStockInfoAsync(string ticker);

        Task<bool> ExistsValueAsync(string ticker);
        Task AddValuesAsync(IEnumerable<StockValueDto> values, string ticker);
        Task<IEnumerable<StockValueDto>> GetValuesAsync(string ticker, int days);

        Task<bool> IsSavedStockAsync(string username, string ticker);
        Task RemoveSavedStockAsync(SavedStock savedStock);
        Task AddSavedStockAsync(SavedStock savedStock);
        Task<IEnumerable<StockDetails>> GetSavedStocks(string username);
    }
}