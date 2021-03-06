using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockInfo.Server.Services;
using StockInfo.Shared.Models;
using System;
using System.Threading.Tasks;

namespace StockInfo.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TickersController : ControllerBase
    {
        private readonly IApiService _apiService;
        private readonly IDatabaseService _dbService;

        public TickersController(IApiService apiService, IDatabaseService dbService)
        {
            _apiService = apiService;
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickersAsync(string filter)
        {
            return Ok(await _apiService.GetFilteredTickersAsync(filter));
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetStockInfoAsync(string ticker, string username)
        {
            var info = await _apiService.GetStockInfoAsync(ticker);

            //try to get cached info from the database
            if (info == null)
            {
                if (await _dbService.ExistsStockAsync(ticker))
                {
                    info = await _dbService.GetStockInfoAsync(ticker);
                    return Ok(info);
                }
                else
                {
                    return NotFound();
                }
            }

            if (!await _dbService.ExistsStockAsync(ticker))
            {
                await _dbService.AddStockInfoAsync(new StockDetails
                {
                    Ticker = ticker,
                    Name = info.Name,
                    Country = info.Country,
                    Industry = info.Industry,
                    Sector = info.Sector,
                    Exchange = info.Exchange,
                    Currency = info.Currency,
                    LastUpdate = DateTime.Now
                });
            }
            else if (!await _dbService.HasStockUpToDateInfoAsync(ticker))
            {
                await _dbService.UpdateStockInfoAsync(new StockDetails
                {
                    Ticker = ticker,
                    Name = info.Name,
                    Description = info.Description,
                    Country = info.Country,
                    Industry = info.Industry,
                    Sector = info.Sector,
                    Exchange = info.Exchange,
                    Currency = info.Currency,
                    LastUpdate = DateTime.Now
                });
            }

            info.IsSaved = await _dbService.IsSavedStockAsync(username, ticker);

            return Ok(info);
        }

        [HttpGet("{ticker}/data")]
        public async Task<IActionResult> GetTickerChartDataAsync(string ticker)
        {
            var data = await _apiService.GetChartDataAsync(ticker);

            if (data == null)
            {
                //try to get from database
                if (await _dbService.ExistsValueAsync(ticker))
                {
                    data = await _dbService.GetValuesAsync(ticker);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                await _dbService.AddValuesAsync(data, ticker);
            }

            return Ok(data);
        }

        [HttpPost("saved")]
        public async Task<IActionResult> ToggleSaved(SavedStock savedStock)
        {
            if (await _dbService.IsSavedStockAsync(savedStock.Username, savedStock.Ticker))
            {
                await _dbService.RemoveSavedStockAsync(savedStock);
            }
            else
            {
                await _dbService.AddSavedStockAsync(savedStock);
            }

            return NoContent();
        }

        [HttpGet("saved")]
        public async Task<IActionResult> GetSaved(string username)
        {
            return Ok(await _dbService.GetSavedStocks(username));
        }
    }
}