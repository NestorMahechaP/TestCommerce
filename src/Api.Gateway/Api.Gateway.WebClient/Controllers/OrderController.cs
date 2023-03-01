using Api.Gateway.Models.Order;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProxy _orderProxy;
        private readonly ICustomerProxy _customerProxy;
        public OrderController(IOrderProxy orderProxy, ICustomerProxy customerProxy)
        {
            _orderProxy = orderProxy;
            _customerProxy = customerProxy;
        }
        [HttpGet]
        public async Task<List<ClientOrderDto>> GetAll()
        {
            var orders = await _orderProxy.GetAllAsync();
            foreach(var order in orders) 
            {
                order.Client = await _customerProxy.GetByIdAsync(order.CustomerId);
            }
            return orders;
        }
        [HttpGet("{id}")]
        public async Task<ClientOrderDto> GetByIdAsync(int id)
        {
            var order = await _orderProxy.GetByIdAsync(id);
            order.Client = await _customerProxy.GetByIdAsync(order.CustomerId);
            return order;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ClientOrderDto client)
        {
            await _orderProxy.CreateAsync(client);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ClientOrderDto client)
        {
            await _orderProxy.UpdateAsync(id, client);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _orderProxy.DeleteAsync(id);
            return Ok();
        }
    }
}