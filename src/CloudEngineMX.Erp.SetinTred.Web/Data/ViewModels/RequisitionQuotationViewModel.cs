using Microsoft.AspNetCore.Http;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class RequisitionQuotationViewModel
    {
        public string RequisitionKey { get; set; }
        public string QuotationKey { get; set; }
        public string RouteFile { get; set; }
        public string FileName { get; set; }

        public IFormFile QuotationFile { get; set; }
    }
}
