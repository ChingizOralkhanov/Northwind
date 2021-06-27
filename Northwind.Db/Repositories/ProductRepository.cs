using Northwind.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Db.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NorthwindDbContext _context;
        public ProductRepository(NorthwindDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();
            var suppliers = _context.Suppliers.ToList();
            foreach (var product in products)
            {
                product.Category = categories.First(x => x.CategoryId == product.CategoryId);
                product.Supplier = suppliers.First(x => x.SupplierId == product.SupplierId);
            }
            return  _context.Products.ToList();
        }

        public Product GetProduct(int? id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public void Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }
    }
}
