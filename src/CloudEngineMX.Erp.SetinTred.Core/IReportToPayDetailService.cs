using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Core
{
    public interface IReportToPayDetailService
    {
        Task<IEnumerable<ReportToPayDetail>> ReportToPayDetailExcelAsync();
    }
}
