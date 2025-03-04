using Jago.Application.Interfaces.Services;
using Jago.CrossCutting.Dto;
using Jago.domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Jago.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly HttpContextAccessor _httpContext;

        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager, 
            HttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel register)
        {
            User newUser = new()
            {
                Name = register.Name,
                Email = register.Email,
                UserName = register.Email,
                Address = register.Email
            };
            var result = await _userManager.CreateAsync(newUser, register.Password);

            if (result.Succeeded)
                await _signInManager.SignInAsync(newUser, false);

            return result;
        }

        public async Task<SignInResult> LogIn(LoginViewModel login) =>
             await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, false);

        public async Task LogOut() =>
             await _signInManager.SignOutAsync();

        public void Dispose() =>
            GC.SuppressFinalize(this);

        public string GetUserId()
        {
            var user = _httpContext.HttpContext.User;
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
