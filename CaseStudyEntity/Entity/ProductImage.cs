﻿namespace CaseStudyEntity.Entity
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }
    }
}
