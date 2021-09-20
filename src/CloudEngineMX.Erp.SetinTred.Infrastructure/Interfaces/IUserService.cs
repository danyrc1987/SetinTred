using System.Collections.Generic;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces
{
    using System.Threading.Tasks;
    using Data.Identity;
    using Data.ViewModels;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    ///
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the user by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// Adds the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<IdentityResult> AddUserAsync(User user, string password);

        /// <summary>
        /// Logouts the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<SignInResult> LoginAsync(LoginViewModel model);

        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<IdentityResult> UpdateUserAsync(User user);

        /// <summary>
        /// Changes the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        /// <summary>
        /// Validates the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<SignInResult> ValidatePasswordAsync(User user, string password);

        /// <summary>
        /// Checks the role asynchronous.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        Task CheckRoleAsync(string roleName);

        /// <summary>
        /// Adds the user to role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        Task AddUserToRoleAsync(User user, string roleName);

        /// <summary>
        /// Determines whether [is user in role asynchronous] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        Task<bool> IsUserInRoleAsync(User user, string roleName);

        /// <summary>
        /// Generates the email confirmation token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        /// <summary>
        /// Confirms the email asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        /// <summary>
        /// Gets the user by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(string userId);

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<string> GeneratePasswordResetTokenAsync(User user);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        /// <summary>
        /// Remoses the user from role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        Task RemosUserFromRoleAsync(User user, string roleName);

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task DeleteUserAsync(User user);

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
