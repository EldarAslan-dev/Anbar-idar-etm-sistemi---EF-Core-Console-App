namespace EFCoreInventoryManager.Services
{
    public class InventoryApp
    {
        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
                DbSeeder.Seed(db);
            }

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n=== ANBAR İDARƏETMƏ SİSTEMİ ===");
                Console.WriteLine("1. Bütün kateqoriyaları göstər");
                Console.WriteLine("2. Yeni kateqoriya əlavə et");
                Console.WriteLine("3. Kateqoriya adını yenilə");
                Console.WriteLine("4. Bütün məhsulları göstər");
                Console.WriteLine("5. Yeni məhsul əlavə et");
                Console.WriteLine("6. Məhsul axtar (İd üzrə)");
                Console.WriteLine("7. Məhsulu yenilə");
                Console.WriteLine("8. Məhsulu sil");
                Console.WriteLine("9. LINQ Tapşırıqları (Filtrləmə və axtarış)");
                Console.WriteLine("0. Çıxış");
                Console.Write("Seçiminizi edin: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowCategories(); break;
                    case "2": AddCategory(); break;
                    case "3": UpdateCategory(); break;
                    case "4": ShowProducts(); break;
                    case "5": AddProduct(); break;
                    case "6": FindProduct(); break;
                    case "7": UpdateProduct(); break;
                    case "8": DeleteProduct(); break;
                    case "9": LinqMenu(); break;
                    case "0": isRunning = false; break;
                    default: Console.WriteLine("Yanlış seçim!"); break;
                }
            }
        }

        private void ShowCategories()
        {
            using var db = new AppDbContext();
            Console.WriteLine("\n--- KATEQORİYALAR ---");
            foreach (var c in db.Categories.ToList())
                Console.WriteLine($"ID: {c.Id} | Ad: {c.Name}");
        }

        private void AddCategory()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Kateqoriya adı: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ad boş ola bilməz!"); return;
            }

            using var db = new AppDbContext();
            db.Categories.Add(new Category { Name = name });
            db.SaveChanges();
            Console.WriteLine("Kateqoriya əlavə edildi.");
        }

        private void UpdateCategory()
        {
            Console.Write("Yeniləmək istədiyiniz kateqoriya ID-sini daxil edin: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            using var db = new AppDbContext();
            var category = db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                Console.WriteLine("Bu ID ilə kateqoriya tapılmadı."); return;
            }

            Console.Write("Yeni ad: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                category.Name = newName;
                db.SaveChanges();
                Console.WriteLine("Kateqoriya yeniləndi.");
            }
        }

        private void ShowProducts()
        {
            using var db = new AppDbContext();
            Console.WriteLine("\n--- MƏHSULLAR ---");
            foreach (var p in db.Products.ToList()) PrintProduct(p);
        }

        private void AddProduct()
        {
            using var db = new AppDbContext();

            Console.Write("Məhsulun adı: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) { Console.WriteLine("Ad boş ola bilməz!"); return; }

            Console.Write("Kateqoriya ID: ");
            if (!int.TryParse(Console.ReadLine(), out int catId) || !db.Categories.Any(c => c.Id == catId))
            {
                Console.WriteLine("Xəta: Belə bir kateqoriya ID mövcud deyil!"); return;
            }

            Console.Write("Qiymət (AZN): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
            {
                Console.WriteLine("Xəta: Qiymət mənfi ola bilməz!"); return;
            }

            Console.Write("Stok sayı: ");
            if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 0)
            {
                Console.WriteLine("Xəta: Stok mənfi ola bilməz!"); return;
            }

            Console.Write("Aktivdirmi? (true/false): ");
            bool.TryParse(Console.ReadLine(), out bool isActive);

            db.Products.Add(new Product
            {
                Name = name,
                CategoryId = catId,
                Price = price,
                StockQuantity = stock,
                IsActive = isActive
            });
            db.SaveChanges();
            Console.WriteLine("Məhsul əlavə edildi.");
        }

        private void FindProduct()
        {
            Console.Write("Axtarılan Məhsulun ID-si: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            using var db = new AppDbContext();
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null) Console.WriteLine("Məhsul tapılmadı.");
            else PrintProduct(product);
        }

        private void UpdateProduct()
        {
            Console.Write("Yenilənəcək məhsulun ID-si: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            using var db = new AppDbContext();
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) { Console.WriteLine("Məhsul tapılmadı."); return; }

            Console.Write($"Yeni qiymət (Köhnə: {product.Price}): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice >= 0)
                product.Price = newPrice;

            Console.Write($"Yeni stok sayı (Köhnə: {product.StockQuantity}): ");
            if (int.TryParse(Console.ReadLine(), out int newStock) && newStock >= 0)
                product.StockQuantity = newStock;

            Console.Write($"Aktivdirmi? (Köhnə: {product.IsActive}) (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool newStatus))
                product.IsActive = newStatus;

            db.SaveChanges();
            Console.WriteLine("Məhsul uğurla yeniləndi.");
        }

        private void DeleteProduct()
        {
            Console.Write("Silinəcək məhsulun ID-si: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            using var db = new AppDbContext();
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) { Console.WriteLine("Məhsul tapılmadı."); return; }

            db.Products.Remove(product);
            db.SaveChanges();
            Console.WriteLine("Məhsul silindi.");
        }

        private void LinqMenu()
        {
            using var db = new AppDbContext();
            Console.WriteLine("\n--- AXTARIŞ VƏ HESABLAMALAR ---");
            Console.WriteLine("1. Qiyməti 20 AZN-dən yuxarı olanlar");
            Console.WriteLine("2. Kateqoriya üzrə məhsullar");
            Console.WriteLine("3. Stokda olmayan məhsullar (Stok = 0)");
            Console.WriteLine("4. Aktiv məhsullar (Ucuzdan bahaya)");
            Console.WriteLine("5. Ada görə axtarış");
            Console.WriteLine("6. Statistika (Ən bahalı, Orta qiymət, Ümumi stok)");
            Console.WriteLine("7. Kateqoriya üzrə məhsul sayı");
            Console.Write("Seçim: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    foreach (var p in db.Products.Where(x => x.Price > 20)) PrintProduct(p);
                    break;
                case "2":
                    Console.Write("Kateqoriya ID: ");
                    if (int.TryParse(Console.ReadLine(), out int cId))
                        foreach (var p in db.Products.Where(x => x.CategoryId == cId)) PrintProduct(p);
                    break;
                case "3":
                    foreach (var p in db.Products.Where(x => x.StockQuantity == 0)) PrintProduct(p);
                    break;
                case "4":
                    foreach (var p in db.Products.Where(x => x.IsActive).OrderBy(x => x.Price)) PrintProduct(p);
                    break;
                case "5":
                    Console.Write("Axtarış mətni: ");
                    string text = Console.ReadLine();
                    foreach (var p in db.Products.Where(x => x.Name.Contains(text))) PrintProduct(p);
                    break;
                case "6":
                    var maxPrice = db.Products.Max(x => x.Price);
                    var avgPrice = db.Products.Average(x => x.Price);
                    var totalStock = db.Products.Sum(x => x.StockQuantity);
                    Console.WriteLine($"Ən bahalı: {maxPrice} AZN | Orta qiymət: {avgPrice:F2} AZN | Ümumi Stok: {totalStock}");
                    break;
                case "7":
                    Console.Write("Kateqoriya ID: ");
                    if (int.TryParse(Console.ReadLine(), out int catCountId))
                    {
                        var count = db.Products.Count(x => x.CategoryId == catCountId);
                        Console.WriteLine($"Bu kateqoriyada {count} məhsul var.");
                    }
                    break;
            }
        }

        private void PrintProduct(Product p)
        {
            Console.WriteLine($"ID: {p.Id} | Ad: {p.Name} | KateqoriyaID: {p.CategoryId} | Qiymət: {p.Price} AZN | Stok: {p.StockQuantity} | Aktiv: {p.IsActive}");
        }
    }
}