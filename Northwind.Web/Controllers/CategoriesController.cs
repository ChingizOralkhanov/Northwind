using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Db.Models;
using Northwind.Db.Repositories;
using Northwind.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostEnvironment;
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {
                var categoriesDTO = _categoryRepository.GetAllCategories();
                var categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categoriesDTO);

                return View(categoryViewModels);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IActionResult Image(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }
            try
            {
                var categoryDTO = _categoryRepository.GetCategory(categoryId);
                var categoryViewModel = _mapper.Map<CategoryViewModel>(categoryDTO);
                Task.Run(async () =>
                {
                 

                    var wwwRootPath = _hostEnvironment.WebRootPath;
                    var path = Path.Combine(wwwRootPath + "/image", categoryViewModel.CategoryName + new Guid() + ".jpg");
                    categoryViewModel.FileName = path;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        IFormFile formFile = new FormFile(fileStream, 0, fileStream.Length, Path.GetFileNameWithoutExtension(path), Path.GetFileName(path));
                        categoryViewModel.PicFormFile = formFile;
                        await categoryViewModel.PicFormFile.CopyToAsync(fileStream);
                    }
                });
                return View(categoryViewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public IActionResult ChangeImage(CategoryViewModel category)
        {
            /*            if (ModelState.IsValid)
                        {
                            var wwwRootPath = _hostEnvironment.WebRootPath;
                            var path = Path.Combine(wwwRootPath + "/image", category.CategoryName + ".jpg");
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await category.PicFormFile.CopyToAsync(fileStream);
                                var categoryDTO = _categoryRepository.GetCategory(category.CategoryId);
                                categoryDTO.Picture = category.Picture;
                                _categoryRepository.Update(categoryDTO);
                            }
                        }*/

            Task.Run(async () =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    await category.PicFormFile.CopyToAsync(memoryStream);
                    if (memoryStream.Length < 5097152)
                    {
                        var categoryDTO = _categoryRepository.GetCategory(category.CategoryId);
                        categoryDTO.Picture = memoryStream.ToArray();
                        _categoryRepository.Update(categoryDTO);
                        

                    }
                    else
                    {
                        ModelState.AddModelError("File", "File is too large");
                    }
                }
                
            });
            return Image(category.CategoryId);
        }
    }
}
