using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CloudEngineMX.Erp.SetinTred.Reporting.HttpResult
{
    public class PdfFileResult : IHttpActionResult
    {
        private readonly MemoryStream _fileStuff;
        private readonly string _pdfFileName;
        private readonly HttpRequestMessage _httpRequestMessage;
        private HttpResponseMessage _httpResponseMessage;

        public PdfFileResult(MemoryStream data, HttpRequestMessage request, string filename)
        {
            _fileStuff = data;
            _httpRequestMessage = request;
            _pdfFileName = filename;
        }

        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            _httpResponseMessage = _httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            _httpResponseMessage.Content = new StreamContent(_fileStuff);
            _httpResponseMessage.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            _httpResponseMessage.Content.Headers.ContentDisposition.FileName = _pdfFileName;
            _httpResponseMessage.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return System.Threading.Tasks.Task.FromResult(_httpResponseMessage);
        }
    }
}
