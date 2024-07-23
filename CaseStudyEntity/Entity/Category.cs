namespace CaseStudyEntity.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string IconCssClass { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; } = null;
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
