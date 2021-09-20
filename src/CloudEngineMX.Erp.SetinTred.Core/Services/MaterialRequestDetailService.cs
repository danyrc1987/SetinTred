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
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IMaterialRequestDetailService" />
    public class MaterialRequestDetailService : IMaterialRequestDetailService
    {
        private readonly IRepository<MaterialRequestDetail> _materialRequestDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialRequestDetailService"/> class.
        /// </summary>
        /// <param name="materialRequestDetailRepository">The material request detail repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public MaterialRequestDetailService(
            IRepository<MaterialRequestDetail> materialRequestDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _materialRequestDetailRepository = materialRequestDetailRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the request detail asynchronous.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">material</exception>
        public async Task<bool> AddRequestDetailAsync(MaterialRequestDetail material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));
            try
            {
                await _materialRequestDetailRepository.AddAsync(material);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the request detail asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">materialRequest</exception>
        public async Task<bool> UpdateRequestDetailAsync(MaterialRequestDetail materialRequest)
        {
            if (materialRequest == null)
                throw new ArgumentNullException(nameof(materialRequest));
            try
            {
                _materialRequestDetailRepository.Update(materialRequest);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all details by request asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MaterialRequestDetail>> GetAllDetailsByRequestAsync(MaterialRequest materialRequest)
        {
            return await _materialRequestDetailRepository.GetAllAsync(
                predicate: x => x.MaterialRequest.Equals(materialRequest),
                include: i => i.Include(x => x.Material));
        }
    }
}
