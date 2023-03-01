using Clients.WebClient.Models.Customer;

namespace Clients.WebClient.Models.Order
{
    public class ClientOrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ProductIds { get; set; }
        public ClientDto Client { get; set; }

    }
}