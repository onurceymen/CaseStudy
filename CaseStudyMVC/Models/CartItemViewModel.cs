namespace CaseStudyMVC.Models
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }

    public class AddCartItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
