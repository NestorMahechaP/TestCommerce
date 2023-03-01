using Catalog.Domain;
using Catalog.Service.Queries.DTOs;
using Catalog.Service.Queries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductQueryService _productService;

        public ProductController(ILogger<ProductController> logger, IProductQueryService productQueryService)
        {
            _logger = logger;
            _productService = productQueryService;
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productService.GetProductsAsync();
            return products.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto product)
        {
            _productService.AddProduct(product);
            if (!await _productService.SaveAll())
            {
                return BadRequest("Could not create the new product.");
            }
            return Created(string.Empty, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(string.Format("Id {0} not found.", id));
            }
            await _productService.DeleteProduct(id);
            if (!await _productService.SaveAll())
            {
                return BadRequest(string.Format("Could not delete product with {0}.", id));
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto product)
        {
            await _productService.UpdateProduct(product);
            if (!await _productService.SaveAll())
            {
                return NotFound();
            }
            return Ok();
        }
    }
}