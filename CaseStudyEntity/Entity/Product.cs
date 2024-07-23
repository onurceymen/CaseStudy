using System;
using System.Collections.Generic;

namespace CaseStudyEntity.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string SellerId { get; set; }
        public User Seller { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public byte StockAmount { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
