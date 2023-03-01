using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.Database.Configuration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> entityBuilder) 
        {
            entityBuilder.HasIndex(p => p.ProductId);
            entityBuilder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        }
    }
}
