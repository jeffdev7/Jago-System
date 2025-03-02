using Jago.Application.Interfaces.Services;
using Jago.CrossCutting.Dto;
using Jago.domain.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Jago.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
    }
}
