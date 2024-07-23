namespace CaseStudyMVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderViewModel
    {
        public string Address { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
    }
}
