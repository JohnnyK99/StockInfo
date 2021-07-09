using Microsoft.EntityFrameworkCore;
using StockInfo.Server.Data;
using StockInfo.Shared.Models;
using StockInfo.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInfo.Server.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsStockAsync(string ticker)
        {
            return await _dbContext.Stocks.AnyAsync(stock => stock.Ticker == ticker);
        }

        public async Task<bool> HasStockUpToDateInfoAsync(string ticker)
        {
            var aWeekAgo = DateTime.Now.AddDays(-7); //LINQ does not support DateTime.subtract() method
            return await _dbContext.Stocks.AnyAsync(stock => stock.Ticker == ticker && stock.LastUpdate > aWeekAgo);
        }

        public async Task AddStockInfoAsync(StockDetails newInfo)
        {
            await _dbContext.Stocks.AddAsync(newInfo);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStockInfoAsync(StockDetails newInfo)
        {
            _dbContext.Stocks.Attach(newInfo);

            _dbContext.Entry(newInfo).Property("Name").IsModified = true;
            _dbContext.Entry(newInfo).Property("Logo").IsModified = true;
            _dbContext.Entry(newInfo).Property("Url").IsModified = true;
            _dbContext.Entry(newInfo).Property("Ceo").IsModified = true;
            _dbContext.Entry(newInfo).Property("Country").IsModified = true;
            _dbContext.Entry(newInfo).Property("Industry").IsModified = true;
            _dbContext.Entry(newInfo).Property("Sector").IsModified = true;
            _dbContext.Entry(newInfo).Property("LastUpdate").IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<StockInfoDto> GetStockInfoAsync(string ticker)
        {
            var info = await _dbContext.Stocks.SingleAsync(stock => stock.Ticker == ticker);

            return new StockInfoDto
            {
                Name = info.Name,
                Logo = info.Logo,
                Ceo = info.Ceo,
                Url = info.Url,
                Country = info.Country,
                Industry = info.Industry,
                Sector = info.Sector
            };
        }

        public async Task<bool> ExistsValueAsync(string ticker)
        {
            return await _dbContext.StockValues.AnyAsync(value => value.Ticker == ticker);
        }

        private async Task<bool> AnyValuesToAdd(IEnumerable<StockValueDto> values, string ticker)
        {
            return (values.Max(value => value.Date) > await _dbContext.StockValues.Where(value => value.Ticker == ticker).MaxAsync(value => value.Date) ||
                values.Min(value => value.Date) < await _dbContext.StockValues.Where(value => value.Ticker == ticker).MinAsync(value => value.Date));
        }

        public async Task AddValuesAsync(IEnumerable<StockValueDto> values, string ticker)
        {
            if (!await ExistsValueAsync(ticker))
            {
                foreach (var value in values)
                {
                    _dbContext.StockValues.Add(new StockValue
                    {
                        Ticker = ticker,
                        Date = value.Date,
                        O = value.O,
                        L = value.L,
                        C = value.C,
                        H = value.H,
                        V = value.V
                    });
                }
            }
            else if (await AnyValuesToAdd(values, ticker))
            {
                var minDate = await _dbContext.StockValues.Where(value => value.Ticker == ticker).MinAsync(value => value.Date);
                var maxDate = await _dbContext.StockValues.Where(value => value.Ticker == ticker).MaxAsync(value => value.Date);

                foreach (var value in values)
                {
                    if (value.Date < minDate || value.Date > maxDate)
                    {
                        _dbContext.StockValues.Add(new StockValue
                        {
                            Ticker = ticker,
                            Date = value.Date,
                            O = value.O,
                            L = value.L,
                            C = value.C,
                            H = value.H,
                            V = value.V
                        });
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockValueDto>> GetValuesAsync(string ticker, int days)
        {
            return await _dbContext.StockValues.Where(value => value.Ticker == ticker).OrderBy(value => value.Date).Take(days).Select(value => new StockValueDto
            {
                Date = value.Date,
                O = value.O,
                L = value.L,
                C = value.C,
                H = value.H,
                V = value.V
            })
                .ToListAsync();
        }

        public async Task<bool> IsSavedStockAsync(string username, string ticker)
        {
            return await _dbContext.SavedStocks.AnyAsync(saved => saved.Ticker == ticker && saved.Username == username);
        }

        public async Task RemoveSavedStockAsync(SavedStock savedStock)
        {
            _dbContext.SavedStocks.Remove(savedStock);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddSavedStockAsync(SavedStock savedStock)
        {
            await _dbContext.SavedStocks.AddAsync(savedStock);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDetails>> GetSavedStocks(string username)
        {
            return await _dbContext.SavedStocks.Where(saved => saved.Username == username)
                .Select(saved => saved.Stock)
                .ToListAsync();
        }
    }
}