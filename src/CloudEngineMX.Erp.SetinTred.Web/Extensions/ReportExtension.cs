using System;
using System.Collections.Generic;
using System.Drawing;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Web.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CloudEngineMX.Erp.SetinTred.Web.Extensions
{
    public static class ReportExtension
    {
        public static ResponseDocument CreateReportDetail(IEnumerable<ReportToPayDetail> lst)
        {
            try
            {
                using var documento = new ExcelPackage();
                var color = ColorTranslator.FromHtml("#427fcd");
                var fuente = ColorTranslator.FromHtml("#ffffff");
                var colorFromHex = ColorTranslator.FromHtml("#293a50");
                var workSheet = documento.Workbook.Worksheets.Add("Reporte Pagos Pendientes");

                workSheet.Row(1).Height *= 2.5;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Font.Size = 18;
                workSheet.Row(1).Style.Font.Color.SetColor(fuente);
                workSheet.Cells[1, 1].Value = $"Reporte Pagos Pendientes";
                workSheet.Cells[1, 1].AutoFitColumns();
                workSheet.Cells["A1:K1"].Merge = true;
                workSheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A1:K1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(color);

                workSheet.Row(2).Height *= 1.5;
                workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(2).Style.Font.Bold = true;
                workSheet.Row(2).Style.Font.Color.SetColor(fuente);
                workSheet.Cells["A2:K2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A2:K2"].Style.Fill.BackgroundColor.SetColor(colorFromHex);
                workSheet.Cells[2, 1].Value = "Fecha De Creación";
                workSheet.Cells[2, 2].Value = "# I.C.";
                workSheet.Cells[2, 3].Value = "Solicitante";
                workSheet.Cells[2, 4].Value = "Area";
                workSheet.Cells[2, 5].Value = "Importe";
                workSheet.Cells[2, 6].Value = "Moneda";
                workSheet.Cells[2, 7].Value = "Base Operativa";
                workSheet.Cells[2, 8].Value = "Aplicación";
                workSheet.Cells[2, 9].Value = "Clasificación";
                workSheet.Cells[2, 10].Value = "Proveedor";
                workSheet.Cells[2, 11].Value = "Tipo Proveedor";

                var record = 3;

                foreach (var item in lst)
                {
                    workSheet.Cells[record, 1].Value = item.CreationDate;
                    workSheet.Cells[record, 1].Style.Numberformat.Format = "yyyy-mm-dd";
                    workSheet.Cells[record, 1].AutoFitColumns();
                    workSheet.Cells[record, 2].Value = item.PurchaseOrderCode;
                    workSheet.Cells[record, 2].AutoFitColumns();
                    workSheet.Cells[record, 3].Value = item.Applicant;
                    workSheet.Cells[record, 3].AutoFitColumns();
                    workSheet.Cells[record, 4].Value = item.AreaName;
                    workSheet.Cells[record, 4].AutoFitColumns();
                    workSheet.Cells[record, 5].Value = item.Total;
                    workSheet.Cells[record, 5].Style.Numberformat.Format = "0.0";
                    workSheet.Cells[record, 5].AutoFitColumns();
                    workSheet.Cells[record, 6].Value = item.CurrencyName;
                    workSheet.Cells[record, 6].AutoFitColumns();
                    workSheet.Cells[record, 7].Value = item.OperatingBaseName;
                    workSheet.Cells[record, 7].AutoFitColumns();
                    workSheet.Cells[record, 8].Value = item.Application;
                    workSheet.Cells[record, 8].AutoFitColumns();
                    workSheet.Cells[record, 9].Value = item.Classification;
                    workSheet.Cells[record, 9].AutoFitColumns();
                    workSheet.Cells[record, 10].Value = item.FiscalName;
                    workSheet.Cells[record, 10].AutoFitColumns();
                    workSheet.Cells[record, 11].Value = item.IsCritical == true ? "Critico" : "No Critico";
                    workSheet.Cells[record, 11].AutoFitColumns();

                    record++;
                }

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                var file = $"Reporte_Pagos_Pendientes_{DateTime.Now:yyyy-MM-dd:hhmmss}.xlsx";

                return new ResponseDocument
                {
                    Message = file,
                    Success = true,
                    File = documento.GetAsByteArray()
                };
            }
            catch (Exception ex)
            {
                return new ResponseDocument
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public static ResponseDocument CreateReportGeneral(IEnumerable<ReportEntity> lst)
        {
            try
            {
                using var documento = new ExcelPackage();
                var color = ColorTranslator.FromHtml("#427fcd");
                var fuente = ColorTranslator.FromHtml("#ffffff");
                var colorFromHex = ColorTranslator.FromHtml("#293a50");
                var workSheet = documento.Workbook.Worksheets.Add("Reporte Pagos Global");

                workSheet.Row(1).Height *= 2.5;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Font.Size = 18;
                workSheet.Row(1).Style.Font.Color.SetColor(fuente);
                workSheet.Cells[1, 1].Value = $"Reporte Pagos Pendientes Global";
                workSheet.Cells[1, 1].AutoFitColumns();
                workSheet.Cells["A1:D1"].Merge = true;
                workSheet.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A1:D1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells["A1:D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1:D1"].Style.Fill.BackgroundColor.SetColor(color);

                workSheet.Row(2).Height *= 1.5;
                workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(2).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(2).Style.Font.Bold = true;
                workSheet.Row(2).Style.Font.Color.SetColor(fuente);
                workSheet.Cells["A2:D2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A2:D2"].Style.Fill.BackgroundColor.SetColor(colorFromHex);
                workSheet.Cells[2, 1].Value = "Proveedor";
                workSheet.Cells[2, 2].Value = "Solicitante";
                workSheet.Cells[2, 3].Value = "Moneda";
                workSheet.Cells[2, 4].Value = "Importe";

                var record = 3;

                foreach (var item in lst)
                {
                    workSheet.Cells[record, 1].Value = item.FiscalName;
                    workSheet.Cells[record, 1].AutoFitColumns();
                    workSheet.Cells[record, 2].Value = item.Applicant;
                    workSheet.Cells[record, 2].AutoFitColumns();
                    workSheet.Cells[record, 3].Value = item.CurrencyName;
                    workSheet.Cells[record, 3].AutoFitColumns();
                    workSheet.Cells[record, 4].Value = item.Amount;
                    workSheet.Cells[record, 4].Style.Numberformat.Format = "0.0";
                    workSheet.Cells[record, 4].AutoFitColumns();

                    record++;
                }

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                var file = $"Reporte_Pagos_Global_{DateTime.Now:yyyy-MM-dd:hhmmss}.xlsx";

                return new ResponseDocument
                {
                    Message = file,
                    Success = true,
                    File = documento.GetAsByteArray()
                };
            }
            catch (Exception ex)
            {
                return new ResponseDocument
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
