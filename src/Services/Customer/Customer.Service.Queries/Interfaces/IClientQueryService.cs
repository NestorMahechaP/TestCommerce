using Customer.Service.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Queries.Interfaces
{
    public interface IClientQueryService
    {
        void AddClient(ClientDto clientDto);
        Task DeleteClient(int id);
        Task<bool> SaveAll();
        Task<IEnumerable<ClientDto>> GetClientsAsync();
        Task<ClientDto> UpdateClient(ClientDto clientDto);
        Task<ClientDto> GetClientByIdAsync(int id);
    }
}
