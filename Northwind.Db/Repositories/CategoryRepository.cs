using Northwind.Db.Models;
using Northwind.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Db.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private NorthwindDbContext _context;
        public CategoryRepository(NorthwindDbContext context)
        {
            _context = context;
        }
        public void Add(Category category)
        {
            try
            {
                _context.Add(category);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                var categories = _context.Categories.ToList();
                return categories;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Category GetCategory(int? id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
                return category;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public void Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }
    }
}
