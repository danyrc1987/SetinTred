namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IMaterialRequestService
    {
        /// <summary>
        /// Adds the material request asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        Task<bool> AddMaterialRequestAsync(MaterialRequest materialRequest);
        /// <summary>
        /// Updates the material request asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        Task<bool> UpdateMaterialRequestAsync(MaterialRequest materialRequest);
        /// <summary>
        /// Gets all material request asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MaterialRequest>> GetAllMaterialRequestAsync();
        /// <summary>
        /// Gets all material request by user asynchronous.
        /// </summary>
        /// <param name="userCore">The user core.</param>
        /// <returns></returns>
        Task<IEnumerable<MaterialRequest>> GetAllMaterialRequestByUserAsync(UserCore userCore);
        /// <summary>
        /// Gets the material request by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<MaterialRequest> GetMaterialRequestByKeyAsync(string key);

        /// <summary>
        /// Gets the total request asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> GetTotalRequestAsync();
    }
}
