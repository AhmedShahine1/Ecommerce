using Ecommerce.Core.Entity.Files;
using Ecommerce.Core.Entity.Others;
using Ecommerce.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Ecommerce.Core.Entity.ApplicationData
{
    [DebuggerDisplay("{FullName,nq}")]
    public class ApplicationUser : IdentityUser
    {
        public bool Status { get; set; } = true; // true account is active, false account isn't active
        public bool IsFeatured { get; set; } = false; // هل متميز ام لا
        public bool IsAdmin { get; set; } = false;
        //-----------------------------------------------------------------------------------------
        public string FullName { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Language Language { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }

        //-----------------------------------------------------------------------------------------
        [ForeignKey(nameof(Profile))]
        public string ProfileId { get; set; }
        public Images Profile { get; set; }

        [ForeignKey("City")]
        public string? CityId { get; set; }
        public City City { get; set; }
    }
}