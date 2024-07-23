using System;
using System.Collections.Generic;
using Bogus;
using CaseStudyEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStudyData.Context
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CaseStudyDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            //using (var context = new CaseStudyDbContext(
            //    serviceProvider.GetRequiredService<DbContextOptions<CaseStudyDbContext>>()))
            //{
            // Veritabanında zaten veri varsa işlemi bitir
            //if (context.Users.Any())
            //    {
            //        return;
            //    }

            // Bogus kullanarak örnek kullanıcı verileri oluştur

            var utcNow = DateTime.UtcNow;

            var rolesArray = new[] { "Admin", "Seller", "Buyer" };
            var roleFaker = new Faker<Role>()
                //.RuleFor(r => r.Id, f => 0)
                .RuleFor(r => r.Name, f => rolesArray[f.IndexFaker])
                .RuleFor(r => r.CreatedAt, f => utcNow);

            var roles = roleFaker.Generate(3);
            context.Roles.AddRange(roles);

            await context.SaveChangesAsync();

            var userFaker = new Faker<User>()
                    .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.PasswordHash, f => f.Internet.Password())
                    .RuleFor(u => u.CreatedAt, f => utcNow)
                    //.RuleFor(u => u.RoleId, f => roles[(int)Math.Floor(f.Random.Float() * 3)].Id);
                    .RuleFor(u => u.RoleId, f => f.PickRandom(roles).Id);

            var users = userFaker.Generate(3);
            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            var categoryFaker = new Faker<Category>()
                //.RuleFor(c => c.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Color, f => f.Commerce.Color())
                .RuleFor(c => c.IconCssClass, f => f.Lorem.Word())
                .RuleFor(c => c.CreatedAt, f => utcNow)
                .RuleFor(c => c.ParentCategoryId, f => null);

            var categories = categoryFaker.Generate(3);
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            var productFaker = new Faker<Product>()
                //.RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.SellerId, f => f.PickRandom(users).Id)
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(p => p.Details, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.StockAmount, f => (byte)f.Random.Number(1, 100))
                .RuleFor(p => p.Enabled, f => true)
                .RuleFor(p => p.CreatedAt, f => utcNow);

            var products = productFaker.Generate(10);
            context.Products.AddRange(products);
            await context.SaveChangesAsync();


            var productImageFaker = new Faker<ProductImage>()
                //.RuleFor(pi => pi.Id, f => f.IndexFaker + 1)
                .RuleFor(pi => pi.ProductId, f => f.PickRandom(products).Id)
                .RuleFor(pi => pi.Url, f => f.Internet.Avatar())
                .RuleFor(pi => pi.CreatedAt, f => utcNow);

            var productImages = productImageFaker.Generate(3);


            //var cartItemFaker = new Faker<CartItem>()
            //    .RuleFor(ci => ci.Id, f => f.IndexFaker + 1)
            //    .RuleFor(ci => ci.UserId, f => f.PickRandom(users).Id)
            //    .RuleFor(ci => ci.ProductId, f => f.PickRandom(products).Id)
            //    .RuleFor(ci => ci.Quantity, f => (byte)f.Random.Number(1, 10))
            //    .RuleFor(ci => ci.CreatedAt, f => f.Date.Past());

            //var cartItems = cartItemFaker.Generate(3);

            //var orderFaker = new Faker<Order>()
            //    .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            //    .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
            //    .RuleFor(o => o.OrderCode, f => f.Random.AlphaNumeric(10))
            //    .RuleFor(o => o.Address, f => f.Address.FullAddress())
            //    .RuleFor(o => o.CreatedAt, f => f.Date.Past());

            //var orders = orderFaker.Generate(3);

            //var orderItemFaker = new Faker<OrderItem>()
            //    .RuleFor(oi => oi.Id, f => f.IndexFaker + 1)
            //    .RuleFor(oi => oi.OrderId, f => f.PickRandom(orders).Id)
            //    .RuleFor(oi => oi.ProductId, f => f.PickRandom(products).Id)
            //    .RuleFor(oi => oi.Quantity, f => (byte)f.Random.Number(1, 10))
            //    .RuleFor(oi => oi.UnitPrice, f => decimal.Parse(f.Commerce.Price()))
            //    .RuleFor(oi => oi.CreatedAt, f => f.Date.Past());

            //var orderItems = orderItemFaker.Generate(3);

            var productCommentFaker = new Faker<ProductComment>()
                //.RuleFor(pc => pc.Id, f => f.IndexFaker + 1)
                .RuleFor(pc => pc.ProductId, f => f.PickRandom(products).Id)
                .RuleFor(pc => pc.UserId, f => f.PickRandom(users).Id)
                .RuleFor(pc => pc.Text, f => f.Lorem.Sentence())
                .RuleFor(pc => pc.StarCount, f => (byte)f.Random.Number(1, 5))
                .RuleFor(pc => pc.IsConfirmed, f => true)
                .RuleFor(pc => pc.CreatedAt, f => utcNow);

            var productComments = productCommentFaker.Generate(30);

            context.ProductComments.AddRange(productComments);
            context.ProductImages.AddRange(productImages);

            await context.SaveChangesAsync();
            //}
        }
    }
}
