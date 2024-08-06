using AutoMapper;
using Ecommerce.BusinessLayer.AutoMapper;
using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.Core.DTO;
using Ecommerce.Core.DTO.AuthViewModel.RegisterModel;
using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Support Developer")]
    public class AdminController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        public AdminController(IAccountService _accountService, IMapper _mapper)
        {
            accountService = _accountService;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var cities = await accountService.GetAllCitiesAsync();
            ViewBag.Cities = new SelectList(cities, "Value", "Text");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAdmin model)
        {
            try
            {
                var cities = await accountService.GetAllCitiesAsync();
                if (ModelState.IsValid)
                {
                    var result = await accountService.RegisterAdmin(model);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    ViewBag.Cities = new SelectList(cities, "Value", "Text");
                    return View(model);
                }
                ViewBag.Cities = new SelectList(cities, "Value", "Text");
                return View(model);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في تسجيل البيانات",
                    StackTrace = ex.InnerException.Message
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var admin = await accountService.GetUserById(id);
            if (admin == null)
            {
                return NotFound();
            }
            var model = new RegisterAdmin
            {
                FullName = admin.FullName,
                Email = admin.Email,
                PhoneNumber = admin.PhoneNumber,
                Age = admin.Age,
                Gender = admin.Gender,
                Language = admin.Language,
                CityId = admin.CityId
            };
            var cities = await accountService.GetAllCitiesAsync();
            ViewBag.Cities = new SelectList(cities, "Value", "Text");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RegisterAdmin model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.UpdateAdmin(id, model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            var cities = await accountService.GetAllCitiesAsync();
            ViewBag.Cities = new SelectList(cities, "Value", "Text");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await accountService.Suspend(id);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}
