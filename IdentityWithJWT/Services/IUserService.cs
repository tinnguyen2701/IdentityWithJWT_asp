using IdentityWithJWT.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityWithJWT.Api.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null) throw new NullReferenceException("Register model is null");

            if (model.Password != model.ConfirmPassword) return new UserManagerResponse
            {
                Message = "password not same with a confirm password",
                IsSuccess = false
            };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            
            if(result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "user did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
