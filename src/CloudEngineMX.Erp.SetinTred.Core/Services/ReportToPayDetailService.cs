using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class ReportToPayDetailService : IReportToPayDetailService
    {
        private readonly IRepository<ReportToPayDetail> _reportToPayDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReportToPayDetailService(
            IRepository<ReportToPayDetail> reportToPayDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _reportToPayDetailRepository = reportToPayDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportToPayDetail>> ReportToPayDetailExcelAsync()
        {
            return await _reportToPayDetailRepository.ExecuteStoreProcedureAsync("ReportToPayDetail");
        }
    }
}
