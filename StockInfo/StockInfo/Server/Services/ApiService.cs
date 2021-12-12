using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StockInfo.Shared.Helpers;
using StockInfo.Shared.Models;
using StockInfo.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockInfo.Server.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ApiService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<TickerName>> GetFilteredTickersAsync(string filter)
        {
            var response = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={filter}&apikey={_configuration["ApiKey"]}");

            if (response.IsSuccessStatusCode)
            {
                var wrapper = JsonConvert.DeserializeObject<TickerNameWrapper>(await response.Content.ReadAsStringAsync());
                return wrapper.BestMatches.Select(value => new TickerName { Name = value.Name, Symbol = value.Symbol}).ToList();
            }
            return null;
        }


        public async Task<StockInfoDto> GetStockInfoAsync(string ticker)
        {
            var response = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=OVERVIEW&symbol={ticker}&apikey={_configuration["ApiKey"]}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<StockInfoDto>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<IEnumerable<StockValueDto>> GetChartDataAsync(string ticker)
        {
            var response = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={ticker}&apikey={_configuration["ApiKey"]}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                StockValueWrapper wrapper = JsonConvert.DeserializeObject<StockValueWrapper>(content);

                return wrapper.Values.Select(value => new StockValueDto
                {
                    Date = value.Key,
                    Open = value.Value.Open,
                    High = value.Value.High,
                    Low = value.Value.Low,
                    Close = value.Value.Close,
                    Volume = value.Value.Volume
                });
            }

            return null;
        }
    }
}