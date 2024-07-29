using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.Core.DTO.AuthViewModel.FilesModel;
using Ecommerce.Core.DTO.AuthViewModel.RoleModel;
using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BusinessLayer.Services
{
    public class SupportService : ISupportService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public SupportService(RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public  bool AddRole(AddRoleModel model)
        {
            if(_roleManager.FindByNameAsync(model.RoleName).Result != null)
                return false;
            ApplicationRole role = new ApplicationRole();
            role.ArName = model.RoleAr;
            role.Name = model.RoleName;
            role.Description = model.RoleDescription;
            _unitOfWork.Roles.Add(role);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateRole(AddRoleModel model)
        {
            if(!_roleManager.RoleExistsAsync(model.RoleName).Result) return false;
            var role = _roleManager.FindByIdAsync(model.RoleId).Result;
            if(role.Name != model.RoleName)
            {
                if (_roleManager.FindByNameAsync(model.RoleName).Result != null)
                    return false;
                role.Name = model.RoleName;
                role.Description= model.RoleDescription;
                role.ArName = model.RoleAr;
            }
            _unitOfWork.Roles.Update(role);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteRole(string RoleId)
        {
            var role = _roleManager.FindByIdAsync(RoleId).Result;
            if (!_roleManager.RoleExistsAsync(role.Name).Result) return false;
            _unitOfWork.Roles.Delete(role);
            _unitOfWork.SaveChanges();
            return true;
        }

        //-------------------------------------------------------------------
        public bool AddPath(PathsModel model)
        {
            if(_unitOfWork.PathsRepository.FindByQuery(s=>s.Name==model.Name)!=null)
                return false;
            Paths paths = new Paths
            {
                Name = model.Name,
                Description = model.Description,
            };
            _unitOfWork.PathsRepository.Add(paths);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeletePath(string PathId)
        {
            var path = _unitOfWork.PathsRepository.FindByQuery(s => s.Id == PathId).FirstOrDefault();
            if (path == null)
                return false;
            _unitOfWork.PathsRepository.Delete(path);
            _unitOfWork.SaveChanges();
            return true;
        }


    }
}
