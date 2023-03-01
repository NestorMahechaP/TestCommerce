using AutoMapper;
using Catalog.Domain;
using Catalog.Persistence.Database;
using Catalog.Service.Queries.DTOs;
using Catalog.Service.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Queries
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductQueryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Add(product);
        }
        public async Task DeleteProduct(int id) 
        {
            var entity = _mapper.Map<Product>(await GetProductByIdAsync(id));
            _context.Remove(entity);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            var uproduct = _mapper.Map<Product>(productDto);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productDto.ProductId);
            _context.Entry(product).CurrentValues.SetValues(uproduct);
            return productDto;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
