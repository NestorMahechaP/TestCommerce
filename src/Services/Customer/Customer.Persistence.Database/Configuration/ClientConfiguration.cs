using Customer.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Persistence.Database.Configuration
{
    public class ClientConfiguration
    {
        public ClientConfiguration(EntityTypeBuilder<Client> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(p => p.ClientId);
            entityTypeBuilder.Property(p=> p.FirstName).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
        }
    }
}
