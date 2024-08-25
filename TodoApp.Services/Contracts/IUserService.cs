using Microsoft.AspNetCore.Identity;
using TodoApp.Models;
using Google.Apis.Auth;

namespace TodoApp.Service.Contracts
{
    public interface IUserService
    {
        public Task<ServiceResult> Register(UserDTO user);
        public Task<ServiceResult> Login(LoginModel user);
        public Task<ServiceResult> GetUserDetails(string id);
        public Task<bool> IsExistingUser(string username);
        public Task<ServiceResult> GetUserId(string name);
        public Task<ServiceResult> RegisterGoogleDetails(GoogleJsonWebSignature.Payload data);




    }
}
