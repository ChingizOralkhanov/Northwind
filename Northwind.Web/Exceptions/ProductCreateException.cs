using Northwind.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web.Exceptions
{
    [Serializable]
    public class ProductCreateException : Exception
    {
        public ProductCreateException()
        {

        }
        public ProductCreateException(Product product) : base($"Unable to create product: {product.ProductName}")
        {

        }
        public ProductCreateException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
