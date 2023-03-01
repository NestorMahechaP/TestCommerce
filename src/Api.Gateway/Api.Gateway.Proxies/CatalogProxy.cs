using Api.Gateway.Models.Catalog;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Api.Gateway.Proxies
{
    public interface ICatalogProxy
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task CreateAsync(ProductDto product);
        Task UpdateAsync(int id, ProductDto product);
        Task DeleteAsync(int id);
    }
    public class CatalogProxy : ICatalogProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CatalogProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogUrl}products");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<List<ProductDto>>(
                    await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
            );
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogUrl}products/{id}");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ProductDto>(
                    await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
            );
        }
        public async Task CreateAsync(ProductDto product)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{_apiUrls.CatalogUrl}products", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, ProductDto product)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var request = await _httpClient.PutAsync($"{_apiUrls.CatalogUrl}products/{id}", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var request = await _httpClient.DeleteAsync($"{_apiUrls.CatalogUrl}products/{id}");
            request.EnsureSuccessStatusCode();
        }
    }
}
