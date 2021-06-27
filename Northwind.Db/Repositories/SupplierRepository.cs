using Northwind.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Db.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private NorthwindDbContext _context;
        public SupplierRepository(NorthwindDbContext context)
        {
            _context = context;
        }
        public void Add(Supplier supplier)
        {
            _context.Add(supplier);
            _context.SaveChanges();
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            var suppliers = _context.Suppliers.ToList();
            return suppliers;
        }

        public Supplier GetSupplier(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.SupplierId == id);
            return supplier;
        }
    }
}
