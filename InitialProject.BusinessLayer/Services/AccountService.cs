using AutoMapper;
using Ecommerce.BusinessLayer.AutoMapper;
using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.Core.DTO.AuthViewModel;
using Ecommerce.Core.DTO.AuthViewModel.RegisterModel;
using Ecommerce.Core.DTO.AuthViewModel.RoleModel;
using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
using Ecommerce.Core.Entity.Others;
using Ecommerce.Core.Helpers;
using Ecommerce.RepositoryLayer.Interfaces;
using Ecommerce.RepositoryLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.BusinessLayer.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileHandling _fileHandling;
    private readonly Jwt _jwt;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMemoryCache memoryCache;
    private readonly IMapper mapper;
    public AccountService(IHttpClientFactory clientFactory, UserManager<ApplicationUser> userManager, IFileHandling photoHandling,
        RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork,
        IOptions<Jwt> jwt, IMemoryCache _memoryCache, IMapper _mapper, SignInManager<ApplicationUser> signInManager)
    {
        _clientFactory = clientFactory;
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _jwt = jwt.Value;
        _fileHandling = photoHandling;
        memoryCache = _memoryCache;
        mapper = _mapper;
        _signInManager = signInManager;
    }
    //------------------------------------------------------------------------------------------------------------
    public async Task<ApplicationUser> GetUserById(string id)
    {
        var user = await _userManager.Users
            .Include(u => u.Profile)
            .Include(u => u.City)
            .FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }
    //------------------------------------------------------------------------------------------------------------
    public async Task<IdentityResult> RegisterAdmin(RegisterAdmin model)
    {
        var user = mapper.Map<ApplicationUser>(model);

        if (model.ImageProfile != null)
        {
            var path = await GetPathByName("ProfileImages");
            user.ProfileId = await _fileHandling.UploadFile(model.ImageProfile, path);
        }
        else
        {
            var path = await GetPathByName("ProfileImages");
            user.ProfileId = await _fileHandling.DefaultProfile(path);
        }

        // Create the user
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
        }
        else
        {
            // Handle potential errors by throwing an exception or logging details
            throw new InvalidOperationException("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return result;
    }

    public async Task<IdentityResult> RegisterSupportDeveloper(RegisterSupportDeveloper model)
    {
        var user = mapper.Map<ApplicationUser>(model);

        if (model.ImageProfile != null)
        {
            var path = await GetPathByName("ProfileImages");
            user.ProfileId = await _fileHandling.UploadFile(model.ImageProfile, path);
        }
        else
        {
            var path = await GetPathByName("ProfileImages");
            user.ProfileId = await _fileHandling.DefaultProfile(path);
        }

        // Create the user
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Support Developer");
        }
        else
        {
            // Handle potential errors by throwing an exception or logging details
            throw new InvalidOperationException("Failed to create user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return result;
    }

    //------------------------------------------------------------------------------------------------------------
    public async Task<IdentityResult> UpdateAdmin(string adminId, RegisterAdmin model)
    {
        var user = await _userManager.FindByIdAsync(adminId);
        if (user == null)
            throw new ArgumentException("Admin not found");

        user.FullName = model.FullName;
        user.Age = model.Age;
        user.Gender = model.Gender;
        user.Language = model.Language;
        user.CityId = model.CityId;

        if (model.ImageProfile != null)
        {
            var path = await GetPathByName("ProfileImages");
            try
            {
                var newProfileId = await _fileHandling.UpdateFile(model.ImageProfile, path, user.ProfileId);
                user.ProfileId = newProfileId;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating file: {ex.Message}");
                throw new InvalidOperationException("Failed to update profile image", ex);
            }
        }

        try
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Log errors
                Console.WriteLine($"Error updating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error updating user: {ex.Message}");
            throw new InvalidOperationException("Failed to update admin", ex);
        }
    }

    public async Task<IdentityResult> UpdateSupportDeveloper(string adminId, RegisterSupportDeveloper model)
    {
        var user = await _userManager.FindByIdAsync(adminId);
        if (user == null)
            throw new ArgumentException("Admin not found");

        user.FullName = model.FullName;
        user.Age = model.Age;
        user.Gender = model.Gender;
        user.Language = model.Language;
        user.CityId = model.CityId;

        if (model.ImageProfile != null)
        {
            var path = await GetPathByName("ProfileImages");
            try
            {
                var newProfileId = await _fileHandling.UpdateFile(model.ImageProfile, path, user.ProfileId);
                user.ProfileId = newProfileId;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating file: {ex.Message}");
                throw new InvalidOperationException("Failed to update profile image", ex);
            }
        }

        try
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Log errors
                Console.WriteLine($"Error updating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                throw new InvalidOperationException($"Error updating user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error updating user: {ex.Message}");
            throw new InvalidOperationException("Failed to update admin", ex);
        }
    }

    //------------------------------------------------------------------------------------------------------------
    public async Task<(bool IsSuccess, string Token, string ErrorMessage)> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return (false, null, "Invalid login attempt.");
        }
        await _signInManager.SignInAsync(user, model.RememberMe);
        var token = await GenerateJwtToken(user);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return (true, tokenString, null);
    }

    public async Task<bool> Logout(string token)
    {
        var userId = ValidateJwtToken(token);
        if (userId == null)
            return false;

        var user = await GetUserById(userId);
        if (user == null)
            return false;

        await _signInManager.SignOutAsync();
        return true;
    }

    //------------------------------------------------------------------------------------------------------------
    public async Task<IEnumerable<SelectListItem>> GetAllCitiesAsync()
    {
        string CacheKey = "CityCache";
        var city = (memoryCache.TryGetValue(CacheKey, out IEnumerable<City>? Cities)) ?
            Cities :
            await _unitOfWork.CityRepository.FindAllAsync(x=>x.IsShow);
        return city.Select(city => new SelectListItem
        {
            Value = city.Id.ToString(),
            Text = city.NameEn
        });
    }

    private async Task<Paths> GetPathByName(string name)
    {
        var path = await _unitOfWork.PathsRepository
            .FindAsync(x => x.Name == name);

        if (path == null)
            throw new ArgumentException("Path not found");

        return path;
    }
    //------------------------------------------------------------------------------------------------------------

    public async Task<string> AddRoleAsync(RoleUserModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user is null)
            return "User not found!";

        if (model.RoleId != null && model.RoleId.Count() > 0)
        {
            var roleUser = _userManager.GetRolesAsync(user).Result;
            IEnumerable<string> roles = new List<string>();
            foreach (var roleid in model.RoleId)
            {
                var role = _roleManager.FindByIdAsync(roleid).Result.Name;
                if (roleUser.Contains(role))
                {
                    roles.Append(role);
                }
            }
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }
        return " Role is empty";
    }

    public Task<List<string>> GetRoles()
    {
        return _roleManager.Roles.Select(x => x.Name).ToListAsync();
    }

    //------------------------------------------------------------------------------------------------------------

    public async Task<IdentityResult> Activate(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("Admin not found");

        user.Status = true;
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> Suspend(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("Admin not found");

        user.Status = false;
        return await _userManager.UpdateAsync(user);
    }

    public async Task<string> GetUserProfileImage(string profileId)
    {
        if (string.IsNullOrEmpty(profileId))
        {
            return null;
        }

        var profileImage = await _fileHandling.GetFile(profileId);
        return profileImage;
    }
    //------------------------------------------------------------------------------------------------------------

    #region create and validate JWT token

    private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("name", user.FullName),
        new Claim("profileImage", await _fileHandling.GetFile(user.ProfileId))
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);
    }

    public string ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            if (token == null)
                return null;
            if (token.StartsWith("Bearer "))
                token = token.Replace("Bearer ", "");

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var accountId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

            return accountId;
        }
        catch
        {
            return null;
        }
    }

    #endregion create and validate JWT token

    #region Random number and string

    //Generate RandomNo
    public int GenerateRandomNo()
    {
        const int min = 1000;
        const int max = 9999;
        var rdm = new Random();
        return rdm.Next(min, max);
    }


    public string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    #endregion Random number and string
}