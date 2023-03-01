using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Order.Service.Queries.Interfaces;

namespace Order.Service.Queries
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public OrderQueryService(IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _context = applicationDbContext;
        }

        public void AddOrder(ClientOrderDto clientDto)
        {
            var client = _mapper.Map<ClientOrder>(clientDto);
            _context.Add(client);
        }
        public async Task DeleteOrder(int id)
        {
            var entity = _mapper.Map<ClientOrder>(await GetOrderByIdAsync(id));
            _context.Remove(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<ClientOrderDto>> GetClientsAsync()
        {
            return _mapper.Map<List<ClientOrderDto>>(await _context.ClientOrders.AsNoTracking().ToListAsync());
        }
        public async Task<ClientOrderDto> UpdateOrder(ClientOrderDto clientDto)
        {
            var uClient = _mapper.Map<ClientOrder>(clientDto);
            var client = await _context.ClientOrders.FirstOrDefaultAsync(x => x.OrderId == clientDto.OrderId);
            _context.Entry(client).CurrentValues.SetValues(uClient);
            return clientDto;
        }
        public async Task<ClientOrderDto> GetOrderByIdAsync(int id)
        {
            var client = await _context.ClientOrders.AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == id);
            return _mapper.Map<ClientOrderDto>(client);
        }
    }
}