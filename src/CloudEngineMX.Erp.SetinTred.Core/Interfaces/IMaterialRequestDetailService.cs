namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    ///
    /// </summary>
    public interface IMaterialRequestDetailService
    {
        /// <summary>
        /// Adds the request detail asynchronous.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <returns></returns>
        Task<bool> AddRequestDetailAsync(MaterialRequestDetail material);
        /// <summary>
        /// Updates the request detail asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        Task<bool> UpdateRequestDetailAsync(MaterialRequestDetail materialRequest);


        /// <summary>
        /// Gets all details by request asynchronous.
        /// </summary>
        /// <param name="materialRequest">The material request.</param>
        /// <returns></returns>
        Task<IEnumerable<MaterialRequestDetail>> GetAllDetailsByRequestAsync(MaterialRequest materialRequest);
    }
}
