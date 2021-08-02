using AutoMapper;
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
    public class CategoriesApiController :ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesApiController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Index()
        {
            var categories = _categoryRepository.GetAllCategories();
            if(categories == null)
            {
                return NotFound();
            }
            return categories.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateCategory(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Add(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId });
        }

        [HttpPost]
        public async Task<ActionResult<Product>> EditCategory(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Update(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int productId)
        {
            _categoryRepository.Delete(productId);
            return NoContent();
        }
    }
}
