using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Common.DTOs;
using OnlineShopping.DataAccess.Data;
using OnlineShopping.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _db.Products.ToListAsync();
            return products.Select(p => new ProductDto { Id = p.Id, ProductName = p.ProductName, Price = p.Price, StockQuantity = p.StockQuantity }).ToList();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var p = new Product { ProductName = dto.ProductName, Price = dto.Price, StockQuantity = dto.StockQuantity };
            _db.Products.Add(p);
            await _db.SaveChangesAsync();
            dto.Id = p.Id;
            return CreatedAtAction(nameof(GetAll), new { id = p.Id }, dto);
        }
    }
}
