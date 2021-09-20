namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Interface to operating base service.
    /// </summary>
    public interface IOperatingBaseService
    {
        Task<bool> AddOperatingBaseAsync(OperatingBase operatingBase);
        Task<bool> UpdateOperatingBaseAsync(OperatingBase operatingBase);
        Task<IEnumerable<OperatingBase>> GetAllOperatingBasesAsync();
        Task<OperatingBase> GetOperatingBaseByKeyAsync(string key);
        Task<OperatingBase> GetOperatingBaseByIdAsync(int id);
        Task<IEnumerable<SelectListItem>> GetOperatingBaseComboAsync();
        /// <summary>
        /// Gets the combo bases asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SelectListItem>> GetComboBasesAsync();
    }
}
