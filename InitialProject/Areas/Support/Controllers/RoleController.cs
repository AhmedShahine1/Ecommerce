using Ecommerce.Core.DTO;
using Ecommerce.Core.DTO.AuthViewModel.FilesModel;
using Ecommerce.Core.DTO.AuthViewModel.RoleModel;
using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
using Ecommerce.RepositoryLayer.Interfaces;
using Ecommerce.RepositoryLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Support.Controllers
{
    [Area("Support")]
    public class RoleController : Controller
    {
        private readonly BaseResponse baseResponse;
        private readonly IUnitOfWork unitOfWork;
        public RoleController(IUnitOfWork _unitOfWork)
        {
            baseResponse = new BaseResponse();
            unitOfWork = _unitOfWork;
        }
        // GET: PathController
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewData["Title"] = "Roles";
                var AllRoles = await unitOfWork.Roles.GetAllAsync();
                baseResponse.Data = AllRoles;
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
            ViewData["Title"] = "Create Role";
            return View();
        }

        // POST: PathController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddRoleModel roleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationRole roles = new ApplicationRole()
                    {
                        Name = roleModel.RoleName,
                        Description = roleModel.RoleDescription,
                        ArName = roleModel.RoleAr,
                    };
                    await unitOfWork.Roles.AddAsync(roles);
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
                baseResponse.ErrorMessage = "فشل حقظ البيانات";
                return View(baseResponse);
            }
        }

        // GET: PathController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                ViewData["Title"] = "Edit Role";
                var Role = await unitOfWork.Roles.FindByQuery(s => s.Id == id, isNoTracking: true).FirstOrDefaultAsync();
                baseResponse.Data = Role;
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
        public async Task<IActionResult> Edit(ApplicationRole role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Roles.Update(role);
                    await unitOfWork.SaveChangesAsync();
                    ViewBag.Message = "تم تعديل البيانات بنجاح";
                    return RedirectToAction(nameof(Index));

                }
                return View(role);
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
                var role = await unitOfWork.Roles.FindByQuery(s => s.Id == id).FirstOrDefaultAsync();
                unitOfWork.Roles.Delete(role);
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
