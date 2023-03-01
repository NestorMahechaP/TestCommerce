using Api.Gateway.Models.Customer;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ClientController : ControllerBase
    {
        private readonly ICustomerProxy _clientProxy;
        public ClientController(ICustomerProxy clientProxy)
        {
            _clientProxy = clientProxy;
        }
        [HttpGet]
        public async Task<List<ClientDto>> GetAll()
        {
            var clients = await _clientProxy.GetAllAsync();
            return clients.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ClientDto> GetByIdAsync(int id)
        {
            return await _clientProxy.GetByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ClientDto client)
        {
            await _clientProxy.CreateAsync(client);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ClientDto client)
        {
            await _clientProxy.UpdateAsync(id, client);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _clientProxy.DeleteAsync(id);
            return Ok();
        }
    }
}
