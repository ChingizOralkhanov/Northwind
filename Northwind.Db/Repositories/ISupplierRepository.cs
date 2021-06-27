using Northwind.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Db.Repositories
{
    public interface ISupplierRepository
    {
        Supplier GetSupplier(int id);
        IEnumerable<Supplier> GetAllSuppliers();
        void Add(Supplier supplier);
    }
}
