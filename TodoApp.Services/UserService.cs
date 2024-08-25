using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Entites;
using TodoApp.Models;
using TodoApp.Service.Contracts;

namespace TodoApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IUserStore<User> _userStore;
        IUserEmailStore<User> _userEmailStore;
        public UserService(UserManager<User> userManager,
             SignInManager<User> signInManager,
             IUserStore<User> userStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _userEmailStore = GetEmailStore();

        }
        public async Task<ServiceResult> Register(UserDTO userDTO)
        {
            User user = CreateUser();
            await _userStore.SetUserNameAsync(user, userDTO.Username, CancellationToken.None);
            user.Name = userDTO.Username;
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                var createdUser = await GetUser(userDTO.Username);
                return new ServiceResult
                {
                    IsSucceeded = true,
                    Message = "SignIn Successful New User Created",
                    Value=createdUser.Value
                };
            }
            else
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                new ServiceResult
                {
                    IsSucceeded = false,
                    Value = userDTO.Username,
                    Message = errors
                };

            }
            return new  ServiceResult{ IsSucceeded = false, Message = "Failed to create user" };


        }
        public async Task<ServiceResult> RegisterGoogleDetails(GoogleJsonWebSignature.Payload data)
        {
            User user = CreateUser();
            await _userStore.SetUserNameAsync(user, data.Email, CancellationToken.None);
            await _userEmailStore.SetEmailAsync(user, data.Email, CancellationToken.None);
            user.Name = data.Name;
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var createdUser = await GetUser(data.Email);
                return new ServiceResult
                {
                    IsSucceeded = true,
                    Message = "SignIn Successful New User Created",
                    Value = ((User)createdUser.Value).Id.ToString(),
                };

            }
            else
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                new ServiceResult
                {
                    IsSucceeded = false,
                    Message = errors
                };

            }
            return new ServiceResult { IsSucceeded = false, Message = "Failed to create user" };

        }

        public async Task<ServiceResult> Login(LoginModel userDTO)
        {
            var user = await _userManager.FindByNameAsync(userDTO.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, userDTO.Password, false, false);
                if (result.Succeeded)
                {
                    return new ServiceResult
                    {
                        IsSucceeded = true,
                        Message = "Login Successful",
                        Value = user.Id.ToString(),
                    };
                }
                else
                {
                    return new ServiceResult
                    {
                        IsSucceeded = false,
                        Message = "Login Failed",
                        Value = user.Id.ToString(),
                    };
                }
            }
            return new ServiceResult
            {
                IsSucceeded = false,
                Message = "Login Failed! User do not Exist",
                Value=userDTO.Username
            };
        }
        public async Task<bool> IsExistingUser(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username);
        }
        private async Task<ServiceResult> GetUser(string name)
        {
            var user =await _userManager.FindByNameAsync(name);
            if (user is null)
            {
                return new ServiceResult { IsSucceeded = false, Message = "User Not Found", Value = name };
            }
            return new ServiceResult { Value = user ,IsSucceeded=true,Message="User Found"};
        }
        public async Task<ServiceResult> GetUserId(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user is null)
            {
                return new ServiceResult { IsSucceeded = false, Message = "User Not Found", Value = name };
            }
            return new ServiceResult { IsSucceeded=true,Message="User Id",Value=user.Id.ToString()};
        }
        public async Task<ServiceResult> GetUserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                return new ServiceResult { IsSucceeded=false,Message="User Not Found",Value=id };

            }
            var userDetails= new UserView
            {
                Name = user?.Name ?? "Not Exists",  
                Email = user?.Email ?? "Not Exists",  // Use null-conditional operator for Email
                PhoneNumber = user?.PhoneNumber ?? "Not Exists",  // Use null-conditional operator for PhoneNumber
                Username = user?.UserName ?? "Not Exists"  // Use null-conditional operator for Username
            };
            return new ServiceResult { IsSucceeded=true,Value=userDetails,Message="User Found"};
        }
        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'");
            }
        }
        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }

    }
}
