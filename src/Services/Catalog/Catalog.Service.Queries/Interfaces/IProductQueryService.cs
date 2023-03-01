using Catalog.Service.Queries.DTOs;

namespace Catalog.Service.Queries.Interfaces
{
    public interface IProductQueryService
    {
        void AddProduct(ProductDto productDto);
        Task DeleteProduct(int id);
        Task<bool> SaveAll();
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> UpdateProduct(ProductDto productDto);
    }
}
