namespace Order.Service.Queries.DTOs
{
    public class ClientOrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ProductIds { get; set; }
    }
}
