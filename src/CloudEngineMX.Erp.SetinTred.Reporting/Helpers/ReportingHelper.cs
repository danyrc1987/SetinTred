using System;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using CloudEngineMX.Erp.SetinTred.Reporting.Models;
using Microsoft.Reporting.WebForms;

namespace CloudEngineMX.Erp.SetinTred.Reporting.Helpers
{
    public class ReportingHelper
    {
        private static ReportViewer CreateConfiguration()
        {
            var viewer = new ReportViewer();

            var reportingService = WebConfigurationManager.AppSettings["ReportingService"];
            viewer.ProcessingMode = ProcessingMode.Remote;
            viewer.ServerReport.ReportServerUrl = new Uri(reportingService);
            IReportServerCredentials irsc = new CustomReportCredentials(
                WebConfigurationManager.AppSettings["ReportingUser"],
                WebConfigurationManager.AppSettings["ReportingPass"],
                WebConfigurationManager.AppSettings["ReportingDomain"]);
            viewer.ServerReport.ReportServerCredentials = irsc;
            viewer.ShowParameterPrompts = false;
            viewer.SizeToReportContent = true;
            viewer.Width = Unit.Percentage(100);
            viewer.Height = Unit.Percentage(100);
            viewer.ShowToolBar = false;
            viewer.ZoomMode = ZoomMode.PageWidth;
            viewer.ServerReport.Refresh();

            return viewer;
        }

        public static ReportResponse GetPurchaseOrder(Guid orderKey)
        {
            try
            {
                var reportPath = WebConfigurationManager.AppSettings["PurchaseOrderReportPath"];
                var rViewer = CreateConfiguration();
                rViewer.ServerReport.ReportPath = reportPath;
                rViewer.ServerReport.SetDataSourceCredentials(new[]
                {
                    new DataSourceCredentials
                        {UserId = "racd1987_usersetintred", Password = "Vero_2015", Name = "SetinTred"}
                });
                var orderParam = new ReportParameter("OrderKey", orderKey.ToString());
                rViewer.ServerReport.SetParameters(orderParam);

                var fileByte = rViewer.ServerReport.Render("PDF", string.Empty, out var mimeType,
                    out var encoding, out var extension, out var streams, out _);

                return new ReportResponse
                {
                    Success = true,
                    ReportByte = fileByte,
                };
            }
            catch (Exception ex)
            {
                return new ReportResponse
                {
                    Success = false,
                    Mensaje = ex.Message,
                    Exception = ex
                };
            }
        }

        public static ReportResponse GetCustomerQuote(int id)
        {
            try
            {
                var reportPath = WebConfigurationManager.AppSettings["PurchaseOrderReportPath"];
                var rViewer = CreateConfiguration();
                rViewer.ServerReport.ReportPath = reportPath;
                rViewer.ServerReport.SetDataSourceCredentials(new[]
                {
                    new DataSourceCredentials
                        {UserId = "racd1987_usersetintred", Password = "Vero_2015", Name = "SetinTred"}
                });
                var orderParam = new ReportParameter("QuoteId", id.ToString());
                rViewer.ServerReport.SetParameters(orderParam);

                var fileByte = rViewer.ServerReport.Render("PDF", string.Empty, out var mimeType,
                    out var encoding, out var extension, out var streams, out _);

                return new ReportResponse
                {
                    Success = true,
                    ReportByte = fileByte,
                };
            }
            catch (Exception ex)
            {
                return new ReportResponse
                {
                    Success = false,
                    Mensaje = ex.Message,
                    Exception = ex
                };
            }
        }
    }
}
