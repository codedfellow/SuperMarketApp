using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupermarketApp.Models;
using SupermarketApp.ViewModels;

namespace SupermarketApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        // GET: Account
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("landingpage","Home");
                    }
                    ModelState.AddModelError("", "invalid username or password");
                }
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateSalesAgent()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateSalesAgent(CreateSAVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = _mapper.Map<AppUser>(model);
                    newUser.UserName = model.UserName;

                    var result = _userManager.CreateAsync(newUser, model.Password).Result;

                    if (result.Succeeded)
                    {
                        var user = _userManager.FindByNameAsync(newUser.UserName).Result;
                        var addedToRole = _userManager.AddToRoleAsync(user, "SalesRep").Result;

                        if (addedToRole.Succeeded)
                        {
                            return RedirectToAction("landingpage", "Home");
                        }
                        return View();
                    }
                }
                ModelState.AddModelError("", "Enter all fields properly");
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        //[Authorize(Roles = "Administrator")]
        //public IActionResult CreateAdmin()
        //{
        //    return View();
        //}

        //[Authorize(Roles = "Administrator")]
        //[HttpPost]
        //public IActionResult CreateAdmin(CreateAdminVM model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var newUser = _mapper.Map<AppUser>(model);
        //            newUser.UserName = model.UserName;

        //            var result = _userManager.CreateAsync(newUser, model.Password).Result;

        //            if (result.Succeeded)
        //            {
        //                var user = _userManager.FindByEmailAsync(newUser.Email).Result;
        //                var addedToRole = _userManager.AddToRoleAsync(user, "Administrator").Result;

        //                if (addedToRole.Succeeded)
        //                {
        //                    return View("Login");
        //                }
        //                return View();
        //            }
        //        }
        //        ModelState.AddModelError("", "Enter all fields properly");
        //        return View(model);
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("", e.Message);
        //        return View(model);
        //    }
        //}

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsUsernameInUse(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Username {userName} is already in use");
            }
        }

        
    }
}