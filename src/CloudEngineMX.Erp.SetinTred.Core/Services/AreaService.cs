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
    /// <seealso cref="CloudEngineMX.Erp.SetinTred.Core.Interfaces.IAreaService" />
    public class AreaService : IAreaService
    {
        private readonly IRepository<Area> _areaRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaService"/> class.
        /// </summary>
        /// <param name="areaRepository">The area repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public AreaService(
            IRepository<Area> areaRepository,
            IUnitOfWork unitOfWork)
        {
            _areaRepository = areaRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the area asynchronous.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">area</exception>
        public async Task<bool> CreateAreaAsync(Area area)
        {
            if (area == null)
                throw new ArgumentNullException(nameof(area));
            try
            {
                await _areaRepository.AddAsync(area);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the area asynchronous.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">area</exception>
        public async Task<bool> UpdateAreaAsync(Area area)
        {
            if (area == null)
                throw new ArgumentNullException(nameof(area));
            try
            {
                _areaRepository.Update(area);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all areas asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Area>> GetAllAreasAsync()
        {
            return await _areaRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets the area by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<Area> GetAreaByKeyAsync(string key)
        {
            return await _areaRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }

        /// <summary>
        /// Gets the combo areas asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetComboAreasAsync()
        {
            var areas = await _areaRepository.GetAllAsync();

            var lst = areas.Select(x => new SelectListItem
            {
                Text = x.AreaName,
                Value = x.Key
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Text = "[Debes de seleccionar una area...]",
                Value = string.Empty,
            });

            return lst;
        }
    }
}
