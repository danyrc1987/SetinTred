namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IMaterialRequestService" />
    public class MaterialRequestService : IMaterialRequestService
    {
        private readonly IRepository<MaterialRequest> _materialRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialRequestService"/> class.
        /// </summary>
        /// <param name="materialRequestRepository">The material request repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public MaterialRequestService(
            IRepository<MaterialRequest> materialRequestRepository,
            IUnitOfWork unitOfWork)
        {
            _materialRequestRepository = materialRequestRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the material request asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">materialRequest</exception>
        public async Task<bool> AddMaterialRequestAsync(MaterialRequest materialRequest)
        {
            if (materialRequest == null)
                throw new ArgumentNullException(nameof(materialRequest));
            try
            {
                await _materialRequestRepository.AddAsync(materialRequest);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the material request asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">materialRequest</exception>
        public async Task<bool> UpdateMaterialRequestAsync(MaterialRequest materialRequest)
        {
            if (materialRequest == null)
                throw new ArgumentNullException(nameof(materialRequest));
            try
            {
                _materialRequestRepository.Update(materialRequest);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all material request asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MaterialRequest>> GetAllMaterialRequestAsync()
        {
            return await _materialRequestRepository.GetAllAsync(
                include: i => i.Include(x => x.UserCore)
                    .Include(x => x.MaterialRequestDetails));
        }

        /// <summary>
        /// Gets all material request by user asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MaterialRequest>> GetAllMaterialRequestByUserAsync(UserCore userCore)
        {
            return await _materialRequestRepository.GetAllAsync(
                predicate: x => x.UserCore.Equals(userCore),
                include: i => i.Include(x => x.UserCore)
                    .Include(x => x.MaterialRequestDetails).ThenInclude(x => x.Material));
        }

        /// <summary>
        /// Gets the material request by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<MaterialRequest> GetMaterialRequestByKeyAsync(string key)
        {
            return await _materialRequestRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key),
                include: i => i.Include(x => x.UserCore)
                    .Include(x => x.MaterialRequestDetails).ThenInclude(x => x.Material));
        }

        /// <summary>
        /// Gets the total request asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTotalRequestAsync()
        {
            return await _materialRequestRepository.CountAsync();
        }
    }
}
