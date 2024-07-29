using Ecommerce.Core.DTO.AuthViewModel.FilesModel;
using Ecommerce.Core.DTO.AuthViewModel.RoleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BusinessLayer.Interfaces
{
    public interface ISupportService
    {
        bool AddRole(AddRoleModel model);
        bool UpdateRole(AddRoleModel model);
        bool DeleteRole(string RoleId);
        //--------------------------------------------
        bool AddPath(PathsModel model);
        bool DeletePath(string PathId);

    }
}
