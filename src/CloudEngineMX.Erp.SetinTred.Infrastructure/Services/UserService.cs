using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Identity;
    using Data.ViewModels;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users
                .Where(x => x.UserName == email)
                .Include(x => x.UserCore).ThenInclude(x => x.Area)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.Remember,
                false);
        }

        /// <summary>
        /// Logouts the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Changes the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        /// <summary>
        /// Validates the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        /// <summary>
        /// Checks the role asynchronous.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        /// <summary>
        /// Adds the user to role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        /// <summary>
        /// Determines whether [is user in role asynchronous] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        /// <summary>
        /// Generates the email confirmation token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        /// <summary>
        /// Confirms the email asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.Users.Include(x => x.UserCore).Where(x => x.Id.Equals(userId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        /// <summary>
        /// Remoses the user from role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public async Task RemosUserFromRoleAsync(User user, string roleName)
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task DeleteUserAsync(User user)
        {
            await _userManager.DeleteAsync(user);
        }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager
                .Users.Include(x => x.UserCore)
                .ThenInclude(x => x.Area)
                .ToListAsync();
        }
    }
}

