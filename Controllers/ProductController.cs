using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketApp.Interfaces;
using SupermarketApp.Models;
using SupermarketApp.ViewModels;

namespace SupermarketApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productsRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productsRepository, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }
        // GET: Categories
        public IActionResult Index()
        {
            var model = _productsRepository.FindAll().ToList();
            return View(model);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newProduct = _mapper.Map<Product>(model);

                    if (model.PictureContainer != null)
                    {
                        newProduct.Picture = ProcessUploadedFile(model);
                    }
                    else
                    {
                        newProduct.Picture = null;
                    }

                    var created = _productsRepository.Create(newProduct);
                    if (created)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Database Error");
                }
                ModelState.AddModelError("", "Please fill the required fields");
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        //// GET: Categories/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _productsRepository.FindById(id);
            if (product != null)
            {
                var model = _mapper.Map<ProductVM>(product);
                return View(model);
            }
            return NotFound();
        }

        //// POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _productsRepository.FindById(model.Id);
                    if (product != null)
                    {
                        product.ProductName = model.ProductName;
                        product.UnitPrice = model.UnitPrice;
                        product.UnitsInStock = model.UnitsInStock;

                        if (model.PictureContainer != null)
                        {
                            if (model.Picture != null)
                            {
                                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", model.Picture);
                                System.IO.File.Delete(filePath);
                            }
                            product.Picture = ProcessUploadedFile(model);
                        }
                        else
                        {
                            if (model.DeleteOldPicture)
                            {
                                if (model.Picture != null)
                                {
                                    string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", model.Picture);
                                    System.IO.File.Delete(filePath);
                                    product.Picture = null;
                                }
                                else
                                {
                                    product.Picture = null;
                                }
                            }
                        }

                        var updated = _productsRepository.Update(product);
                        if (updated)
                        {
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("", "Database error");
                        return View(model);
                    }
                    return NotFound();
                }
                // TODO: Add update logic here
                ModelState.AddModelError("", "Fill all the required fields properly");
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }


        private string ProcessUploadedFile(ProductVM model)
        {
            string uniqueFileName = null;
            if (model.PictureContainer != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath + "/images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PictureContainer.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.PictureContainer.CopyTo(fileStream);
            }
            else
            {
                return uniqueFileName;
            }

            return uniqueFileName;
        }

        //// POST: Categories/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _productsRepository.FindById(id);
                if (product != null)
                {
                    var result = _productsRepository.Delete(product);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    return View("Error", "Home");
                }
                return View("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}