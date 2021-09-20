using System.Collections.Generic;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;

namespace CloudEngineMX.Erp.SetinTred.Core.Services
{
    public class ReportToPayService : IReportToPayService
    {
        private readonly IRepository<ReportEntity> _reportEntityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReportToPayService(
            IRepository<ReportEntity> reportEntityRepository,
            IUnitOfWork unitOfWork)
        {
            _reportEntityRepository = reportEntityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportEntity>> ReportToPayApprovedAsync()
        {
            var lst = await _reportEntityRepository.ExecuteStoreProcedureAsync("Select * From View_ToPay");

            return lst;
        }
    }
}
