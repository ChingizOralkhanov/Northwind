using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Db.Models;
using Northwind.Db.Repositories;
using Northwind.Web.Controllers;
using System;
using Xunit;

namespace Northwind.Test
{
    public class ProductContollerTests
    {
        private Mock<IProductRepository> _productRepo;
        private Mock<ICategoryRepository> _categoryRepo;
        private Mock<ISupplierRepository> _supplierRepo;
        public ProductContollerTests()
        {
            _productRepo = new Mock<IProductRepository>();
            _categoryRepo = new Mock<ICategoryRepository>();
            _supplierRepo = new Mock<ISupplierRepository>();
        }

        [Fact]
        public void IsProductEditValid()
        {
            _productRepo.Setup(repo => repo.GetProduct(1)).Returns(new Product()
            {
                ProductName = "Chais",
                CategoryId = 1,
                Discontinued = false,
                ProductId = 1,
                ReorderLevel = 0,
                QuantityPerUnit = "10 boxes x 20 bags",
                UnitPrice = 18.0000M,
                UnitsInStock = 39
            });
            ProductsController pC = new ProductsController(_productRepo.Object, _categoryRepo.Object, _supplierRepo.Object, null, null);
            var result = pC.Edit(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.Model);
        }
    }
}
