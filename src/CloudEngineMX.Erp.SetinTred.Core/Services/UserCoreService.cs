using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;

    /// <summary>
    ///Implementation the User coreService
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IUserCoreService" />
    public class UserCoreService : IUserCoreService
    {
        private readonly IRepository<UserCore> _userCoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCoreService"/> class.
        /// </summary>
        /// <param name="userCoreRepository">The user core repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UserCoreService(
            IRepository<UserCore> userCoreRepository,
            IUnitOfWork unitOfWork)
        {
            _userCoreRepository = userCoreRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the user core asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">userCore</exception>
        public async Task<bool> CreateUserCoreAsync(UserCore userCore)
        {
            if (userCore == null)
                throw new ArgumentNullException(nameof(userCore));
            try
            {
                await _userCoreRepository.AddAsync(userCore);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the user core asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">userCore</exception>
        public async Task<bool> UpdateUserCoreAsync(UserCore userCore)
        {
            if (userCore == null)
                throw new ArgumentNullException(nameof(userCore));

            try
            {
                _userCoreRepository.Update(userCore);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all users combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllUsersComboAsync()
        {
            var users = await _userCoreRepository.GetAllAsync(
                predicate: x => x.IsActive,
                include: i => i.Include(x => x.Area)
                    .Include(x => x.Report));

            var lst = users.Select(x => new SelectListItem
            {
                Value = x.Key,
                Text = x.FullName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona un Responsable]"
            });

            return lst;
        }

        /// <summary>
        /// Gets the combo users asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetComboUsersAsync()
        {
            var users = await _userCoreRepository.GetAllAsync(
                predicate: x => x.IsActive,
                include: i => i.Include(x => x.Area)
                    .Include(x => x.Report));

            var lst = users.Select(x => new SelectListItem
            {
                Value = x.FullName,
                Text = x.FullName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona un Responsable]"
            });

            return lst;
        }

        /// <summary>
        /// Gets the user core by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<UserCore> GetUserCoreByKeyAsync(string key)
        {
            return await _userCoreRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.Area)
                    .Include(x => x.Report));
        }

        /// <summary>
        /// Gets the user general direction asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<UserCore> GetUserGeneralDirectionAsync(string email)
        {
            return await _userCoreRepository.FirstOrDefaultAsync(
                predicate: x => x.Email.Equals(email),
                include: i => i.Include(x => x.Area));
        }

        public async Task<IEnumerable<UserCore>> GetPurchaseUsersAsync()
        {
            return await _userCoreRepository.GetAllAsync(
                predicate: x => x.Area.AreaName.Equals("Compras") && x.IsActive,
                include: i => i.Include(x => x.Area));
        }
    }
}
