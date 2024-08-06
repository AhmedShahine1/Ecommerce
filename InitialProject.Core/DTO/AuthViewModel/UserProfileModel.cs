using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.AuthViewModel
{
    public class UserProfileModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
    }
}
