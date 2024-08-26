namespace CaseStudyBusiness.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }

    public class OrderItemCreateDto
    {
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
    }
}
