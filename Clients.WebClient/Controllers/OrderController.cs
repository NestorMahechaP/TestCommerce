using Clients.WebClient.Models.Order;
using Clients.WebClient.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Clients.WebClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderProxy _orderProxy;

        public OrderController(IOrderProxy orderProxy)
        {
            _orderProxy = orderProxy;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderProxy.GetAllAsync();
            return View(orders);
        }
        public async Task<IActionResult> Order(int id)
        {
            var product = new ClientOrderDto();
            ViewBag.Name = "New Order";

            if (id != 0)
            {
                product = await _orderProxy.GetByIdAsync(id);
                ViewBag.Name = "Edit Order";
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ClientOrderDto order)
        {
            order.Client = new Models.Customer.ClientDto()
            {
                ClientId = 0,
                FirstName = "",
                LastName = ""
            };
            if (order.OrderId == 0)
            {
                await _orderProxy.CreateAsync(order);
            }
            else
            {
                await _orderProxy.UpdateAsync(order.OrderId, order);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderProxy.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
