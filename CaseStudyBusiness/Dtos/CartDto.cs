namespace CaseStudyBusiness.Dtos
{
    public class CartDto
    {
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
