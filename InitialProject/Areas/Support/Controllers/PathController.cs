using Ecommerce.Core.DTO;
using Ecommerce.Core.DTO.AuthViewModel.FilesModel;
using Ecommerce.Core.Entity.Files;
using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Support.Controllers
{
    [Area("Support")]
    public class PathController : Controller
    {
        private readonly BaseResponse baseResponse;
        private readonly IUnitOfWork unitOfWork;
        public PathController(IUnitOfWork _unitOfWork)
        {
            baseResponse = new BaseResponse();
            unitOfWork = _unitOfWork;
        }
        // GET: PathController
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewData["Title"] = "Paths";
                var AllPaths =await unitOfWork.PathsRepository.GetAllAsync();
                baseResponse.Data = AllPaths;
                baseResponse.Status = true;
                baseResponse.ErrorCode = 200;
                baseResponse.ErrorMessage = "تم جلب البيانات بنجاح";
            }
            catch (Exception ex)
            {
                baseResponse.Status = false;
                baseResponse.ErrorCode = 400;
                baseResponse.ErrorMessage = "خطا في جلب البيانات بنجاح";
            }
            return View(baseResponse);
        }

        // GET: PathController/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Path";
            return View();
        }

        // POST: PathController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PathsModel pathsModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Paths paths = new Paths()
                    {
                        Name= pathsModel.Name,
                        Description= pathsModel.Description,
                    };
                    await unitOfWork.PathsRepository.AddAsync(paths);
                    await unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                baseResponse.Status = false;
                baseResponse.ErrorCode = 400;
                baseResponse.ErrorMessage = "فشل حقظ البيانات";
                return View(baseResponse);
            }
            catch
            {
                baseResponse.Status = false;
                baseResponse.ErrorCode = 400;
                baseResponse.ErrorMessage =  "فشل حقظ البيانات";
                return View(baseResponse);
            }
        }

        // GET: PathController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                ViewData["Title"] = "Edit Path";
                var Paths = await unitOfWork.PathsRepository.FindByQuery(s => s.Id == id, isNoTracking: true).FirstOrDefaultAsync();
                baseResponse.Data = Paths;
                baseResponse.Status = true;
                baseResponse.ErrorCode = 200;
                baseResponse.ErrorMessage = "تم جلب البيانات بنجاح";
                return View(baseResponse);
            }
            catch (Exception ex)
            {
                baseResponse.Status = false;
                baseResponse.ErrorCode = 400;
                baseResponse.ErrorMessage = "فشل جلب البيانات ";
                return View(baseResponse);
            }
        }

        // POST: PathController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Paths paths)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.PathsRepository.Update(paths);
                    await unitOfWork.SaveChangesAsync();
                    ViewBag.Message = "تم تعديل البيانات بنجاح";
                    return RedirectToAction(nameof(Index));

                }
                return View(paths);
            }
            catch
            {
                return View();
            }
        }

        // POST: PathController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var Paths = await unitOfWork.PathsRepository.FindByQuery(s => s.Id == id).FirstOrDefaultAsync();
                unitOfWork.PathsRepository.Delete(Paths);
                await unitOfWork.SaveChangesAsync();
                ViewBag.Message = "تم حذف البيانات بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = "فشل حذف البيانات";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
