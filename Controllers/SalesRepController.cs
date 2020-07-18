using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketApp.Interfaces;
using SupermarketApp.Models;
using SupermarketApp.ViewModels;

namespace SupermarketApp.Controllers
{
    public class SalesRepController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly ISalesDetailRepository _salesDetailRepository;
        private readonly ISalesRepository _salesRepository;
        public SalesRepController(IMapper mapper, IProductRepository productRepository, ICartRepository cartRepository, 
            ISalesDetailRepository salesDetailRepository, ISalesRepository salesRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _salesDetailRepository = salesDetailRepository;
            _salesRepository = salesRepository;
        }
        // GET: SalesRep
        public ActionResult AddToCart(int productId)
        {
            var product = _productRepository.FindById(productId);
            var productModel = _mapper.Map<ProductVM>(product);
            var model = new CartVM
            {
                ProductId = productId,
                Product = productModel,
            };
            return View(model);
        }

        // POST: SalesRep/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(CartVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var product = _productRepository.FindById(model.ProductId);

                    if (model.Quantity > product.UnitsInStock || model.Quantity < 1)
                    {
                        ModelState.AddModelError("", "The value entered must not be greater than the units in stock and must be greater than zero");
                        return View(model);
                    }
                    var cartItem = new Cart
                    {
                        ProductId = model.ProductId,
                        Quantity = model.Quantity,
                        TotalPrice = model.Quantity * product.UnitPrice
                    };

                    var added = _cartRepository.Create(cartItem);
                    return RedirectToAction("landingpage", "Home");
                }
                ModelState.AddModelError("", "Fill all appropriate fields");
                return View(model);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        // GET: SalesRep/Edit/5
        public IActionResult Cart(int id)
        {
            var model = _cartRepository.FindAll().ToList();
            return View(model);
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = _cartRepository.FindById(id);
            _cartRepository.Delete(cartItem);
            return RedirectToAction("Cart");
        }

        public IActionResult CancelSale()
        {
            var cancelled = _cartRepository.RemoveAll();
            if (!cancelled)
            {
                return RedirectToAction("LandingPage","Home");
            }
            return View("Error", "Home");
        }

        public IActionResult MakeSale()
        {
            var cartItems = _cartRepository.FindAll().ToList();
            double total = 0;
            foreach (var item in cartItems)
            {
                total += item.TotalPrice;
            }
            var sale = new Sales
            {
                SalesDate = DateTime.Now,
                Amount = total
            };

            var addSale = _salesRepository.Create(sale);

            if (addSale)
            {
                var lastSale = _salesRepository.GetLastSale();
                for (int i = 0; i < cartItems.Count; i++)
                {
                    var product = _productRepository.FindById(cartItems[i].ProductId);
                    var salesDetail = new SalesDetail
                    {
                        SalesId = lastSale.Id,
                        ProductName = product.ProductName,
                        UnitPrice = product.UnitPrice,
                        Quantity = cartItems[i].Quantity
                    };
                    
                    product.UnitsInStock -= cartItems[i].Quantity;
                    _productRepository.Update(product);

                    _salesDetailRepository.Create(salesDetail);
                }

                _cartRepository.RemoveAll();
                return RedirectToAction("Sales");
            }
            return View("Error", "Home");
        }

        // GET: SalesRep/Delete/5
        public ActionResult Sales(int id)
        {
            var model = _salesRepository.FindAll().Reverse().ToList();
            return View(model);
        }

        public IActionResult SaleDetail(int saleId)
        {
            var model = _salesDetailRepository.GetSalesDetail(saleId).ToList();
            return View(model);
        }

        // POST: SalesRep/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}