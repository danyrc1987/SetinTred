using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Core.Interfaces
{
    public interface IReportToPayService
    {
        Task<IEnumerable<ReportEntity>> ReportToPayApprovedAsync();
    }
}
