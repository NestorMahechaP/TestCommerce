using Order.Service.Queries.DTOs;

namespace Order.Service.Queries.Interfaces
{
    public interface IOrderQueryService
    {
        void AddOrder(ClientOrderDto clientDto);
        Task DeleteOrder(int id);
        Task<bool> SaveAll();
        Task<IEnumerable<ClientOrderDto>> GetClientsAsync();
        Task<ClientOrderDto> UpdateOrder(ClientOrderDto clientDto);
        Task<ClientOrderDto> GetOrderByIdAsync(int id);
    }
}
