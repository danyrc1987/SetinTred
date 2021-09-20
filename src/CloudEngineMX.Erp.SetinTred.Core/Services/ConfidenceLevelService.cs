namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IConfidenceLevelService" />
    public class ConfidenceLevelService : IConfidenceLevelService
    {
        private readonly IRepository<ConfidenceLevel> _confidenceLevelRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfidenceLevelService"/> class.
        /// </summary>
        /// <param name="confidenceLevelRepository">The confidence level repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public ConfidenceLevelService(
            IRepository<ConfidenceLevel> confidenceLevelRepository,
            IUnitOfWork unitOfWork)
        {
            _confidenceLevelRepository = confidenceLevelRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Adds the confidence level asynchronous.
        /// </summary>
        /// <param name="confidenceLevel">The confidence level.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">confidenceLevel</exception>
        public async Task<bool> AddConfidenceLevelAsync(ConfidenceLevel confidenceLevel)
        {
            if (confidenceLevel == null)
                throw new ArgumentNullException(nameof(confidenceLevel));
            try
            {
                await _confidenceLevelRepository.AddAsync(confidenceLevel);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the confidence level asynchronous.
        /// </summary>
        /// <param name="confidenceLevel">The confidence level.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">confidenceLevel</exception>
        public async Task<bool> UpdateConfidenceLevelAsync(ConfidenceLevel confidenceLevel)
        {
            if (confidenceLevel == null)
                throw new ArgumentNullException(nameof(confidenceLevel));
            try
            {
                _confidenceLevelRepository.Update(confidenceLevel);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the confidence level by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<ConfidenceLevel> GetConfidenceLevelByKeyAsync(string key)
        {
            return await _confidenceLevelRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }
        /// <summary>
        /// Gets the confidence level by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ConfidenceLevel> GetConfidenceLevelByIdAsync(int id)
        {
            return await _confidenceLevelRepository.FirstOrDefaultAsync(
                predicate: x => x.Id.Equals(id));
        }
        /// <summary>
        /// Gets all confidence levels asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ConfidenceLevel>> GetAllConfidenceLevelsAsync()
        {
            return await _confidenceLevelRepository.GetAllAsync();
        }
        /// <summary>
        /// Gets all confidence levels combo asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetAllConfidenceLevelsComboAsync()
        {
            var levels = await _confidenceLevelRepository.GetAllAsync(
                predicate: x => x.IsActive);

            var lst = levels.Select(x => new SelectListItem
            {
                Text = x.ConfidenceLevelName,
                Value = x.Key
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Text = "[Debes de Seleccionar Un Nivel...]",
                Value = String.Empty
            });

            return lst;
        }
    }
}
