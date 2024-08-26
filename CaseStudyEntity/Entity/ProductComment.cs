namespace CaseStudyEntity.Entity
{
    public class ProductComment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public byte StarCount { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
    
 
}
