using Northwind.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Northwind.Web.ViewModels
{
    public partial class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string QuantityPerUnit { get; set; }

        [Required]
        [Range(0.0, Double.PositiveInfinity)]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public CategoryViewModel Category { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}
