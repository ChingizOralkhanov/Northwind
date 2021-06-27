using Northwind.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Db.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(int? id);
        IEnumerable<Product> GetAllProducts();
        void Add(Product product);
        void Update(Product product);
    }
}
