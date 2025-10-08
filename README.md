## Gereksinimler

* .NET 8 SDK
* (Opsiyonel) SQL Server / LocalDB (varsayılan: InMemory DB)
* Tercihen Postman veya curl ve bir tarayıcı (Swagger UI için)

## Adımlar — Lokal olarak çalıştırma

1. ZIP'i açın ve proje köküne gidin:

```bash
unzip sonprojeson.zip -d project
cd project/OnlineShopping.API
```

2. Paketleri yükleyin ve projeyi çalıştırın:

```bash
dotnet restore
dotnet build
dotnet run
```

3. Uygulama başladıktan sonra `http://localhost:<port>/swagger` açarak API uç noktalarını görebilirsiniz.

### Örnek istekler (login -> token -> protected endpoint)

* Login (Postman / curl)

```bash
curl -X POST http://localhost:5000/api/users/login \
 -H "Content-Type: application/json" \
 -d '{"username":"admin","password":"Admin@123"}'
```

* Dönen yanıt: `{ "token": "eyJ..." }` veya `{ "Token": "eyJ..." }` (projede iki farklı controller üzerinden dönebilir)
* Protected endpoint (admin olarak):

```bash
curl -X POST http://localhost:5000/api/products \
 -H "Authorization: Bearer <TOKEN>" \
 -H "Content-Type: application/json" \
 -d '{"productName":"Yeni Urun","price":12.5,"stockQuantity":10}'
```

### appsettings.json içinde JWT ayarları

```json
"Jwt": {
  "Key": "ThisIsASuperSecretKeyForJwtAuth12345",
  "Issuer": "OnlineShoppingAPI",
  "Audience": "OnlineShoppingClient",
  "ExpireMinutes": 60
}
```

## Common troubleshooting

* **401 Unauthorized**: Genelde `Authorization` header eksik/yanlış veya `UseAuthentication()`/`UseAuthorization()` sırası hatalıysa olur. Header şöyle olmalı: `Authorization: Bearer <token>`
* **403 Forbidden**: Token geçerli ama rol yetkisi yok. Token içindeki claim'larda `role` olup olmadığını kontrol edin.
* **Token invalid signature**: `Jwt:Key` hem token üreten yerde hem de AddJwtBearer içinde aynı olmalı.


