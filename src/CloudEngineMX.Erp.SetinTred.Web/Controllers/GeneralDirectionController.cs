using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudEngineMX.Erp.SetinTred.Core;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces;
using CloudEngineMX.Erp.SetinTred.Web.Data;
using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using CloudEngineMX.Erp.SetinTred.Web.Extensions;
using CloudEngineMX.Erp.SetinTred.Web.Extensions.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    [Authorize]
    public class GeneralDirectionController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRequisitionService _requisitionService;
        private readonly IMapper _mapper;
        private readonly IRequisitionDetailService _requisitionDetailService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMailService _mailService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPurchaseOrderDetailService _purchaseOrderDetailService;
        private readonly IPreQuotationService _preQuotationService;
        private readonly ISupplierService _supplierService;
        private readonly IReportToPayService _reportToPayService;
        private readonly IReportToPayDetailService _reportToPayDetailService;
        private readonly IUserCoreService _userCoreService;
        private readonly ICustomerQuoteService _customerQuoteService;
        private readonly ICustomerQuoteDetailService _customerQuoteDetailService;

        public GeneralDirectionController(
            IUserService userService,
            IRequisitionService requisitionService,
            IMapper mapper,
            IRequisitionDetailService requisitionDetailService,
            IWebHostEnvironment hostEnvironment,
            IMailService mailService,
            IPurchaseOrderService purchaseOrderService,
            IPurchaseOrderDetailService purchaseOrderDetailService,
            IPreQuotationService preQuotationService,
            ISupplierService supplierService,
            IReportToPayService reportToPayService,
            IReportToPayDetailService reportToPayDetailService,
            IUserCoreService userCoreService,
            ICustomerQuoteService customerQuoteService,
            ICustomerQuoteDetailService customerQuoteDetailService)
        {
            _userService = userService;
            _requisitionService = requisitionService;
            _mapper = mapper;
            _requisitionDetailService = requisitionDetailService;
            _hostEnvironment = hostEnvironment;
            _mailService = mailService;
            _purchaseOrderService = purchaseOrderService;
            _purchaseOrderDetailService = purchaseOrderDetailService;
            _preQuotationService = preQuotationService;
            _supplierService = supplierService;
            _reportToPayService = reportToPayService;
            _reportToPayDetailService = reportToPayDetailService;
            _userCoreService = userCoreService;
            _customerQuoteService = customerQuoteService;
            _customerQuoteDetailService = customerQuoteDetailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RequisitionList()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            if (!user.UserCore.Area.AreaName.Equals("Direccion General"))
            {
                return RedirectToAction("Index", "Warehouse")
                    .WithDanger(Constants.ErrorMessage, "NO tienes los permisos requeridos");
            }

            var requisitions = await _requisitionService.GetAllRequisitionsAsync();
            var model = new RequisitionsListViewModel
            {
                Requisitions = _mapper.Map<IEnumerable<RequisitionViewModel>>(
                    requisitions.Where(x => x.Status.Equals(Constants.RequestToAuthorizePurchase)))
            };


            return View(model);
        }

        public async Task<IActionResult> ApproveOrder(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("RequisitionList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "La requisición seleccionado no existe");
            }

            var requisition = await _requisitionService.GetRequisitionByKeyAsync(id);
            var items = await _requisitionDetailService.GetAllRequisitionDetailAsync(requisition);

            var model = new RequisitionDetailViewModel
            {
                Requisition = _mapper.Map<RequisitionViewModel>(requisition),
                Items = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(items),
                Quotations = _mapper.Map<IEnumerable<RequisitionQuotationViewModel>>(requisition.PreQuotations)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RejectItem(CancelOrderViewModel model)
        {
            var item = await _requisitionDetailService.GetRequisitionDetailByKeyAsync(model.Id);

            item.IsDispatched = false;
            item.IsApproved = false;
            item.ReasonForRejection = model.Comments;

            var update = await _requisitionDetailService.UpdateRequisitionDetailAsync(item);

            if (!update)
            {

                return RedirectToAction("ApproveOrder", "GeneralDirection", new { id = item.Requisition.Key })
                    .WithWarning(Constants.ErrorMessage, "Ocurrió un error intenta de nuevo");
            }

            return RedirectToAction("ApproveOrder", "GeneralDirection", new { id = item.Requisition.Key })
                .WithSuccess(Constants.SuccessMessage, "Material rechazado Correctamente");
        }

        public async Task<IActionResult> ApprovePurchase(string id)
        {
            var item = await _requisitionService.GetRequisitionByKeyAsync(id);
            var buyers = await _userCoreService.GetPurchaseUsersAsync();

            item.Status = Constants.RequestValidate;

            var update = await _requisitionService.UpdateRequisitionAsync(item);

            if (!update)
            {
                return RedirectToAction("ApproveOrder", "GeneralDirection", new { id = item.Key })
                    .WithWarning(Constants.ErrorMessage, "Ocurrió un error intenta de nuevo");
            }

            var userPurchase = await _userService.GetUserByEmailAsync("comprasnorte@setintred.com");

            var link = Url.Action("ApproveOrder", "GeneralDirection", new { id = item.Key },
                protocol: HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            var materials = await _requisitionDetailService.GetAllRequisitionDetailAsync(item);
            var lst = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(materials);
            var lstToMail = (from p in lst where p.IsApproved select p.ToMailPurchase).ToList();
            var lstBuy = (from p in buyers select p.Email).ToList();
            var lstMailCancel = (from p in lst where !p.IsApproved && p.QuantityToBuy > 0 select p.ToMailPurchaseCancel).ToList();
            string[] labels =
            {
                "[#UserName]", "[#RequisitionCode]","[#Status]","[#Owner]", "[#Area]", "[#OperatingBase]", "[#Classification]",
                "[#Application]", "[#Materials]","[#MaterialsReject]","[#Link]"
            };
            string[] data =
            {
                userPurchase.UserCore.FullName, item.RequisitionCode, item.Status, item.UserCore.FullName, item.Area.AreaName,
                item.OperatingBase.OperatingBaseName,
                item.Classification, item.Application, string.Join("<br />", lstToMail.ToArray()), string.Join("<br />",lstMailCancel.ToArray()), link
            };
            string[] to = { userPurchase.Email, item.UserCore.Email };
            string[] cc = { string.Join(';', lstBuy.ToArray()) };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/PurchaseApprobe.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Requisición de compra aprobada: {item.RequisitionCode}", view);

            return RedirectToAction("RequisitionList", "GeneralDirection")
                .WithSuccess(Constants.SuccessMessage, "Solicitud finaliza correctamente");
        }

        public async Task<IActionResult> PurchaseOrderList()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            if (!user.UserCore.Area.AreaName.Equals("Direccion General"))
            {
                return RedirectToAction("Index", "Warehouse")
                    .WithDanger(Constants.ErrorMessage, "NO tienes los permisos requeridos");
            }

            var orders = await _purchaseOrderService.GetAllPurchaseOrderAsync();

            var model = new PurchaseOrdersViewModel
            {
                Orders = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(orders)
            };

            return View(model);
        }

        public async Task<IActionResult> PurchaseOrderDetail(string id)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(id);

            var model = _mapper.Map<PurchaseOrderViewModel>(order);
            model.Details = _mapper.Map<IEnumerable<PurchaseOrderDetailViewModel>>(order.PurchaseOrderDetails);
            model.Quotations = order.Quotations.Select(x => new PurchaseOrderQuotationViewModel
            {
                FileName = x.FileName,
                QuotationKey = x.Key,
                RouteFile = x.RouteFile
            }).ToList();
            return View(model);
        }

        public async Task<IActionResult> ValidatePurchaseOrder(string id)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(id);

            var model = _mapper.Map<PurchaseOrderViewModel>(order);
            model.Details = _mapper.Map<IEnumerable<PurchaseOrderDetailViewModel>>(order.PurchaseOrderDetails);
            model.Quotations = order.Quotations.Select(x => new PurchaseOrderQuotationViewModel
            {
                FileName = x.FileName,
                QuotationKey = x.Key,
                RouteFile = x.RouteFile
            }).ToList();
            return View(model);
        }

        public async Task<IActionResult> DeleteItemPurchaseOrder(string id)
        {
            var item = await _purchaseOrderDetailService.GetDetailByKeyAsync(id);

            item.IsApproved = false;

            var response = await _purchaseOrderDetailService.UpdatePurchaseOrderDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("ValidatePurchaseOrder", "GeneralDirection", new { id = item.PurchaseOrder.Key })
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error al rechazar el item, intenta de nuevo.");
            }

            return RedirectToAction("ValidatePurchaseOrder", "GeneralDirection", new { id = item.PurchaseOrder.Key })
                .WithSuccess(Constants.SuccessMessage, "Item rechazado correctamente");
        }

        public async Task<IActionResult> ApprovePurchaseOrder(string id)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(id);
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var subtotal = order.PurchaseOrderDetails.Sum(x => x.Quantity * x.UnitPrice);
            var vat = 0m;
            var buyers = await _userCoreService.GetPurchaseUsersAsync();
            var lstBuy = (from p in buyers select p.Email).ToList();

            if (order.RequiresVat)
            {
                vat = subtotal * .16m;
            }

            var total = subtotal + vat;

            var letters = Converter.NumeroALetras(total, order.Currency.CurrencyName);
            var guid = order.Key;
            var split = guid.Split('-');
            var sing = $"{split[4]}-{user.UserCore.FullName.Replace(" ", "")}-{DateTime.Now:yyyMMdd}";

            var validate = order.PurchaseOrderDetails.Count(x => x.IsApproved);
            var totalItems = order.PurchaseOrderDetails.Count;

            if (validate == 0)
            {
                return RedirectToAction("ValidatePurchaseOrder", "GeneralDirection", new { id = id })
                    .WithDanger(Constants.ErrorMessage, "No puedes aprobar esta instrucción de Compra, todos los materiales están rechazados.");
            }

            if (validate < totalItems)
            {
                order.Status = Constants.PurchaseOrderCompletePartialy;
            }

            order.Status = Constants.PurchaseOrderComplete;
            order.DigitalSignature = sing;
            order.TotalInLetters = letters;
            order.LiberationDate = DateTime.Now;

            var response = await _purchaseOrderService.UpdatePurchaseOrderAsync(order);

            if (!response)
            {
                return RedirectToAction("ValidatePurchaseOrder", "GeneralDirection", new { id = id })
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error al aprobar la instrucción de compra.");
            }
            var requisition = await _requisitionService.GetRequisitionByCodeAsync(order.RequisitionCode);
            var link = Url.Action("PurchaseOrderList", "Purchases", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            var materials = await _purchaseOrderDetailService.GetAllItemsByOrderAsync(order);
            var lst = _mapper.Map<IEnumerable<PurchaseOrderDetailViewModel>>(materials);
            var lstToMail = (from p in lst where p.IsApproved select p.ToMail).ToList();
            string[] labels =
            {
                "[#PurchaseUser]", "[#PurcharseOrderCode]", "[#DirectionUser]", "[#Items]", "[#Link]"
            };
            string[] data =
            {
                order.UserCore.FullName, order.PurchaseOrderCode, user.UserCore.FullName,  string.Join("<br />", lstToMail.ToArray()), link
            };
            string[] to = { order.UserCore.Email, requisition.UserCore.Email };
            string[] cc = { string.Join(";", lstBuy.ToArray()) };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/PurchaseOrderValidate.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Instrucción de Compra Autorizada: {order.PurchaseOrderCode}", view);

            return RedirectToAction("PurchaseOrderList", "GeneralDirection")
                .WithSuccess(Constants.SuccessMessage, "Instrucción de compra aprobada correctamente");
        }

        [HttpPost]
        public async Task<IActionResult> RejectPurchase(CancelOrderViewModel model)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(model.Id);
            purchaseOrder.Status = Constants.PurchaseOrderCancel;
            purchaseOrder.CancelComments = model.Comments;

            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var response = await _purchaseOrderService.UpdatePurchaseOrderAsync(purchaseOrder);

            var requisition = await _requisitionService.GetRequisitionByCodeAsync(purchaseOrder.RequisitionCode);

            var link = Url.Action("Index", "Home", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            string[] labels =
            {
                "[#PurchaseUser]", "[#PurcharseOrderCode]", "[#DirectionUser]", "[#Comments]", "[#Link]"
            };
            string[] data =
            {
                purchaseOrder.UserCore.FullName, purchaseOrder.PurchaseOrderCode, user.UserCore.FullName,
                model.Comments, link
            };
            string[] to = { purchaseOrder.UserCore.Email };
            string[] cc = { requisition.UserCore.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/CancelPurchaseOrder.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Instrucción de Compra Rechazada: {purchaseOrder.PurchaseOrderCode}", view);

            return !response ? StatusCode(StatusCodes.Status400BadRequest) : Ok();
        }

        public async Task<IActionResult> AddQuantity(string id)
        {
            var item = await _purchaseOrderDetailService.GetDetailByKeyAsync(id);

            var model = new PurchaseOrderDetailViewModel
            {
                PurchaseOrderDetailKey = id,
                Quantity = item.Quantity
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItem(PurchaseOrderDetailViewModel model)
        {
            var item = await _purchaseOrderDetailService.GetDetailByKeyAsync(model.PurchaseOrderDetailKey);

            item.OriginalQuantity = item.Quantity;
            item.Quantity = model.Quantity;

            var response = await _purchaseOrderDetailService.UpdatePurchaseOrderDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("ValidatePurchaseOrder", "GeneralDirection", new { id = item.PurchaseOrder.Key })
                    .WithDanger(Constants.ErrorMessage, "No fue posible modificar la cantidad.");
            }

            return RedirectToAction("ValidatePurchaseOrder", "GeneralDirection", new { id = item.PurchaseOrder.Key })
                .WithDanger(Constants.SuccessMessage, "Cantidad Modificada Correctamente");
        }

        public async Task<IActionResult> DeleteRequisition(string id)
        {
            var requisition = await _requisitionService.GetRequisitionByKeyAsync(id);

            if (requisition == null)
            {
                return RedirectToAction("RequisitionList", "GeneralDirection")
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error al borrar la requisición.");
            }

            requisition.Status = Constants.RequestCancelDg;

            var update = await _requisitionService.UpdateRequisitionAsync(requisition);

            if (!update)
            {
                return RedirectToAction("RequisitionList", "GeneralDirection")
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error al borrar la requisición.");
            }

            return RedirectToAction("RequisitionList", "GeneralDirection")
                .WithSuccess(Constants.SuccessMessage, "Requisición borrada correctamente.");
        }

        public async Task<IActionResult> PurchaseInstruccionPayable()
        {
            //var test = await _reportToPayService.ReportToPayApprovedAsync();

            var lst = await _reportToPayService.ReportToPayApprovedAsync();

            var model = new ReportsToPayViewModel
            {
                Pays = lst.Select(x => new ReportsToPayViewModel
                {
                    Amount = x.Amount,
                    FiscalName = x.FiscalName,
                    CurrencyName = x.CurrencyName,
                    Applicant = x.Applicant,
                    SupplierKey = x.Key
                }).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> DownReportDetail()
        {
            var data = await _reportToPayDetailService.ReportToPayDetailExcelAsync();

            var report = ReportExtension.CreateReportDetail(data);

            if (data == null)
            {
                return RedirectToAction("PurchaseInstruccionPayable", "GeneralDirection")
                    .WithInfo(Constants.SuccessMessage, "No hay información para generar el reporte.");
            }

            return File(report.File, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", report.Message);
        }

        public async Task<IActionResult> DownGeneralReport()
        {
            var data = await _reportToPayService.ReportToPayApprovedAsync();

            var report = ReportExtension.CreateReportGeneral(data);

            if (data == null)
            {
                return RedirectToAction("PurchaseInstruccionPayable", "GeneralDirection")
                    .WithInfo(Constants.SuccessMessage, "No hay información para generar el reporte.");
            }

            return File(report.File, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", report.Message);
        }

        public async Task<IActionResult> DetailSupplier(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("PurchaseInstruccionPayable", "GeneralDirection")
                    .WithDanger(Constants.ErrorMessage, "Debes de seleccionar un proveedor.");
            }

            var supplier = await _supplierService.GetSupplierByKeyAsync(id);

            if (supplier == null)
            {
                return RedirectToAction("PurchaseInstruccionPayable", "GeneralDirection")
                    .WithDanger(Constants.ErrorMessage, "El proveedor seleccionado no existe.");
            }

            var data = await _purchaseOrderService.GetAllPurchaseOrderBySupplierAsync(supplier);

            var lst = new List<ReportToPayDetail>();

            foreach (var item in data.Where(x => x.Status.Equals("Aprobada")))
            {
                var request = await _requisitionService.GetRequisitionByCodeAsync(item.RequisitionCode);

                lst.Add(new ReportToPayDetail
                {
                    KeyDescription = item.KeyDescription,
                    CurrencyName = item.Currency.CurrencyName,
                    Id = item.Id,
                    Applicant = item.Applicant,
                    FiscalName = item.Supplier.FiscalName,
                    Total = item.PurchaseOrderDetails.Sum(d => d.Quantity * d.UnitPrice),
                    CreationDate = item.CreationDate,
                    Key = item.Key,
                    PurchaseOrderCode = item.PurchaseOrderCode,
                    OperatingBaseName = request.OperatingBase.OperatingBaseName,
                    Application = request.Application,
                    Classification = request.Classification,
                    IsCritical = item.Supplier.IsCritical,
                    AreaName = request.Area.AreaName
                });
            }

            //var model = data.Select(x => new ReportToPayDetail
            //{
            //    KeyDescription = x.KeyDescription,
            //    CurrencyName = x.Currency.CurrencyName,
            //    Id = x.Id,
            //    Applicant = x.Applicant,
            //    FiscalName = x.Supplier.FiscalName,
            //    Total = x.PurchaseOrderDetails.Sum(d => d.Quantity * d.UnitPrice),
            //    CreationDate = x.CreationDate,
            //    Key = x.Key,
            //    PurchaseOrderCode = x.PurchaseOrderCode
            //}).ToList();

            return View(lst);
        }

        #region Cotizaciones
        public async Task<IActionResult> CustomerQuotesList()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var data = await _customerQuoteService.GetAllCustomerQuotesAsync();
            var model = new CustomerQuotesViewModel
            {
                Quotes = data.Select(x => new CustomerQuotesViewModel
                {
                    Status = x.Status,
                    PaymentType = x.User.FullName,
                    QuoteCode = x.QuoteCode,
                    QuoteType = x.QuoteType,
                    DeliveryTime = x.DeliveryTime,
                    CreationDate = x.CreationDate.ToShortDateString(),
                    CurrencyName = x.Currency.CurrencyName,
                    QuoteKey = x.Key,
                    CustomerName = x.Customer.SocialName,
                    Details = x.CustomerQuoteDetails.Select(x => new ItemCustomerQuoteViewModel
                    {
                        Availability = x.Availability,
                        Consecutive = x.Consecutive,
                        CustomerQuoteId = x.CustomerQuoteId,
                        Description = x.Description,
                        Key = x.Key,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        TypeItem = x.TypeItem,
                        Unit = x.Unit
                    }).ToList()
                }).Where(x => x.Status.Equals(Constants.PendingG1)).ToList(),
            };
            return View(model);
        }


        #endregion


        public async Task<IActionResult> ValidateCustomerQuote(string id)
        {
            var data = await _customerQuoteService.GetCustomerQuoteByKeyAsync(id);
            var model = new CustomerQuoteDetailViewModel
            {
                LegalDocumentation = data.LegalDocumentation,
                TechnicalData = data.LegalDocumentation,
                NormativeData = data.NormativeData,
                QuoteType = data.QuoteType,
                Warranty = data.Warranty,
                DeliveryTime = data.DeliveryTime,
                PaymentType = data.PaymentType,
                ManufacturingStandard = data.ManufacturingStandard,
                QualityProcess = data.QualityProcess,
                QuoteCode = data.QuoteCode,
                CurrencyName = data.Currency.CurrencyName,
                CustomerName = data.Customer.SocialName,
                QuoteKey = data.Key,
                Validity = data.Validity,
                VendorName = data.User.FullName,
                Status = data.Status,
                Lab = data.Lab,
                Items = data.CustomerQuoteDetails.Select(x => new ItemCustomerQuoteViewModel
                {
                    Availability = x.Availability,
                    Consecutive = x.Consecutive,
                    CustomerQuoteId = x.CustomerQuoteId,
                    Description = x.Description,
                    Key = x.Key,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TypeItem = x.TypeItem,
                    Unit = x.Unit,
                    Scope = x.Scope,
                    Remarks = x.Remarks
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendApproveQuote(ApproveCustomerQuoteViewModel model)
        {
            var quote = await _customerQuoteService.GetCustomerQuoteByKeyAsync(model.Id);

            if (quote.CustomerQuoteDetails.Count == 0)
            {
                return RedirectToAction("ValidateCustomerQuote", "GeneralDirection", new { id = quote.Key })
                    .WithDanger(Constants.ErrorMessage, "No se puede aprobar una cotizacion sin items");
            }

            quote.Status = Constants.AproveG1;

            var response = await _customerQuoteService.UpdateCustomerQuoteAsync(quote);

            #region SendEmail

            UserCore director;
            director = await _userCoreService.GetUserGeneralDirectionAsync("kingdavid_5@hotmail.com");

            var link = Url.Action("Index", "Home", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            string[] labels =
            {
                "[#SalesManager]", "[#QuoteCode]", "[#DirectorName]", "[#QuoteCode]",
                "[#CustomerName]", "[#Link]"
            };
            string[] data =
            {
                quote.User.FullName, quote.QuoteCode, director.FullName,quote.QuoteCode,
                quote.Customer.SocialName, link
            };
            string[] to = { quote.User.Email };
            string[] cc = { director.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/ApproveCustomerQuote.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Aprobación de cotización folio: {quote.QuoteCode}", view);

            #endregion

            return !response ? StatusCode(StatusCodes.Status400BadRequest) : Ok();
        }

        public async Task<IActionResult> EditItemQuote(string id)
        {
            var item = await _customerQuoteDetailService.GetDetailByKeyAsync(id);
            var model = new EditCustomerQuoteItemViewModel
            {
                TypeItem = item.TypeItem,
                Unit = item.Unit,
                UnitPrice = item.UnitPrice,
                Availability = item.Availability,
                Description = item.Description,
                Quantity = item.Quantity,
                QuoteKey = item.CustomerQuote.Key,
                ItemKey = item.Key
            };
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditItemQuote(EditCustomerQuoteItemViewModel model)
        {
            var item = await _customerQuoteDetailService.GetDetailByKeyAsync(model.ItemKey);

            item.Availability = model.Availability;
            item.Description = model.Description;
            item.Quantity = model.Quantity;
            item.TypeItem = model.TypeItem;
            item.Unit = model.Unit;
            item.UnitPrice = model.UnitPrice;

            var update = await _customerQuoteDetailService.UpdateDetailAsync(item);

            if (!update)
            {
                return RedirectToAction("ValidateCustomerQuote", "GeneralDirection", new { id = model.QuoteKey })
                    .WithDanger(Constants.ErrorMessage, "No fue posible editar el elemento, intenta de nuevo.");
            }

            return RedirectToAction("ValidateCustomerQuote", "GeneralDirection", new { id = model.QuoteKey })
                .WithSuccess(Constants.SuccessMessage, "Elemento actualizado correctamente.");
        }

        public async Task<IActionResult> DeleteItemQuote(string id)
        {
            var item = await _customerQuoteDetailService.GetDetailByKeyAsync(id);

            var response = await _customerQuoteDetailService.DeleteDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("ValidateCustomerQuote", "GeneralDirection", new { id = item.CustomerQuote.Key })
                    .WithDanger(Constants.ErrorMessage,
                        "Ocurrio un error, intenta de nuevo.");
            }

            return RedirectToAction("ValidateCustomerQuote", "GeneralDirection", new { id = item.CustomerQuote.Key })
                .WithSuccess(Constants.SuccessMessage,
                    "Item eliminado correctamente.");
        }

        [HttpPost]
        public async Task<IActionResult> CancelQuote(ApproveCustomerQuoteViewModel model)
        {
            var quote = await _customerQuoteService.GetCustomerQuoteByKeyAsync(model.Id);

            quote.Status = Constants.RejectG1;

            var response = await _customerQuoteService.UpdateCustomerQuoteAsync(quote);

            #region SendEmail

            UserCore director;
            director = await _userCoreService.GetUserGeneralDirectionAsync("kingdavid_5@hotmail.com");

            var link = Url.Action("Index", "Home", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            string[] labels =
            {
                "[#SalesManager]", "[#QuoteCode]", "[#DirectorName]", "[#QuoteCode]",
                "[#CustomerName]","[#Comments]", "[#Link]"
            };
            string[] data =
            {
                quote.User.FullName, quote.QuoteCode, director.FullName,quote.QuoteCode,
                quote.Customer.SocialName,model.Comments, link
            };
            string[] to = { quote.User.Email };
            string[] cc = { director.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/CancelCustomerQuote.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Cancelación de cotización folio: {quote.QuoteCode}", view);

            #endregion

            return !response ? StatusCode(StatusCodes.Status400BadRequest) : Ok();
        }

        public async Task<IActionResult> CustomerQuotesListApprove()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var data = await _customerQuoteService.GetAllCustomerQuotesAsync();
            var model = new CustomerQuotesViewModel
            {
                Quotes = data.Select(x => new CustomerQuotesViewModel
                {
                    Status = x.Status,
                    PaymentType = x.User.FullName,
                    QuoteCode = x.QuoteCode,
                    QuoteType = x.QuoteType,
                    DeliveryTime = x.DeliveryTime,
                    CreationDate = x.CreationDate.ToShortDateString(),
                    CurrencyName = x.Currency.CurrencyName,
                    QuoteKey = x.Key,
                    CustomerName = x.Customer.SocialName,
                    Details = x.CustomerQuoteDetails.Select(x => new ItemCustomerQuoteViewModel
                    {
                        Availability = x.Availability,
                        Consecutive = x.Consecutive,
                        CustomerQuoteId = x.CustomerQuoteId,
                        Description = x.Description,
                        Key = x.Key,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        TypeItem = x.TypeItem,
                        Unit = x.Unit
                    }).ToList()
                }).Where(x => x.Status.Equals(Constants.AproveG1)).ToList(),
            };
            return View(model);
        }

        public async Task<IActionResult> DetailCustomerQuote(string id)
        {
            var data = await _customerQuoteService.GetCustomerQuoteByKeyAsync(id);
            var model = new CustomerQuoteDetailViewModel
            {
                LegalDocumentation = data.LegalDocumentation,
                TechnicalData = data.LegalDocumentation,
                NormativeData = data.NormativeData,
                QuoteType = data.QuoteType,
                Warranty = data.Warranty,
                DeliveryTime = data.DeliveryTime,
                PaymentType = data.PaymentType,
                ManufacturingStandard = data.ManufacturingStandard,
                QualityProcess = data.QualityProcess,
                QuoteCode = data.QuoteCode,
                CurrencyName = data.Currency.CurrencyName,
                CustomerName = data.Customer.SocialName,
                QuoteKey = data.Key,
                Validity = data.Validity,
                VendorName = data.User.FullName,
                Status = data.Status,
                Lab = data.Lab,
                Items = data.CustomerQuoteDetails.Select(x => new ItemCustomerQuoteViewModel
                {
                    Availability = x.Availability,
                    Consecutive = x.Consecutive,
                    CustomerQuoteId = x.CustomerQuoteId,
                    Description = x.Description,
                    Key = x.Key,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TypeItem = x.TypeItem,
                    Unit = x.Unit,
                    Scope = x.Scope,
                    Remarks = x.Remarks
                }).ToList()
            };
            return View(model);
        }
    }
}
