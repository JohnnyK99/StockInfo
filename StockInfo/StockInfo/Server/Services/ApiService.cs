using Microsoft.Extensions.Configuration;
using StockInfo.Shared.Helpers;
using StockInfo.Shared.Models;
using StockInfo.Shared.Models.DTOs;
using StockInfo.Shared.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockInfo.Server.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpService _httpService;

        public ApiService(IConfiguration configuration, IHttpService httpService)
        {
            _configuration = configuration;
            _httpService = httpService;
        }

        public async Task<List<TickerName>> GetTickersAsync()
        {
            List<TickerName> result = new List<TickerName>();
            HttpResponseWrapper<TickerNameWrapper> currentResponse;
            string url = "https://api.polygon.io/v3/reference/tickers?market=stocks&active=true&sort=ticker&order=asc&limit=1000";

            do
            {
                currentResponse = await _httpService.GetAsync<TickerNameWrapper>($"{url}&apiKey={_configuration["PolygonKey"]}");

                url = currentResponse.Response.Next_Url;

                result.AddRange(currentResponse.Response.Results);

            } while (url != null);

            return result;
        }


        public async Task<StockInfoDto> GetStockInfoAsync(string ticker)
        {
            var response = await _httpService.GetAsync<StockInfoDto>($"https://api.polygon.io/v1/meta/symbols/{ticker}/company?&apiKey={_configuration["PolygonKey"]}");

            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<StockValueDto>> GetChartDataAsync(string ticker, int numberOfDays)
        {
            string from = DateTime.Now.AddDays(-1*numberOfDays).ToString("yyyy-MM-dd");
            string to = DateTime.Now.ToString("yyyy-MM-dd");

            var response = await _httpService.GetAsync<StockValueWrapper>($"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{from}/{to}?unadjusted=false&sort=asc&apiKey={_configuration["PolygonKey"]}");

            if (!response.Success)
            {
                return null;
            }

            var wrapper = response.Response;

            foreach (var c in wrapper.Results)
            {
                c.Date = DateTimeOffset.FromUnixTimeMilliseconds(c.T).UtcDateTime;
            }

            return wrapper.Results;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync(string ticker, int number)
        {
            var response = await _httpService.GetAsync <ArticlesWrapper>($"https://api.polygon.io/v2/reference/news?limit={number}&order=descending&sort=published_utc&ticker={ticker}&apiKey={_configuration["PolygonKey"]}");

            return response.Response.Results;
        }
    }
}