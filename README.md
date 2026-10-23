# 📦 Anbar İdarəetmə Sistemi (EF Core Inventory Manager)

Bu layihə **C#** və **Entity Framework Core** istifadə edilərək hazırlanmış sadə, lakin funksional bir anbar idarəetmə konsol tətbiqidir. Proqram vasitəsilə məhsullar və kateqoriyalar üzərində CRUD əməliyyatları aparmaq və detallı LINQ sorğuları ilə axtarışlar etmək mümkündür.

## 🚀 Əsas Funksionallıqlar (Features)
- **Kateqoriya İdarəetməsi:** Yeni kateqoriya əlavə etmək, mövcud kateqoriya adını yeniləmək və bütün kateqoriyaları listələmək.
- **Məhsul İdarəetməsi:** Baza ilə əlaqəli (Foreign Key məntiqi) yeni məhsullar əlavə etmək, məlumatları yeniləmək və silmək.
- **Ətraflı Axtarış və LINQ Tətbiqi:**
  - Qiyməti müəyyən məbləğdən yuxarı olanları tapmaq.
  - Kateqoriyaya görə filtrləmək.
  - Stokda bitmiş (sayı 0 olan) məhsulları görmək.
  - Aktiv məhsulları qiymətinə görə artan qaydada sıralamaq.
  - Məhsul adına görə axtarış etmək.
- **Statistika:** Ən bahalı məhsul, məhsulların orta qiyməti və ümumi stok sayının hesablanması.

## 🛠 İstifadə Olunan Texnologiyalar
- **Dil:** C# (.NET)
- **ORM:** Entity Framework Core
- **Verilənlər Bazası:** Microsoft SQL Server (LocalDB)
- **Struktur:** Çoxqatlı məntiq (Models, Data, Services)
