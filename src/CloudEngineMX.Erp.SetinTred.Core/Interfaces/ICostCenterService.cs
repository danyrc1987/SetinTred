using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    public interface ICostCenterService
    {
        Task<bool> AddCostCenterAsync(CostCenter costCenter);
        Task<bool> UpdateCostCentarAsync(CostCenter costCenter);
        Task<IEnumerable<CostCenter>> GetAllCostCenterAsync();
        Task<IEnumerable<SelectListItem>> GetCostCenterByComboAsync();
        Task<CostCenter> GetCostCenterByKeyAsync(string key);

    }
}
