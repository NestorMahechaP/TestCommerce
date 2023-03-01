using Clients.WebClient.Models;
using Clients.WebClient.Proxies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Clients.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogProxy _catalogProxy;
        private readonly ICustomerProxy _customerProxy;
        private readonly IOrderProxy _orderProxy;

        public HomeController(ICatalogProxy catalogProxy, ICustomerProxy customerProxy, IOrderProxy clientProxy)
        {
            _catalogProxy = catalogProxy;
            _customerProxy = customerProxy;
            _orderProxy = clientProxy;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}