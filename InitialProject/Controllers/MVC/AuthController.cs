using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.Core.DTO.AuthViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce.Controllers.MVC
{
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            ViewData["Title"] = "Access Denied";
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["Title"] = "Login";
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string ReturnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (isSuccess, token, errorMessage) = await _accountService.Login(model);

            if (isSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(1)
                };
                HttpContext.Response.Cookies.Append("AuthToken", token, cookieOptions);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var userId = _accountService.ValidateJwtToken(token);
            var user = await _accountService.GetUserById(userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UserProfileModel
            {
                FullName = user.FullName,
                Email = user.Email,
                ProfileImage = await _accountService.GetUserProfileImage(user.ProfileId),
                RegistrationDate = user.RegistrationDate,
                Age = user.Age,
                Gender = user.Gender.ToString(),
                Language = user.Language.ToString(),
                City = user.City.NameEn
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (token != null)
            {
                var isLoggedOut = await _accountService.Logout(token);
                if (isLoggedOut)
                {
                    HttpContext.Response.Cookies.Delete("AuthToken");
                    return RedirectToAction("Login");
                }
            }

            return View();
        }
    }
}
