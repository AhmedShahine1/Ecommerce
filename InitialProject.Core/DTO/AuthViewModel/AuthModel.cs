using Ecommerce.Core.Entity.Others;
using Ecommerce.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.AuthViewModel
{
    public class AuthModel
    {
        public bool Status { get; set; }
        public bool IsFeature { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAuthenticated { get; set; }
        //--------------------------------------------------------------------------------
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> Profiles { get; set; }
        public Gender Gender { get; set; }
        public Language Language { get; set; }
        public City City { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
    }
}
