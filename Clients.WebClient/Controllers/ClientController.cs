using Clients.WebClient.Models.Customer;
using Clients.WebClient.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Clients.WebClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly ICustomerProxy _customerProxy;

        public ClientController(ICustomerProxy customerProxy)
        {
            _customerProxy = customerProxy;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _customerProxy.GetAllAsync();
            return View(clients);
        }
        public async Task<IActionResult> Client(int id)
        {
            var product = new ClientDto();
            ViewBag.Name = "New Order";

            if (id != 0)
            {
                product = await _customerProxy.GetByIdAsync(id);
                ViewBag.Name = "Edit Order";
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ClientDto client)
        {
            if (client.ClientId == 0)
            {
                await _customerProxy.CreateAsync(client);
            }
            else
            {
                await _customerProxy.UpdateAsync(client.ClientId, client);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerProxy.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}


