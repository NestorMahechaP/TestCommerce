using Microsoft.AspNetCore.Mvc;
using Order.Service.Queries.DTOs;
using Order.Service.Queries.Interfaces;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private readonly IOrderQueryService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderQueryService orderQueryService)
        {
            _logger = logger;
            _orderService = orderQueryService;
        }

        [HttpGet]
        public async Task<List<ClientOrderDto>> GetAll()
        {
            var products = await _orderService.GetClientsAsync();
            return products.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ClientOrderDto> GetById(int id)
        {
            var product = await _orderService.GetOrderByIdAsync(id);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientOrderDto product)
        {
            _orderService.AddOrder(product);
            if (!await _orderService.SaveAll())
            {
                return BadRequest("Could not create the new product.");
            }
            return Created(string.Empty, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _orderService.GetOrderByIdAsync(id);
            if (product == null)
            {
                return NotFound(string.Format("Id {0} not found.", id));
            }
            await _orderService.DeleteOrder(id);
            if (!await _orderService.SaveAll())
            {
                return BadRequest(string.Format("Could not delete product with {0}.", id));
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClientOrderDto product)
        {
            await _orderService.UpdateOrder(product);
            if (!await _orderService.SaveAll())
            {
                return NotFound();
            }
            return Ok();
        }
    }
}