using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Persistence.Database.Configuration
{
    public class ClientOrderConfiguration
    {
        public ClientOrderConfiguration(EntityTypeBuilder<ClientOrder> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(p => p.OrderId);
            entityTypeBuilder.Property(p => p.CustomerId).IsRequired();
            entityTypeBuilder.Property(p => p.ProductIds).IsRequired().HasMaxLength(250);
        }
    }
}
