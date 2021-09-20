using System;

namespace CloudEngineMX.Erp.SetinTred.Reporting.Models
{
    public class ReportResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public byte[] ReportByte { get; set; }
        public Exception Exception { get; set; }
    }
}
