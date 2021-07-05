using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Db.Models;
using Northwind.Db.Repositories;
using Northwind.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostEnvironment;
        private readonly ILogger _logger;
        public CategoriesController(ICategoryRepository categoryRepository, 
                                    IMapper mapper, 
                                    IWebHostEnvironment hostEnvironment,
                                    ILogger<CategoriesController> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        [Route("Categories/Index")]
        [Route("Categories")]
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
                _logger.LogError($"Error occured: {e}");
                throw;
            }
        }

        [HttpGet]
        public IActionResult UpdateImage(int? id)
        {
            if(id == null) return NotFound();
            try
            {
                var categoryDto = _categoryRepository.GetCategory(id);
                if (categoryDto == null) return NotFound();
                var categoryViewModel = _mapper.Map<CategoryViewModel>(categoryDto);
                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex}");
                throw;
            }

        }

        [HttpPost]
        public IActionResult UpdateImage(CategoryViewModel categoryViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var wwwrootPath = _hostEnvironment.WebRootPath;
                    var path = @$"{wwwrootPath}\images\{Guid.NewGuid().ToString()}.jpg";
                    if (categoryViewModel.FormFile != null)
                    {
                        byte[] image = null;
                        using (BinaryReader binaryReader = new BinaryReader(categoryViewModel.FormFile.OpenReadStream()))
                        {
                            image = binaryReader.ReadBytes((int)categoryViewModel.FormFile.Length);
                        }
                        categoryViewModel.Picture = image;
                        if (image == null)
                        {
                            _logger.LogError($"Error occured: Unable to convert image to byte[]");
                        }
                        var categoryDto = _categoryRepository.GetCategory(categoryViewModel.CategoryId);
                        categoryDto.Picture = categoryViewModel.Picture;
                        _categoryRepository.Update(categoryDto);
                    }
                }
                return RedirectToAction("Image", new { categoryId = categoryViewModel.CategoryId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occured: {ex}");
                throw;
            }
        }


        [Route("Categories/Image")]
        [Route("Categories/Image/{categoryId}")]
        [Route("Image/{categoryId}")]
        public IActionResult Image(int categoryId)
        {
            try
            {
                var categoryDto = _categoryRepository.GetCategory(categoryId);
                if (categoryDto == null)
                {
                    return NotFound();
                }
                var categoryViewModel = _mapper.Map<CategoryViewModel>(categoryDto);
                categoryViewModel.Picture = categoryViewModel.Picture.Skip(78).ToArray();
                return View(categoryViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex}");
                throw;
            }
        }
    }
}
