using Api.Gateway.Models.Customer;

namespace Api.Gateway.Models.Order
{
    public class ClientOrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ProductIds { get; set; }
        public ClientDto Client { get; set; }
    }
}
