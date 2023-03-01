using Api.Gateway.Models.Catalog;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly ICatalogProxy _catalogProxy;
        public ProductController(ICatalogProxy catalogProxy)
        {
            _catalogProxy = catalogProxy;
        }
        [HttpGet]
        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _catalogProxy.GetAllAsync();
            return products.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return await _catalogProxy.GetByIdAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProductDto product)
        {
            await _catalogProxy.CreateAsync(product);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProductDto product)
        {
            await _catalogProxy.UpdateAsync(id, product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _catalogProxy.DeleteAsync(id);
            return Ok();
        }
    }
}