using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Ecommerce.Core.Entity.ApplicationData
{
    [DebuggerDisplay("Name")]
    public class ApplicationRole : IdentityRole
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ArName { get; set; }
    }
}