using System.ComponentModel.DataAnnotations;

namespace Order.Domain
{
    public class ClientOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ProductIds { get; set; }
    }
}