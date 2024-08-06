using AutoMapper;
using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.Core.DTO.AuthViewModel.RegisterModel;
using Ecommerce.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Areas.Support.Controllers
{
    [Area("Support")]
    [Authorize(Policy = "Support Developer")]
    public class SupportDeveloperController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        public SupportDeveloperController(IAccountService _accountService, IMapper _mapper)
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
        public async Task<IActionResult> Register(RegisterSupportDeveloper model)
        {
            try
            {
                var cities = await accountService.GetAllCitiesAsync();
                if (ModelState.IsValid)
                {
                    var result = await accountService.RegisterSupportDeveloper(model);
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
            var model = new RegisterSupportDeveloper
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
        public async Task<IActionResult> Edit(string id, RegisterSupportDeveloper model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.UpdateSupportDeveloper(id, model);
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
