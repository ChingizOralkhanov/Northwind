using Northwind.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Db.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(int? id);
        IEnumerable<Category> GetAllCategories();
        void Add(Category category);
        void Update(Category category);
    }
}
