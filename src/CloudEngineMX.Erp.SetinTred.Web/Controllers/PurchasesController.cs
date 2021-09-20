using System;
using System.IO;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Web.Extensions.Alerts;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Constants = CloudEngineMX.Erp.SetinTred.Web.Data.Constants;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Interfaces;
    using Data.ViewModels;
    using Infrastructure.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Authorize]
    public class PurchasesController : Controller
    {
        private readonly IUserCoreService _coreService;
        private readonly IAreaService _areaService;
        private readonly IOperatingBaseService _operatingBaseService;
        private readonly IRequisitionService _requisitionService;
        private readonly IRequisitionDetailService _requisitionDetailService;
        private readonly IUserService _userService;
        private readonly ISpecificationService _specificationService;
        private readonly ISpecificationTypeService _specificationTypeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMailService _mailService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPurchaseOrderDetailService _purchaseOrderDetailService;
        private readonly ISupplierService _supplierService;
        private readonly IQuotationService _quotationService;
        private readonly IPreQuotationService _preQuotationService;
        private readonly ICurrencyService _currencyService;
        private readonly ICostCenterService _costCenterService;

        public PurchasesController(
            IUserCoreService coreService,
            IAreaService areaService,
            IOperatingBaseService operatingBaseService,
            IRequisitionService requisitionService,
            IRequisitionDetailService requisitionDetailService,
            IUserService userService,
            ISpecificationService specificationService,
            ISpecificationTypeService specificationTypeService,
            IMapper mapper,
            ILogger logger,
            IWebHostEnvironment hostEnvironment,
            IMailService mailService,
            IPurchaseOrderService purchaseOrderService,
            IPurchaseOrderDetailService purchaseOrderDetailService,
            ISupplierService supplierService,
            IQuotationService quotationService,
            IPreQuotationService preQuotationService,
            ICurrencyService currencyService,
            ICostCenterService costCenterService)
        {
            _coreService = coreService;
            _areaService = areaService;
            _operatingBaseService = operatingBaseService;
            _requisitionService = requisitionService;
            _requisitionDetailService = requisitionDetailService;
            _userService = userService;
            _specificationService = specificationService;
            _specificationTypeService = specificationTypeService;
            _mapper = mapper;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _mailService = mailService;
            _purchaseOrderService = purchaseOrderService;
            _purchaseOrderDetailService = purchaseOrderDetailService;
            _supplierService = supplierService;
            _quotationService = quotationService;
            _preQuotationService = preQuotationService;
            _currencyService = currencyService;
            _costCenterService = costCenterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RequisitionList()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var requisitions = await _requisitionService.GetAllRequisitionsAsync();
            var model = new RequisitionsListViewModel();

            if (user.UserCore.Area.AreaName == "Compras")
            {
                var lstStatus = new List<string>() { "Requisición Creada", "Requiere de Compras", "Existencia Parcial" };

                model.Action = "Crear";
                model.CreationDate = DateTime.Now.ToShortDateString();
                model.Areas = await _areaService.GetComboAreasAsync();
                model.CostCenters = await _costCenterService.GetCostCenterByComboAsync();
                model.OperatingBases = await _operatingBaseService.GetOperatingBaseComboAsync();
                model.Classifications = _requisitionService.GetAllClassificationCombo();
                model.Requisitions = _mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions.Where(x => lstStatus.Contains(x.Status)));
                model.RequisitionsForApprove = _mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions.Where(x =>
                    x.Status.Equals(Constants.FirstApprover) && x.UserCore.Report.Equals(user.UserCore)));

                return View(model);
            }

            model.Action = "Crear";
            model.CreationDate = DateTime.Now.ToShortDateString();
            model.Areas = await _areaService.GetComboAreasAsync();
            model.CostCenters = await _costCenterService.GetCostCenterByComboAsync();
            model.OperatingBases = await _operatingBaseService.GetOperatingBaseComboAsync();
            model.Classifications = _requisitionService.GetAllClassificationCombo();
            model.Requisitions =
                _mapper.Map<IEnumerable<RequisitionViewModel>>(
                    requisitions.Where(x => x.UserCore.Equals(user.UserCore)));
            model.RequisitionsForApprove = _mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions.Where(x =>
                x.Status.Equals(Constants.FirstApprover) && x.UserCore.Report.Equals(user.UserCore)));

            return View(model);
        }

        public async Task<IActionResult> CreateRequisition(RequisitionsListViewModel model)
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var totalRequisitions = await _requisitionService.GetCountAllRequisitionsAsync() + 1;

            if (!ModelState.IsValid)
            {
                var requisitions = await _requisitionService.GetAllRequisitionsAsync();

                if (user.UserCore.Area.AreaName == "Compras")
                {
                    model.Action = "Crear";
                    model.CreationDate = DateTime.Now.ToShortDateString();
                    model.Areas = await _areaService.GetComboAreasAsync();
                    model.CostCenters = await _costCenterService.GetCostCenterByComboAsync();
                    model.OperatingBases = await _operatingBaseService.GetOperatingBaseComboAsync();
                    model.Classifications = _requisitionService.GetAllClassificationCombo();
                    model.Requisitions = _mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions);

                    return View("RequisitionList", model);
                }

                model.Action = "Crear";
                model.CreationDate = DateTime.Now.ToShortDateString();
                model.Areas = await _areaService.GetComboAreasAsync();
                model.CostCenters = await _costCenterService.GetCostCenterByComboAsync();
                model.OperatingBases = await _operatingBaseService.GetOperatingBaseComboAsync();
                model.Classifications = _requisitionService.GetAllClassificationCombo();
                model.Requisitions =
                    _mapper.Map<IEnumerable<RequisitionViewModel>>(
                        requisitions.Where(x => x.UserCore.Equals(user.UserCore)));

                return View("RequisitionList", model);

            }

            if (model.Action == "Editar")
            {

            }

            var newRequisition = new Requisition
            {
                Key = Guid.NewGuid().ToString(),
                OperatingBase = await _operatingBaseService.GetOperatingBaseByKeyAsync(model.OperatingBaseKey),
                CreationDate = DateTime.Now,
                UserCore = user.UserCore,
                Classification = model.Classification,
                Application = model.Application,
                CostCenter = model.CostCenter,
                Area = await _areaService.GetAreaByKeyAsync(model.AreaKey),
                RequisitionCode = $"ST-RI-{totalRequisitions.ToString().PadLeft(5, '0')}",
                Status = Constants.InitialRequisition,
                KeyDescription = model.KeyDescription
            };

            var createRequisition = await _requisitionService.AddRequisitionAsync(requisition: newRequisition);

            if (!createRequisition)
            {
                return RedirectToAction("RequisitionList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "No fue posible crear la requisición");
            }

            return RedirectToAction("ConfigureRequisition", "Purchases", new { id = newRequisition.Key }).WithSuccess(
                Constants.SuccessMessage, $"Se creo correctamente la requisición #{newRequisition.RequisitionCode}");
        }

        public async Task<IActionResult> ConfigureRequisition(string id)
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
                Items = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(items)
            };

            return View(model);
        }

        public async Task<IActionResult> CreateItem(string id)
        {
            var model = new ItemViewModel
            {
                RequisitionKey = id,
                Specifications = await _specificationService.GetAllSpecificationComboAsync()
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(ItemViewModel model)
        {
            var specification = await _specificationService.GetSpecificationByKeyAsync(model.SpecificationKey);
            var requisition = await _requisitionService.GetRequisitionByKeyAsync(model.RequisitionKey);
            var total = await _requisitionDetailService.GetCountDetailsByRequisitionAsync(requisition) + 1;

            var newDetail = new RequisitionDetail
            {
                Key = Guid.NewGuid().ToString(),
                Requisition = await _requisitionService.GetRequisitionByKeyAsync(model.RequisitionKey),
                CreationDate = DateTime.Now,
                Quantity = model.Quantity,
                Description = model.Description,
                MeasurementUnit = model.Measure,
                Specification = specification == null ? "N/A" : specification.SpecificationName,
                Review = specification == null ? "N/A" : specification.Revision.ToString(),
                Consecutive = total,
                IsApproved = true,
                IsUrgent = model.IsUrgent
            };

            var response = await _requisitionDetailService.AddRequisitionDetailAsync(newDetail);

            if (!response)
            {
                return RedirectToAction("ConfigureRequisition", "Purchases", new
                {
                    id = model.RequisitionKey
                })
                    .WithDanger(Constants.ErrorMessage, "No se pudo agregar el material");
            }

            return RedirectToAction("ConfigureRequisition", "Purchases", new { id = model.RequisitionKey })
                .WithSuccess(Constants.SuccessMessage, "Material agregado correctamente");
        }

        public async Task<IActionResult> DetailRequisition(string id)
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
                Items = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(items)
            };

            return View(model);
        }

        public async Task<IActionResult> ViewComments(string id)
        {
            var detail = await _requisitionDetailService.GetRequisitionDetailByKeyAsync(id);

            var model = new RequisitionDetailItemViewModel
            {
                ReasonForRejection = detail.ReasonForRejection
            };

            return PartialView(model);
        }
        public async Task<IActionResult> GetApprover(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("ConfigureRequisition", "Purchases", new { id = id })
                    .WithDanger(Constants.ErrorMessage, "No fue posible enviar la requisición, intenta de nuevo.");
            }

            var requisition = await _requisitionService.GetRequisitionByKeyAsync(id);

            var user = await _coreService.GetUserCoreByKeyAsync(requisition.UserCore.Key);

            var userWarehouse = await _userService.GetUserByEmailAsync("almacen01@setintred.com");

            requisition.Status = Constants.FirstApprover;

            var update = await _requisitionService.UpdateRequisitionAsync(requisition);

            if (!update)
            {
                _logger.Error($"Ocurrió un error al actualizar la requisición: {requisition.RequisitionCode}");
                return RedirectToAction("ConfigureRequisition", "Purchases", new { id = id })
                    .WithDanger(Constants.ErrorMessage, "No fue posible enviar la requisición, intenta de nuevo.");
            }


            var link = Url.Action("FillOrder", "Warehouse", new { id = requisition.Key },
                protocol: HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            var materials = await _requisitionDetailService.GetAllRequisitionDetailAsync(requisition);
            var lst = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(materials);
            var lstToMail = (from p in lst select p.ToMail).ToList();
            string[] labels =
            {
                "[#UserName]", "[#Applicant]", "[#RequisitionCode]", "[#Area]", "[#OperatingBase]", "[#Classification]",
                "[#Application]", "[#Materials]","[#Link]"
            };
            string[] data =
            {
                userWarehouse.UserCore.FullName, user.FullName, requisition.RequisitionCode, requisition.Area.AreaName,
                requisition.OperatingBase.OperatingBaseName,
                requisition.Classification, requisition.Application, string.Join("<br />", lstToMail.ToArray()), link
            };
            string[] to = { userWarehouse.Email };
            string[] cc = { user.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/FirstApprover.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Validar Existencia: {requisition.RequisitionCode}", view);


            return RedirectToAction("RequisitionList", "Purchases")
                .WithSuccess(Constants.SuccessMessage, "Requisición enviada correctamente");
        }

        public async Task<IActionResult> SendToWareHouse(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("ConfigureRequisition", "Purchases", new { id = id })
                    .WithDanger(Constants.ErrorMessage, "No fue posible enviar la requisición, intenta de nuevo.");
            }

            var activeUser = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var requisition = await _requisitionService.GetRequisitionByKeyAsync(id);

            var user = await _coreService.GetUserCoreByKeyAsync(requisition.UserCore.Key);

            requisition.Status = Constants.SecondApprover;

            var update = await _requisitionService.UpdateRequisitionAsync(requisition);

            if (!update)
            {
                _logger.Error($"Ocurrió un error al actualizar la requisición: {requisition.RequisitionCode}");
                return RedirectToAction("ConfigureRequisition", "Purchases", new { id = id })
                    .WithDanger(Constants.ErrorMessage, "No fue posible enviar la requisición, intenta de nuevo.");
            }

            _logger.Information(
                $"La requisición {requisition.RequisitionCode} fue aprobada por el usuario {activeUser.UserCore.FullName}");

            var link = Url.Action("DetailRequisition", "Purchases", new { id = requisition.Key },
                protocol: HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            var materials = await _requisitionDetailService.GetAllRequisitionDetailAsync(requisition);
            var lst = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(materials);
            var lstToMail = (from p in lst where p.IsApproved select p.ToMail).ToList();
            string[] labels =
            {
                "[#UserName]", "[#RequisitionCode]","[#Approver]", "[#Area]", "[#OperatingBase]", "[#Classification]",
                "[#Application]", "[#Materials]","[#Link]"
            };
            string[] data =
            {
                requisition.UserCore.FullName, requisition.RequisitionCode, activeUser.UserCore.FullName, requisition.Area.AreaName,
                requisition.OperatingBase.OperatingBaseName,
                requisition.Classification, requisition.Application, string.Join("<br />", lstToMail.ToArray()), link
            };
            string[] to = { requisition.UserCore.Email };
            string[] cc = { "almacen@gmail.com" };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/SecondApprover.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Requisición Aprobada: {requisition.RequisitionCode}", view);


            return RedirectToAction("RequisitionList", "Purchases")
                .WithSuccess(Constants.SuccessMessage, "Requisición enviada correctamente");
        }

        public async Task<IActionResult> DeleteItemToRequest(string id)
        {
            var item = await _requisitionDetailService.GetRequisitionDetailByKeyAsync(id);

            var response = await _requisitionDetailService.DeleteItemToRequestByKeyAsync(item);

            if (!response)
            {
                return RedirectToAction("ConfigureRequisition", "Purchases", new { id = item.Requisition.Key })
                    .WithDanger(Constants.ErrorMessage, "No fue posible eliminar el item.");
            }

            return RedirectToAction("ConfigureRequisition", "Purchases", new { id = item.Requisition.Key })
                .WithSuccess(Constants.SuccessMessage, "Item eliminado correctamente");
        }

        public async Task<IActionResult> DetailToAprobe(string id)
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
                Items = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(items)
            };

            return View(model);
        }

        public async Task<IActionResult> RejectItem(string id)
        {
            var item = await _requisitionDetailService.GetRequisitionDetailByKeyAsync(id);

            item.IsApproved = false;

            var response = await _requisitionDetailService.UpdateRequisitionDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("DetailToAprobe", "Purchases", new { id = item.Requisition.Key })
                    .WithDanger(Constants.ErrorMessage, "No fue posible rechazar el item.");
            }

            return RedirectToAction("DetailToAprobe", "Purchases", new { id = item.Requisition.Key })
                .WithSuccess(Constants.SuccessMessage, "Item rechazado correctamente");
        }

        public async Task<IActionResult> GetApproveToPurchase(string id, string control)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("RequisitionList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "No fue posible enviar la requisición, intenta de nuevo.");
            }

            var requisition = await _requisitionService.GetRequisitionByKeyAsync(id);

            var user = await _coreService.GetUserCoreByKeyAsync(requisition.UserCore.Key);

            UserCore director;

            if (control == "1")
            {
                director = await _coreService.GetUserGeneralDirectionAsync("kingdavid_5@hotmail.com");
            }
            else
            {
                director = await _coreService.GetUserGeneralDirectionAsync("gdositaly@icloud.com");
            }

            requisition.Status = Constants.RequestToAuthorizePurchase;

            var update = await _requisitionService.UpdateRequisitionAsync(requisition);

            if (!update)
            {
                _logger.Error($"Ocurrió un error al actualizar la requisición: {requisition.RequisitionCode}");
                return RedirectToAction("RequisitionList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "No fue posible enviar la requisición, intenta de nuevo.");
            }

            _logger.Information(
                $"Se envió al usuario: {user.Report.FullName}, para aprobar la requisición de compra: {requisition.RequisitionCode}");

            var link = Url.Action("DetailRequisition", "Purchases", new { id = requisition.Key },
                protocol: HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            var materials = await _requisitionDetailService.GetAllRequisitionDetailAsync(requisition);
            var lst = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(materials);
            var lstToMail = (from p in lst where p.IsApproved && !p.IsDispatched select p.ToMailPurchase).ToList();
            string[] labels =
            {
                "[#DirectorName]","[#RequisitionCode]","[#RequisitionCode]", "[#Owner]",  "[#Area]","[#Application]", "[#Materials]","[#Link]"
            };
            string[] data =
            {
                director.FullName, requisition.RequisitionCode, requisition.RequisitionCode, requisition.UserCore.FullName,
                requisition.UserCore.Area.AreaName, requisition.Application, string.Join("<br />", lstToMail.ToArray()), link
            };
            string[] to = { director.Email };
            string[] cc = { user.Email, user.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/ApprovePurchase.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Aprobar Requisición de Compra de Materiales: {requisition.RequisitionCode}", view);


            return RedirectToAction("RequisitionList", "Purchases")
                .WithSuccess(Constants.SuccessMessage, "Requisición de compra enviada correctamente");
        }

        public async Task<IActionResult> PurchaseOrderList()
        {
            var requisitions = await _requisitionService.GetAllRequisitionToPurchaseAsync();

            var orders = await _purchaseOrderService.GetAllPurchaseOrderAsync();

            var model = new PurchaseOrdersViewModel
            {
                Orders = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(orders.Where(x => x.Status.Equals("Creada"))),
                Requisitions =
                    _mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions)
            };
            return View(model);
        }

        public async Task<IActionResult> CreatePurchaseOrder()
        {
            var model = new NewPurchaseOrderViewModel
            {
                Suppliers = await _supplierService.GetComboSuppliersAsync(),
                Requisitions = await _requisitionService.GetComboRequisitionsAsync(),
                Bases = await _operatingBaseService.GetComboBasesAsync(),
                Currencies = await _currencyService.GetCurrencyComboAsync()
            };
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(NewPurchaseOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PurchaseOrderList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error, intenta de nuevo");
            }

            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var total = await _purchaseOrderService.GetTotalOrdersAsync();
            var requisition = await _requisitionService.GetRequisitionByCodeAsync(model.RequisitionCode);
            var sendTo = await _operatingBaseService.GetOperatingBaseByKeyAsync(model.SendTo);
            var quotations = await _preQuotationService.GetAllPreQuotationsByPurchaseOrderAsync(requisition);
            var currency = await _currencyService.GetCurrencyByKeyAsync(model.Currency);

            var newOrder = new PurchaseOrder
            {
                UserCore = user.UserCore,
                Status = Constants.PurchaseOrderCreate,
                RequisitionCode = model.RequisitionCode,
                OperatingBase = sendTo,
                DeliveryTime = model.DeliveryTime,
                Applicant = requisition.UserCore.FullName,
                CreationDate = DateTime.Now,
                SendTo = sendTo.OperatingBaseName,
                Condition = model.Condition,
                Key = Guid.NewGuid().ToString(),
                Remarks = model.Remarks,
                Supplier = await _supplierService.GetSupplierByKeyAsync(model.SupplierName),
                Currency = currency,
                RequiresVat = model.VatRequired,
                PurchaseOrderCode = $"IC-STR-{total.ToString().PadLeft(5, '0')}",
                KeyDescription = requisition.KeyDescription
            };

            var response = await _purchaseOrderService.AddPurchaseOrderAsync(newOrder);

            if (!response)
            {
                return RedirectToAction("PurchaseOrderList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error, intenta de nuevo");
            }

            if (requisition.PreQuotations != null)
            {
                var lst = requisition.PreQuotations.Select(x => new Quotation
                {
                    CreationDate = x.CreationDate,
                    FileName = x.FileName,
                    PurchaseOrder = newOrder,
                    Key = Guid.NewGuid().ToString(),
                    RouteFile = x.RouteFile
                }).ToList();

                var addList = await _quotationService.AddAllPreQuotationsAsync(lst);
            }

            return RedirectToAction("ConfigureOrder", "Purchases", new { id = newOrder.Key })
                .WithSuccess(Constants.SuccessMessage,
                    "Instrucción de compra creada correctamente, no olvides configurar los items");
        }

        public async Task<IActionResult> ConfigureOrder(string id)
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

        public async Task<IActionResult> CreateItemToPurchase(string id)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(id);
            var requisition = await _requisitionService.GetRequisitionByCodeAsync(order.RequisitionCode);


            var model = new PurchaseOrderItemViewModel
            {
                Items = await _requisitionDetailService.GetItemsToPurchaseOrderComboAsync(requisition),
                PurchaseOrderKey = id,
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItemToPurchase(PurchaseOrderItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = model.PurchaseOrderKey })
                    .WithSuccess(Constants.ErrorMessage,
                        "No se pudo agregar el material, intenta de nuevo.");
            }

            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(model.PurchaseOrderKey);

            var detail = await _requisitionDetailService.GetRequisitionDetailByKeyAsync(model.Description);

            detail.QuantityToBuy -= model.Quantity;

            var item = new PurchaseOrderDetail
            {
                IsApproved = true,
                Key = Guid.NewGuid().ToString(),
                Quantity = model.Quantity,
                CreationDate = DateTime.Now,
                Description = detail.Description,
                PartNumber = model.PartNumber,
                PurchaseOrder = order,
                Consecutive = await _purchaseOrderDetailService.GetTotalItemsCountAsync(order),
                Unit = detail.MeasurementUnit,
                UnitPrice = model.UnitPrice,
                IsUrgent = detail.IsUrgent
            };

            var update = await _requisitionDetailService.UpdateRequisitionDetailAsync(detail);

            var response = await _purchaseOrderDetailService.AddPurchaseOrderDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = model.PurchaseOrderKey })
                    .WithSuccess(Constants.ErrorMessage,
                        "No se pudo agregar el material, intenta de nuevo.");
            }

            return RedirectToAction("ConfigureOrder", "Purchases", new { id = model.PurchaseOrderKey })
                .WithSuccess(Constants.SuccessMessage,
                    "Material agregado correctamente.");
        }

        public async Task<IActionResult> DeleteItemPurchaseOrder(string id)
        {
            var item = await _purchaseOrderDetailService.GetDetailByKeyAsync(id);

            var initialRequisition = await _requisitionService.GetRequisitionByCodeAsync(item.PurchaseOrder.RequisitionCode);

            var itemToReload =
                await _requisitionDetailService.GetItemByDescriptionAndRequisition(item.Description,
                    initialRequisition.Id);
            itemToReload.QuantityToBuy = item.Quantity;

            var response = await _purchaseOrderDetailService.DeleteItemAsync(item);

            if (!response)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = item.PurchaseOrder.Key })
                    .WithSuccess(Constants.ErrorMessage,
                        "No se pudo eliminar el material, intenta de nuevo.");
            }

            var reloadItem = await _requisitionDetailService.UpdateRequisitionDetailAsync(itemToReload);

            if (!reloadItem)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = item.PurchaseOrder.Key })
                    .WithSuccess(Constants.ErrorMessage,
                        $"Ocurrió un error al regresar el item a la Requisición {initialRequisition.RequisitionCode}, favor de contactar a soporte para asistencia.");
            }

            return RedirectToAction("ConfigureOrder", "Purchases", new { id = item.PurchaseOrder.Key })
                .WithSuccess(Constants.SuccessMessage,
                    "Material eliminado correctamente.");
        }

        public async Task<IActionResult> EditItem(string id)
        {
            var item = await _purchaseOrderDetailService.GetDetailByKeyAsync(id);

            var model = new PurchaseOrderItemViewModel
            {
                Quantity = item.Quantity,
                Description = item.Description,
                PartNumber = item.PartNumber,
                Unit = item.Unit,
                UnitPrice = item.UnitPrice,
                ItemKey = id,
            };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(PurchaseOrderItemViewModel model)
        {
            var item = await _purchaseOrderDetailService.GetDetailByKeyAsync(model.ItemKey);

            item.Quantity = model.Quantity;
            item.UnitPrice = model.UnitPrice;

            var response = await _purchaseOrderDetailService.UpdatePurchaseOrderDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = item.PurchaseOrder.Key })
                    .WithSuccess(Constants.ErrorMessage,
                        "No se pudo editar el material, intenta de nuevo.");
            }

            return RedirectToAction("ConfigureOrder", "Purchases", new { id = item.PurchaseOrder.Key })
                .WithSuccess(Constants.SuccessMessage,
                    "Material editado correctamente.");
        }


        public async Task<IActionResult> GetApproverPurchaseOrder(string id)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(id);

            if (order.PurchaseOrderDetails.Count == 0)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = id })
                    .WithSuccess(Constants.ErrorMessage,
                        "Necesitas tener por lo menos un elemento para solicitar aprobación.");
            }

            order.Status = Constants.PurchaseOrderApprove;

            var response = await _purchaseOrderService.UpdatePurchaseOrderAsync(order);

            if (!response)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = id })
                    .WithSuccess(Constants.ErrorMessage,
                        "Ocurrió un error al solicitar la aprobación.");
            }

            var requisition = await _requisitionService.GetRequisitionByCodeAsync(order.RequisitionCode);
            var director = await _coreService.GetUserGeneralDirectionAsync("gdositaly@icloud.com");
            var link = Url.Action("PurchaseOrderList", "GeneralDirection", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            var materials = await _purchaseOrderDetailService.GetAllItemsByOrderAsync(order);
            var lst = _mapper.Map<IEnumerable<PurchaseOrderDetailViewModel>>(materials);
            var lstToMail = (from p in lst where p.IsApproved select p.ToMail).ToList();
            string[] labels =
            {
                "[#DirectorName]", "[#PurcharseOrderCode]", "[#RequisitionCode]", "[#Owner]", "[#SupplierName]",
                "[#Materials]", "[#Link]"
            };
            string[] data =
            {
                director.FullName, order.PurchaseOrderCode, requisition.RequisitionCode, requisition.UserCore.FullName,
                order.Supplier.FiscalName, string.Join("<br />", lstToMail.ToArray()), link
            };
            string[] to = { director.Email };
            string[] cc = { requisition.UserCore.Email, order.UserCore.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/ApprovePurchaseOrder.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Aprobar Instrucción de Compra: {order.PurchaseOrderCode}", view);

            return RedirectToAction("PurchaseOrderList", "Purchases")
                .WithSuccess(Constants.SuccessMessage,
                    "Instrucción de compra enviada a aprobación correctamente.");
        }

        public async Task<IActionResult> PurchaseOrderDetail(string id)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(id);

            var model = _mapper.Map<PurchaseOrderViewModel>(order);
            model.Details = _mapper.Map<IEnumerable<PurchaseOrderDetailViewModel>>(order.PurchaseOrderDetails);
            return View(model);
        }

        public IActionResult AddQuotationToPurchase(string id)
        {
            var model = new PurchaseOrderQuotationViewModel
            {
                PurchaseOrderKey = id
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuotationToPurchase(PurchaseOrderQuotationViewModel model)
        {
            if (model.QuotationFile == null || model.QuotationFile.Length <= 0)
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = model.PurchaseOrderKey })
                    .WithDanger("Operación Incorrecta", "Debes agregar una cotización");

            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}{Path.GetExtension(model.QuotationFile.FileName)}";
            var fileName = model.QuotationFile.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\quotations", file);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.QuotationFile.CopyToAsync(stream);
            }

            path = $"~/files/quotations/{file}";

            var quotation = new Quotation
            {
                Key = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                FileName = fileName,
                RouteFile = path,
                PurchaseOrder = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(model.PurchaseOrderKey)
            };

            var response = await _quotationService.AddQuotationAsync(quotation);

            if (!response)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = model.PurchaseOrderKey })
                    .WithDanger("Operación Incorrecta", "No se pudo agregar la cotización");
            }

            return RedirectToAction("ConfigureOrder", "Purchases", new { id = model.PurchaseOrderKey })
                .WithSuccess("Operación Correcta", "Cotización Agregada Correctamente");
        }

        public async Task<IActionResult> DeleteDocument(string id, string order)
        {
            var quote = await _quotationService.GetQuotationByKeyAsync(id);

            var response = await _quotationService.DeleteQuotationAsync(quote);

            if (!response)
            {
                return RedirectToAction("ConfigureOrder", "Purchases", new { id = order })
                    .WithDanger("Operación Incorrecta", "No se pudo eliminar la cotización");
            }


            return RedirectToAction("ConfigureOrder", "Purchases", new { id = order })
                .WithSuccess("Operación Correcta", "Cotización eliminada Correctamente");
        }

        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> DownloadOrder(string id)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(id);

            var model = _mapper.Map<PurchaseOrderViewModel>(order);
            model.Details =
                _mapper.Map<IEnumerable<PurchaseOrderDetailViewModel>>(
                    order.PurchaseOrderDetails.Where(x => x.IsApproved));

            if (model.Status == Constants.PurchaseOrderComplete && model.WithApprovedDetails)
            {
                try
                {
                    _logger.Information($"Imprimiendo instrucción de Compra:{order.PurchaseOrderCode}");
                    HttpContext.JsReportFeature().DebugLogsToResponse().Recipe(Recipe.ChromePdf)
                        .OnAfterRender((r) => HttpContext.Response.Headers["Content-Disposition"] = $"attachment; filename=\"{order.PurchaseOrderCode}.pdf\"");

                    return View(model);
                }
                catch (Exception e)
                {
                    _logger.Error($"Ocurrió un error al imprimir la instrucción de compra: {e.Message}");
                }

            }

            return RedirectToAction("PurchaseOrderList", "Purchases").WithDanger(Constants.ErrorMessage,
                "La instrucción no está aprobada o no tiene ningún material aprobado");
        }

        public async Task<IActionResult> ConfigureToApprobation(string id)
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

        public IActionResult AddQuotationToRequisition(string id)
        {
            var model = new RequisitionQuotationViewModel
            {
                RequisitionKey = id
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuotationToRequisition(RequisitionQuotationViewModel model)
        {
            if (model.QuotationFile == null || model.QuotationFile.Length <= 0)
                return RedirectToAction("ConfigureToApprobation", "Purchases", new { id = model.RequisitionKey })
                    .WithDanger("Operación Incorrecta", "Debes agregar una cotización");

            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}{Path.GetExtension(model.QuotationFile.FileName)}";
            var fileName = model.QuotationFile.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\quotations", file);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.QuotationFile.CopyToAsync(stream);
            }

            path = $"~/files/quotations/{file}";

            var quotation = new PreQuotation
            {
                Key = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                FileName = fileName,
                RouteFile = path,
                Requisition = await _requisitionService.GetRequisitionByKeyAsync(model.RequisitionKey)
            };

            var response = await _preQuotationService.AddPreQuotationAsync(quotation);

            if (!response)
            {
                return RedirectToAction("ConfigureToApprobation", "Purchases", new { id = model.RequisitionKey })
                    .WithDanger("Operación Incorrecta", "No se pudo agregar la cotización");
            }

            return RedirectToAction("ConfigureToApprobation", "Purchases", new { id = model.RequisitionKey })
                .WithSuccess("Operación Correcta", "Cotización Agregada Correctamente");
        }

        public async Task<IActionResult> DeleteDocumentToRequisition(string id, string requisition)
        {
            var quote = await _preQuotationService.GetPreQuotationByKeyAsync(id);

            var response = await _preQuotationService.DeletePreQuotationAsync(quote);

            if (!response)
            {
                return RedirectToAction("ConfigureToApprobation", "Purchases", new { id = requisition })
                    .WithDanger("Operación Incorrecta", "No se pudo eliminar la cotización");
            }


            return RedirectToAction("ConfigureToApprobation", "Purchases", new { id = requisition })
                .WithSuccess("Operación Correcta", "Cotización eliminada Correctamente");
        }

        [HttpPost]
        public async Task<IActionResult> RejectPurchase(CancelOrderViewModel model)
        {
            var purchaseOrder = await _requisitionService.GetRequisitionByKeyAsync(model.Id);


            purchaseOrder.Status = Constants.RequestCancel;
            //purchaseOrder.= model.Comments;

            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var response = await _requisitionService.UpdateRequisitionAsync(purchaseOrder);


            var link = Url.Action("Index", "Home", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            string[] labels =
            {
                "[#PurchaseUser]", "[#PurcharseOrderCode]", "[#DirectionUser]", "[#Comments]", "[#Link]"
            };
            string[] data =
            {
                purchaseOrder.UserCore.FullName, purchaseOrder.RequisitionCode, user.UserCore.FullName,
                model.Comments, link
            };
            string[] to = { purchaseOrder.UserCore.Email };
            string[] cc = { purchaseOrder.UserCore.Report.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/CancelRequisition.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Requisición Cancelada Folio: {purchaseOrder.RequisitionCode}", view);

            return !response ? StatusCode(StatusCodes.Status400BadRequest) : Ok();
        }

        public async Task<IActionResult> RequisitionListPending()
        {
            var lstStatus = new List<string>() { "Aprobacion de Compra" };
            var requisitions = await _requisitionService.GetAllRequisitionsAsync();
            var model = new RequisitionsListViewModel
            {
                Requisitions = _mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions.Where(x => lstStatus.Contains(x.Status)))
            };
            return View(model);
        }

        public async Task<IActionResult> RequisitionListCancel()
        {
            var lstStatus = new List<string>() { "Cancelada", "Cancelada DG" };

            var requisitions = await _requisitionService.GetAllRequisitionsAsync();
            var model = new RequisitionsListViewModel
            {
                Requisitions = _mapper.Map<IEnumerable<RequisitionViewModel>>(requisitions.Where(x => lstStatus.Contains(x.Status)))
            };
            return View(model);
        }

        public async Task<IActionResult> ReactiveRequest(string id)
        {
            var request = await _requisitionService.GetRequisitionByKeyAsync(id);
            var detail = await _requisitionDetailService.GetAllRequisitionDetailAsync(request);

            foreach (var requisitionDetail in detail)
            {
                requisitionDetail.QuantityDispatched = 0;
                requisitionDetail.QuantityToBuy = 0;
                requisitionDetail.IsApproved = false;

                await _requisitionDetailService.UpdateRequisitionDetailAsync(requisitionDetail);
            }

            request.Status = Constants.FirstApprover;

            var response = await _requisitionService.UpdateRequisitionAsync(request);

            if (!response)
            {
                return RedirectToAction("RequisitionListCancel", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "No fue posible reactivar la requisición, intenta nuevamente");
            }

            return RedirectToAction("RequisitionListCancel", "Purchases")
                .WithSuccess(Constants.SuccessMessage,
                    "La requisición fue activada y enviada a almacén para validación.");
        }

        public async Task<IActionResult> PurchaseOrderListPending()
        {
            var orders = await _purchaseOrderService.GetAllPurchaseOrderAsync();

            var model = new PurchaseOrdersViewModel
            {
                Orders = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(orders.Where(x => x.Status.Equals("Espera Aprobacion")))
            };
            return View(model);
        }

        public async Task<IActionResult> PurchaseOrderListCancel()
        {
            var orders = await _purchaseOrderService.GetAllPurchaseOrderAsync();

            var model = new PurchaseOrdersViewModel
            {
                Orders = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(orders.Where(x => x.Status.Equals("Rechazada por D.G.")))
            };
            return View(model);
        }

        public async Task<IActionResult> PurchaseOrderListApprove()
        {
            var orders = await _purchaseOrderService.GetAllPurchaseOrderAsync();

            var model = new PurchaseOrdersViewModel
            {
                Orders = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(orders.Where(x => x.Status.Equals("Aprobada")))
            };
            return View(model);
        }


    }
}
