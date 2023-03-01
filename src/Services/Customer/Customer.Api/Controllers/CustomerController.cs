using Customer.Service.Queries.DTOs;
using Customer.Service.Queries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IClientQueryService _clientQueryService;

        public CustomerController(ILogger<CustomerController> logger, IClientQueryService clientQueryService)
        {
            _logger = logger;
            _clientQueryService = clientQueryService;
        }

        [HttpGet]
        public async Task<List<ClientDto>> GetAll()
        {
            var products = await _clientQueryService.GetClientsAsync();
            return products.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ClientDto> GetById(int id)
        {
            var product = await _clientQueryService.GetClientByIdAsync(id);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientDto product)
        {
            _clientQueryService.AddClient(product);
            if (!await _clientQueryService.SaveAll())
            {
                return BadRequest("Could not create the new product.");
            }
            return Created(string.Empty, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _clientQueryService.GetClientByIdAsync(id);
            if (product == null)
            {
                return NotFound(string.Format("Id {0} not found.", id));
            }
            await _clientQueryService.DeleteClient(id);
            if (!await _clientQueryService.SaveAll())
            {
                return BadRequest(string.Format("Could not delete product with {0}.", id));
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClientDto product)
        {
            await _clientQueryService.UpdateClient(product);
            if (!await _clientQueryService.SaveAll())
            {
                return NotFound();
            }
            return Ok();
        }
    }
}