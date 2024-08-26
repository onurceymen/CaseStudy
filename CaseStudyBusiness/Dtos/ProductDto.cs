namespace CaseStudyBusiness.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public byte StockAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Enabled { get; set; }
    }

    public class ProductCreateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public byte StockAmount { get; set; }
    }

    public class ProductUpdateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public byte StockAmount { get; set; }
    }

    public class ProductDeleteDto
    {
        public bool Enabled { get; set; }
    }

    public class CreateProductCommentDto
    {
        public int ProductId { get; set; }
        public string Text { get; set; }
        public byte StarCount { get; set; }
    }

    public class UpdateProductCommentDto
    {
        public string Text { get; set; }
        public byte StarCount { get; set; }
    }

    public class ProductCommentDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public byte StarCount { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
