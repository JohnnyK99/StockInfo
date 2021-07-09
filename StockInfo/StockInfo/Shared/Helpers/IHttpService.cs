using System.Threading.Tasks;

namespace StockInfo.Shared.Helpers
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<object>> DeleteAsync(string url);
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);
        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T data);
        Task<HttpResponseWrapper<TResponse>> PostAsync<T, TResponse>(string url, T data);
        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T data);
    }
}

