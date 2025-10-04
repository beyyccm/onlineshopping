README.md – Gelişmiş Sürüm
# OnlineShopping API

**OnlineShopping** projesi, ASP.NET Core Web API kullanılarak geliştirilmiş, çok katmanlı bir online alışveriş platformu örnek uygulamasıdır.  
Proje, Entity Framework Core ile Code First yaklaşımını kullanır ve kullanıcı yönetimi (kayıt, giriş) gibi temel işlemleri içerir.

---

## 🚀 Özellikler

- Katmanlı mimari: **API → Business → DataAccess → Common**
- Kullanıcı kayıt ve giriş işlemleri
- Şifreler `SHA256` ile hash’lenir
- Repository ve UnitOfWork deseni ile veri yönetimi
- Async metodlar ile yüksek performans
- Global Exception Handling middleware
- Swagger/OpenAPI desteği

---

## 📁 Proje Yapısı



OnlineShopping
│
├─ OnlineShopping.API → API katmanı, controller’lar
├─ OnlineShopping.Business → Business logic ve servisler
├─ OnlineShopping.Common → DTO ve ortak sınıflar
└─ OnlineShopping.DataAccess → Entity, DbContext, Repository ve UnitOfWork


---

## 🛠️ Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022 veya VS Code
- SQL Server (LocalDB veya başka)
- Postman veya tarayıcı (API testi için)

---

## ⚡ Kurulum ve Çalıştırma

1. Terminal veya PowerShell ile proje klasörüne git:

```powershell
cd "C:\Users\Beyzanur Çimen\OneDrive\Masaüstü\projesonn"


Paketleri geri yükle:

dotnet restore


Projeyi derle:

dotnet build


API’yi çalıştır (startup proje OnlineShopping.API):

dotnet run --project .\OnlineShopping.API\OnlineShopping.API.csproj


API çalıştıktan sonra terminalde göreceksiniz:

Now listening on: http://localhost:5000
Now listening on: https://localhost:5001


Swagger arayüzüne giderek API endpoint’lerini test edebilirsiniz:

http://localhost:5000/swagger

🔗 API Endpoint’leri
1. Kullanıcı Kayıt

URL: POST /api/user/register

Body (JSON):

{
  "name": "Ahmet Yılmaz",
  "email": "ahmet@example.com",
  "password": "123456"
}


Başarılı Response:

"Kayıt başarılı."

2. Kullanıcı Giriş

URL: POST /api/user/login

Body (JSON):

{
  "email": "ahmet@example.com",
  "password": "123456"
}


Başarılı Response:

{
  "id": 1,
  "name": "Ahmet Yılmaz",
  "email": "ahmet@example.com"
}

🧩 Postman Koleksiyonu

Tüm endpoint’ler için örnek Postman koleksiyonu:

Postman’i aç

File → Import → Link veya File → Import → JSON ile koleksiyonu ekle

Base URL: http://localhost:5000

İstersen ben sana bu Postman koleksiyon JSON dosyasını da hazırlayıp verebilirim.

📌 Notlar

Şifreler veri tabanında düz olarak saklanmaz; SHA256 ile hash’lenir.

UnitOfWork ve Repository pattern ile veri yönetimi sağlanır.

Global exception middleware ile API hataları yönetilir.
