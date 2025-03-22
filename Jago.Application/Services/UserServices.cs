using Jago.Application.Interfaces.Services;
using Jago.CrossCutting.Constants;
using Jago.CrossCutting.Dto;
using Jago.domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Jago.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly HttpContextAccessor _httpContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager, 
            HttpContextAccessor httpContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
            _roleManager = roleManager;
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
            var addRole = await _userManager.AddToRoleAsync(newUser, register.Role);

            if (result.Succeeded && addRole.Succeeded)
                await _signInManager.SignInAsync(newUser, false);

            return result;
        }

        public async Task<SignInResult> LogIn(LoginViewModel login) =>
             await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, false);

        public async Task LogOut() =>
             await _signInManager.SignOutAsync();

        public string? GetUserId()
        {
            var user = _httpContext.HttpContext.User;
            if (user is null)
                throw new UnauthorizedAccessException();
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public string? GetUserRole()
        {
            var user = _httpContext.HttpContext.User;
            if (user is null)
                throw new UnauthorizedAccessException();
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }

        public async Task<List<string?>> GetAllRoles() =>   
            await _roleManager.Roles.Select(_ => _.Name).ToListAsync();

        public async Task<bool> GetCurrentUser(ClaimsPrincipal claimsIdentity)
        {
            var user = await _userManager.GetUserAsync(claimsIdentity);
            bool isAdmin = user is not null && await _userManager.IsInRoleAsync(user, Constants.Role);
            return isAdmin;
        }

        public void Dispose() =>
            GC.SuppressFinalize(this);
    }
}
