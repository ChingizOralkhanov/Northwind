using Microsoft.AspNetCore.Mvc;
using Northwind.Db.Models;
using Northwind.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Index()
        {
            var products = _productRepository.GetAllProducts();
            if (products == null)
            {
                return NotFound();
            }
            return products.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId });
        }

        [HttpPost]
        public async Task<ActionResult<Product>> EditProduct(Product product)
        {
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.Update(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int productId)
        {
            _productRepository.Delete(productId);
            return NoContent();
        }
    }
}
