using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class CostCenterService : ICostCenterService
    {
        private readonly IRepository<CostCenter> _costCenterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CostCenterService(
            IRepository<CostCenter> costCenterRepository,
            IUnitOfWork unitOfWork)
        {
            _costCenterRepository = costCenterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCostCenterAsync(CostCenter costCenter)
        {
            if (costCenter == null)
                throw new ArgumentNullException(nameof(costCenter));
            try
            {
                await _costCenterRepository.AddAsync(costCenter);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCostCentarAsync(CostCenter costCenter)
        {
            if (costCenter == null)
                throw new ArgumentNullException(nameof(costCenter));
            try
            {
                _costCenterRepository.Update(costCenter);
                await _unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<CostCenter>> GetAllCostCenterAsync()
        {
            return await _costCenterRepository.GetAllAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetCostCenterByComboAsync()
        {
            var costCenter = await _costCenterRepository.GetAllAsync(
                predicate: x => x.IsActive);

            var lst = costCenter.Select(x => new SelectListItem
            {
                Text = x.CostCenterName,
                Value = x.CostCenterName
            }).ToList();

            lst.Insert(0, new SelectListItem
            {
                Value = string.Empty,
                Text = "[Selecciona Una Opción]"
            });

            return lst;
        }

        public async Task<CostCenter> GetCostCenterByKeyAsync(string key)
        {
            return await _costCenterRepository.FirstOrDefaultAsync(
                predicate: x => x.Key.Equals(key));
        }
    }
}
