using AutoMapper;
using Customer.Domain;
using Customer.Persistence.Database;
using Customer.Service.Queries.DTOs;
using Customer.Service.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Queries
{
    public class ClientQueryService : IClientQueryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public ClientQueryService(IMapper mapper, ApplicationDbContext applicationDbContext) 
        {
            _mapper = mapper;
            _context = applicationDbContext;
        }

        public void AddClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            _context.Add(client);
        }
        public async Task DeleteClient(int id)
        {
            var entity = _mapper.Map<Client>(await GetClientByIdAsync(id));
            _context.Remove(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<ClientDto>> GetClientsAsync()
        {
            return _mapper.Map<List<ClientDto>>(await _context.Clients.AsNoTracking().ToListAsync());
        }
        public async Task<ClientDto> UpdateClient(ClientDto clientDto)
        {
            var uClient = _mapper.Map<Client>(clientDto);
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == clientDto.ClientId);
            _context.Entry(client).CurrentValues.SetValues(uClient);
            return clientDto;
        }
        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId == id);
            return _mapper.Map<ClientDto>(client);
        }
    }
}
