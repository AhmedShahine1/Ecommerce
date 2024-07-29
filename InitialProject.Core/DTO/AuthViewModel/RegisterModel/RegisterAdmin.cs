using Ecommerce.Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO.AuthViewModel.RegisterModel
{
    public class RegisterAdmin
    {
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "You should Enter Full Name"), StringLength(int.MaxValue)]
        public string FullName { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "You should Enter First Name"), StringLength(int.MaxValue)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "You should Enter Last Name"), StringLength(int.MaxValue)]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You should Enter Email"), StringLength(int.MaxValue), EmailAddress]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You should Enter Phone Number"), StringLength(int.MaxValue)]
        public string PhoneNumber { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You should Enter Password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You should Enter ConfirmPassword")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "You should Enter Age")]
        public int Age { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "You should Enter Gender")]
        public Gender Gender { get; set; }

        [DisplayName("Language")]
        [Required(ErrorMessage = "You should Enter Language")]
        public Language Language { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "You should Enter City")]
        public string CityId { get; set; }

        [DisplayName("Profile Image")]
        public IList<IFormFile>? file { get; set; }
    }
}
