﻿using Clients.WebClient.Models;
using Clients.WebClient.Models.Customer;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Clients.WebClient.Proxies
{
    public interface ICustomerProxy
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<ClientDto> GetByIdAsync(int id);
        Task CreateAsync(ClientDto product);
        Task UpdateAsync(int id, ClientDto product);
        Task DeleteAsync(int id);
    }
    public class CustomerProxy : ICustomerProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;
        public CustomerProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.GatewayUrl}clients");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<List<ClientDto>>(await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.GatewayUrl}clients/{id}");
            request.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ClientDto>(await request.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }
        public async Task CreateAsync(ClientDto client)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{_apiUrls.GatewayUrl}clients", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, ClientDto client)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
            var request = await _httpClient.PutAsync($"{_apiUrls.GatewayUrl}clients/{id}", content);
            request.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var request = await _httpClient.DeleteAsync($"{_apiUrls.GatewayUrl}clients/{id}");
            request.EnsureSuccessStatusCode();
        }
    }
}
