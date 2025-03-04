using Jago.CrossCutting.Dto;
using Microsoft.AspNetCore.Identity;

namespace Jago.Application.Interfaces.Services
{
    public interface IUserServices : IDisposable
    {
        Task<IdentityResult> RegisterUser(RegisterViewModel register);
        Task<SignInResult> LogIn(LoginViewModel login);
        Task LogOut();
        string GetUserId();
    }
}
