using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.AuthViewModel.RoleModel
{
    public class AddRoleModel
    {
        [Required, Display(Name = "Role Name"), StringLength(50)]
        public string RoleName { get; set; }
        [Required, Display(Name = "Role Description"), StringLength(int.MaxValue)]
        public string RoleDescription { get; set; }
        [Required, Display(Name = "Role Name Arabic"), StringLength(50)]
        public string RoleAr { get; set; }
    }
}
