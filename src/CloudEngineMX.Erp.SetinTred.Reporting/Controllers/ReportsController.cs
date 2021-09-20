using System;
using System.IO;
using System.Web.Http;
using CloudEngineMX.Erp.SetinTred.Reporting.Helpers;
using CloudEngineMX.Erp.SetinTred.Reporting.HttpResult;

namespace CloudEngineMX.Erp.SetinTred.Reporting.Controllers
{
    [RoutePrefix("api/reports")]
    public class ReportsController : ApiController
    {
        [HttpGet]
        [Route("PurchaseOrder")]
        public IHttpActionResult GetPurchaseOrder(Guid orderKey)
        {
            var result = ReportingHelper.GetPurchaseOrder(orderKey);

            if (!result.Success) return InternalServerError(result.Exception);

            var dataStream = new MemoryStream(result.ReportByte);

            return new PdfFileResult(dataStream, Request, $"{orderKey}.pdf");
        }
    }
}
