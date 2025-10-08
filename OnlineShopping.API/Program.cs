using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.API.Filters;
using OnlineShopping.API.Helpers;
using OnlineShopping.API.Middlewares;
using OnlineShopping.Business.Interfaces;
using OnlineShopping.Business.Services;
using OnlineShopping.DataAccess.Data;
using OnlineShopping.DataAccess.Entities;
using OnlineShopping.DataAccess.Interfaces;
using OnlineShopping.DataAccess.Repositories;
using System.Text;



var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
services.AddControllers(options =>
{
    // global action filter for logging
    options.Filters.Add<LogActionFilter>();
});
services.AddScoped<LogActionFilter>();

// InMemory DB for easier testing (switch to UseSqlServer in production)
services.AddDbContext<AppDbContext>(options =>
 options.UseInMemoryDatabase("OnlineShoppingDb"));
// Geçici olarak SQL Server kullan (migration için)
bool isMigration = args.Contains("--migration"); // Migration eklerken bu parametreyi vereceðiz

if (isMigration)
{
    // Migration eklerken SQL Server kullan
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
}
else
{
    // Normal çalýþma sýrasýnda InMemoryDatabase kullan
    services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("OnlineShoppingDb"));
}

// Identity
services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Dependency Injection for application services and repositories
services.AddScoped<IUserService, UserService>();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
services.AddScoped<TimeRestrictedAttribute>();

// Filters & Middlewares
// JWT config (reads from appsettings.json)
var key = Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "fallback_secret_key_123456");
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {


        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = ClaimTypes.Role,

    };
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "OnlineShopping API", Version = "v1" });
    // JWT Bearer config for Swagger
    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter 'Bearer' [space] and then your valid token.",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

var app = builder.Build();

// Seed roles at startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await OnlineShopping.API.Helpers.RoleSeeder.SeedRolesAsync(roleManager);
}


// Additional seed: create admin and sample data
using (var scope2 = app.Services.CreateScope())
{
    var svcProvider = scope2.ServiceProvider;
    var userManager = svcProvider.GetRequiredService<UserManager<OnlineShopping.DataAccess.Entities.User>>();
    var roleManager = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var db = svcProvider.GetRequiredService<OnlineShopping.DataAccess.Data.AppDbContext>();

    // create roles if not exist
    var roles = new[] { "Admin", "Customer" };
    foreach (var r in roles)
    {
        if (!await roleManager.RoleExistsAsync(r))
            await roleManager.CreateAsync(new IdentityRole(r));
    }

    // create admin user
    var admin = await userManager.FindByNameAsync("admin");
    if (admin == null)
    {
        admin = new OnlineShopping.DataAccess.Entities.User
        {
            UserName = "admin",
            Email = "admin@example.com",
            FirstName = "System",
            LastName = "Admin",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(admin, "Admin@123");
        await userManager.AddToRoleAsync(admin, "Admin");
    }

    // create sample customer
    var customer = await userManager.FindByNameAsync("customer");
    if (customer == null)
    {
        customer = new OnlineShopping.DataAccess.Entities.User
        {
            UserName = "customer",
            Email = "customer@example.com",
            FirstName = "John",
            LastName = "Customer",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(customer, "Customer@123");
        await userManager.AddToRoleAsync(customer, "Customer");
    }

    // seed products if empty
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new OnlineShopping.DataAccess.Entities.Product { ProductName = "Sample A", Price = 9.99m, StockQuantity = 100 },
            new OnlineShopping.DataAccess.Entities.Product { ProductName = "Sample B", Price = 19.99m, StockQuantity = 50 }
        );
        db.SaveChanges();
    }

    // seed maintenance flag if not present
    if (!db.Maintenances.Any())
    {
        db.Maintenances.Add(new OnlineShopping.DataAccess.Entities.Maintenance { IsMaintenance = false, Message = "" });
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Custom middlewares
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<MaintenanceMiddleware>();

app.MapControllers();

app.Run();
