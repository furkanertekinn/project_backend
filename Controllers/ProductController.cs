using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product_backend.Data;
using product_backend.Entities;

namespace product_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            for (int i = 0; i < products.Count; i++)
            {
                products[i].total = products[i].productUnitPrice * products[i].productUnitInStock;

            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
            {
                return BadRequest("Product not found !");
            }

            else
            {
                product.total = product.productUnitPrice * product.productUnitInStock;
                return Ok(product);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, Product product)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct is null)
            {
                return BadRequest("Product not found !");
            }

            dbProduct.productName = product.productName;
            dbProduct.productUnitPrice = product.productUnitPrice;
            dbProduct.productUnitInStock = product.productUnitInStock;

            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct is null)
            {
                return BadRequest("Product not found !");
            }

            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }
    }
}
