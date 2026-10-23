namespace EFCoreInventoryManager.Data
{
    public static class DbSeeder
    {
        
        public static void Seed(AppDbContext db)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (!db.Categories.Any())
            {
                
                db.Categories.AddRange(
                    new Category { Name = "Elektronika" },
                    new Category { Name = "Dəftərxana" },
                    new Category { Name = "Məişət" },
                    new Category { Name = "Aksesuar" }
                );
                db.SaveChanges();
            }

            if (!db.Products.Any())
            {
                var c1 = db.Categories.First(c => c.Name == "Elektronika").Id;
                var c2 = db.Categories.First(c => c.Name == "Dəftərxana").Id;
                var c3 = db.Categories.First(c => c.Name == "Məişət").Id;
                var c4 = db.Categories.First(c => c.Name == "Aksesuar").Id;

                db.Products.AddRange(
                    new Product { Name = "Noutbuk", CategoryId = c1, Price = 1500, StockQuantity = 10, IsActive = true },
                    new Product { Name = "Mouse", CategoryId = c1, Price = 15.5m, StockQuantity = 0, IsActive = false },
                    new Product { Name = "Qələm", CategoryId = c2, Price = 1.2m, StockQuantity = 100, IsActive = true },
                    new Product { Name = "Dəftər", CategoryId = c2, Price = 3.5m, StockQuantity = 50, IsActive = true },
                    new Product { Name = "Ütü", CategoryId = c3, Price = 80, StockQuantity = 15, IsActive = true },
                    new Product { Name = "Tozsoran", CategoryId = c3, Price = 250, StockQuantity = 0, IsActive = false },
                    new Product { Name = "Saat", CategoryId = c4, Price = 45, StockQuantity = 20, IsActive = true },
                    new Product { Name = "Eynək", CategoryId = c4, Price = 18, StockQuantity = 5, IsActive = true },
                    new Product { Name = "Klaviatura", CategoryId = c1, Price = 35, StockQuantity = 25, IsActive = true },
                    new Product { Name = "Çanta", CategoryId = c4, Price = 22, StockQuantity = 0, IsActive = true }
                );
                db.SaveChanges();
            }
        }
    }
}