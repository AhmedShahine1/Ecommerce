using Ecommerce.Core.DTO.AuthViewModel;
using Ecommerce.Core.DTO.AuthViewModel.RegisterModel;
using Ecommerce.Core.DTO.AuthViewModel.RoleModel;
using Ecommerce.Core.Entity.ApplicationData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.BusinessLayer.Interfaces;

public interface IAccountService
{
    Task<ApplicationUser> GetUserById(string id);
    Task<IdentityResult> RegisterAdmin(RegisterAdmin model);
    Task<IdentityResult> UpdateAdmin(string adminId, RegisterAdmin model);
    Task<IdentityResult> RegisterSupportDeveloper(RegisterSupportDeveloper model);
    Task<IdentityResult> UpdateSupportDeveloper(string adminId, RegisterSupportDeveloper model);
    Task<(bool IsSuccess, string Token, string ErrorMessage)> Login(LoginModel model);
    Task<bool> Logout(string token);
    Task<IEnumerable<SelectListItem>> GetAllCitiesAsync();
    Task<string> AddRoleAsync(RoleUserModel model);
    Task<List<string>> GetRoles();
    Task<string> GetUserProfileImage(string profileId);
    string ValidateJwtToken(string token);
    int GenerateRandomNo();
    ////------------------------------------------------------
    Task<IdentityResult> Activate(string userId);
    Task<IdentityResult> Suspend(string userId);
    //string RandomString(int length);
    //Task<bool> DisActiveUserConnnection(string userId);
    //Task<bool> ActiveUserConnnection(string userId);
}