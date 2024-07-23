namespace CaseStudyEntity.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
