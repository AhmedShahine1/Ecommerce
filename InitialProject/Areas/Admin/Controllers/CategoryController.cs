using AutoMapper;
using Ecommerce.Core.DTO;
using Ecommerce.Core.DTO.VendorModel;
using Ecommerce.Core.Entity.Vendor;
using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private const string CacheKey = "CategoryCache";

        public CategoryController(IUnitOfWork _unitOfWork, IMapper _mapper, IMemoryCache _memoryCache)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            memoryCache = _memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewData["Title"] = "Categories";
                if (!memoryCache.TryGetValue(CacheKey, out IEnumerable<Category>? Categories))
                {
                    Categories = await unitOfWork.CategoryRepository.GetAllAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                        SlidingExpiration = TimeSpan.FromMinutes(2)
                    };
                    memoryCache.Set(CacheKey, Categories, cacheEntryOptions);
                }
                return View(Categories);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "Error fetching data",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Category";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = mapper.Map<Category>(categoryDto);
                    await unitOfWork.CategoryRepository.AddAsync(category);
                    await unitOfWork.SaveChangesAsync();
                    memoryCache.Remove(CacheKey);
                    return RedirectToAction(nameof(Index));
                }
                return View(categoryDto);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "Error saving data",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                ViewData["Title"] = "Edit Category";
                var category = (memoryCache.TryGetValue(CacheKey, out IEnumerable<Category>? Categories)) ?
                    Categories?.FirstOrDefault(s => s.Id == id) :
                    await unitOfWork.CategoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    throw new Exception("Category not found");
                }
                var categoryDto = mapper.Map<CategoryDTO>(category);
                return View(categoryDto);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "Error fetching data",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDTO categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = await unitOfWork.CategoryRepository.GetByIdAsync(categoryDto.Id);
                    if (category == null)
                    {
                        return NotFound();
                    }

                    category.Name = categoryDto.Name;
                    category.Description = categoryDto.Description;
                    category.UpdatedAt = DateTime.UtcNow;
                    category.IsUpdated = true;

                    unitOfWork.CategoryRepository.Update(category);
                    await unitOfWork.SaveChangesAsync();
                    memoryCache.Remove(CacheKey);
                    return RedirectToAction(nameof(Index));
                }
                return View(categoryDto);
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "Error updating data",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                unitOfWork.CategoryRepository.Delete(category);
                await unitOfWork.SaveChangesAsync();
                memoryCache.Remove(CacheKey);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "Error deleting data",
                    StackTrace = ex.StackTrace
                };
                return View("~/Views/Shared/Error.cshtml", errorViewModel);
            }
        }
    }
}
