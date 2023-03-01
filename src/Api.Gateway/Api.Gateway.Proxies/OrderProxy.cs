using Api.Gateway.Models.Order;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Api.Gateway.Proxies
{
    public interface IOrderProxy
    {
        Task<List<ClientOrderDto>> GetAllAsync();
        Task<ClientOrderDto> GetByIdAsync(int id);
        Task CreateAsync(ClientOrderDto product);
        Task UpdateAsync(int id, ClientOrderDto product);
        Task DeleteAsync(int id);
    }
    public class OrderProxy : IOrderProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public OrderProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<ClientOrderDto>> GetAllAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.OrderUrl}orders");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<List<ClientOrderDto>>(
                    await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
            );
        }
        public async Task<ClientOrderDto> GetByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.OrderUrl}orders/{id}");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ClientOrderDto>(
                    await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
            );
        }
        public async Task CreateAsync(ClientOrderDto product)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{_apiUrls.OrderUrl}orders", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, ClientOrderDto product)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var request = await _httpClient.PutAsync($"{_apiUrls.OrderUrl}orders/{id}", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var request = await _httpClient.DeleteAsync($"{_apiUrls.OrderUrl}orders/{id}");
            request.EnsureSuccessStatusCode();
        }
    }
}
