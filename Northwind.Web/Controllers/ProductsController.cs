using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Northwind.Db.Models;
using Northwind.Db.Repositories;
using Northwind.Web.Exceptions;
using Northwind.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private readonly IConfiguration _configuration;

        public ProductsController(IProductRepository productRepository,
                                  ICategoryRepository categoryRepository,
                                  ISupplierRepository supplierRepository,
                                  IConfiguration configuration,
                                  IMapper mapper, 
                                  ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            var productViewModels = _mapper.Map<List<ProductViewModel>>(products);
            var maxAmount = _configuration.GetValue<int>("MaxProductAmount");
            if (maxAmount == 0)
            {
                return View(productViewModels);
            }
            else
            {
                return View(productViewModels.Take(maxAmount));
            }
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_supplierRepository.GetAllSuppliers(), "SupplierId", "CompanyName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _mapper.Map<Product>(productViewModel);
                    _productRepository.Add(product);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(_supplierRepository.GetAllSuppliers(), "SupplierId", "CompanyName");
                return View(productViewModel);
            }
            catch (ProductCreateException ex)
            {
                _logger.LogError($"Error Occured, {ex.Message}");
                return RedirectToAction("Error");
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var product = _productRepository.GetProduct(id);
                if (product == null)
                {
                    _logger.LogError($"Error Occured: {id} is not valid");
                    throw new InvalidProductIdException(id.Value);
                }
                var productViewModel = _mapper.Map<ProductViewModel>(product);
                ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "CategoryName");
                ViewData["SupplierId"] = new SelectList(_supplierRepository.GetAllSuppliers(), "SupplierId", "CompanyName");
                return View(productViewModel);
            }
            catch (InvalidProductIdException ex)
            {
                _logger.LogError($"Error occured: {ex}");
                return NotFound();
            }


        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            try
            {
                var productDTO = _mapper.Map<Product>(product);
                _productRepository.Update(productDTO);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex}");
                return RedirectToAction("Error");
            }

        }
    }
}
