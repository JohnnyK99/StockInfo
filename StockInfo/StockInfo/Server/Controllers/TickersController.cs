using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockInfo.Server.Services;
using System.Threading.Tasks;

namespace StockInfo.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TickersController : ControllerBase
    {
        private readonly IApiService _apiService;

        public TickersController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickersAsync()
        {
            return Ok(await _apiService.GetTickersAsync());
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetStockInfoAsync(string ticker)
        {
            return Ok(await _apiService.GetStockInfoAsync(ticker));
        }

        [HttpGet("{ticker}/data")]
        public async Task<IActionResult> GetTickerChartDataAsync(string ticker, int days)
        {
            return Ok(await _apiService.GetChartDataAsync(ticker, days));
        }

        [HttpGet("articles/{ticker}")]
        public async Task<IActionResult> GetArticlesAsync(string ticker, int number)
        {
            return Ok(await _apiService.GetArticlesAsync(ticker, number));
        }
    }
}