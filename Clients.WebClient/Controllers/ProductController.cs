using Clients.WebClient.Models.Catalog;
using Clients.WebClient.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Clients.WebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICatalogProxy _catalogProxy;

        public ProductController(ICatalogProxy catalogProxy)
        {
            _catalogProxy = catalogProxy;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalogProxy.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Product(int id)
        {
            var product = new ProductDto();
            ViewBag.Name = "New Product";

            if (id != 0)
            {
                product = await _catalogProxy.GetByIdAsync(id);
                ViewBag.Name = "Edit Product";
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto product)
        {
            if (product.ProductId == 0)
            {
                await _catalogProxy.CreateAsync(product);
            }
            else
            {
                await _catalogProxy.UpdateAsync(product.ProductId, product);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _catalogProxy.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
