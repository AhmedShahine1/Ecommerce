﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.AuthViewModel.RoleModel
{
    public class AddRoleUserModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public IEnumerable<string> RoleId { get; set; }
    }
}