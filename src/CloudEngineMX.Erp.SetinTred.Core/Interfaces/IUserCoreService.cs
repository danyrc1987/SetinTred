using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    ///
    /// </summary>
    public interface IUserCoreService
    {

        /// <summary>
        /// Creates the user core asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        Task<bool> CreateUserCoreAsync(UserCore userCore);


        /// <summary>
        /// Updates the user core asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        Task<bool> UpdateUserCoreAsync(UserCore userCore);

        /// <summary>
        /// Gets all users combo asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetAllUsersComboAsync();

        /// <summary>
        /// Gets the combo users asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetComboUsersAsync();

        /// <summary>
        /// Gets the user core by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<UserCore> GetUserCoreByKeyAsync(string key);

        /// <summary>
        /// Gets the user general direction asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<UserCore> GetUserGeneralDirectionAsync(string email);

        /// <summary>
        /// Gets the purchase users asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserCore>> GetPurchaseUsersAsync();
    }
}
