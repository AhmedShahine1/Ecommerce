using AutoMapper;
using Ecommerce.Core.DTO.AuthViewModel.CityModel;
using Ecommerce.Core.Entity.Others;
using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class CityController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private const string CacheKey = "CityCache";
        public CityController(IUnitOfWork _unitOfWork, IMapper _mapper, IMemoryCache _memoryCache)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            memoryCache = _memoryCache;
        }

        // GET: CityController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewData["Title"] = "Cities";
                if (!memoryCache.TryGetValue(CacheKey, out IEnumerable<City>? Cities))
                {
                    Cities = await unitOfWork.CityRepository.GetAllAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // Cache for 5 minutes
                        SlidingExpiration = TimeSpan.FromMinutes(2) // Reset cache if accessed within 2 minutes
                    };
                    memoryCache.Set(CacheKey, Cities, cacheEntryOptions);
                }
                return View(Cities);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في جلب البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // GET: CityController/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create City";
            return View();
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityModel cityModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var City = mapper.Map<City>(cityModel);
                    await unitOfWork.CityRepository.AddAsync(City);
                    await unitOfWork.SaveChangesAsync();
                    memoryCache.Remove(CacheKey); // Clear cache
                    return RedirectToAction(nameof(Index));
                }
                return View(cityModel);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في حفظ البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // GET: CityController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                ViewData["Title"] = "Edit City";
                var city = (memoryCache.TryGetValue(CacheKey, out IEnumerable<City>? Cities)) ?
                    Cities?.FirstOrDefault(s => s.Id == id) :
                    await unitOfWork.CityRepository.GetByIdAsync(id);

                if (city == null)
                {
                    throw new Exception("City not found");
                }
                return View(city);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في جلب البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    city.IsUpdated = true;
                    city.UpdatedAt = DateTime.Now;
                    unitOfWork.CityRepository.Update(city);
                    await unitOfWork.SaveChangesAsync();
                    memoryCache.Remove(CacheKey); // Clear cache
                    ViewBag.Message = "تم تعديل البيانات بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                return View(city);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في تعديل البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var city = (memoryCache.TryGetValue(CacheKey, out IEnumerable<City>? Cities)) ? 
                    Cities?.FirstOrDefault(s => s.Id == id) :
                    await unitOfWork.CityRepository.GetByIdAsync(id);
                if (city == null)
                {
                    throw new Exception("City not found");
                }
                city.IsDeleted = true;
                city.DeletedAt = DateTime.Now;
                unitOfWork.CityRepository.Update(city);
                await unitOfWork.SaveChangesAsync();
                memoryCache.Remove(CacheKey); // Clear cache
                ViewBag.Message = "تم حذف البيانات بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "خطا في حذف البيانات",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }
    }
}
