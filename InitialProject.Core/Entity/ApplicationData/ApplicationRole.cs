﻿using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Ecommerce.Core.Entity.ApplicationData
{
    [DebuggerDisplay("{Name,nq}")]
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public string ArName { get; set; }
    }
}