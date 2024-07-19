using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Core.Entity.ApplicationData
{
    public class ApplicationRole : IdentityRole
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ArName { get; set; }
    }
}