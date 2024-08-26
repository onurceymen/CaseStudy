namespace CaseStudyEntity.Entity
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public Role Role { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
    }
}
