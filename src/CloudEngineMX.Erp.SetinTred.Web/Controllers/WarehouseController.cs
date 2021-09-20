using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces;
using CloudEngineMX.Erp.SetinTred.Web.Data;
using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using CloudEngineMX.Erp.SetinTred.Web.Extensions.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    [Authorize]
    public class WarehouseController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRequisitionService _requisitionService;
        private readonly IMapper _mapper;
        private readonly IRequisitionDetailService _requisitionDetailService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMailService _mailService;

        public WarehouseController(
            IUserService userService,
            IRequisitionService requisitionService,
            IMapper mapper,
            IRequisitionDetailService requisitionDetailService,
            ILogger logger,
            IWebHostEnvironment hostEnvironment,
            IMailService mailService
            )
        {
            _userService = userService;
            _requisitionService = requisitionService;
            _mapper = mapper;
            _requisitionDetailService = requisitionDetailService;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RequisitionListToSend()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            if (!user.UserCore.Area.AreaName.Equals("Almacen"))
            {
                return RedirectToAction("Index", "Warehouse")
                    .WithDanger(Constants.ErrorMessage, "NO tienes los permisos requeridos");
            }

            var requisitions = await _requisitionService.GetAllRequisitionsAsync();
            var model = new RequisitionsListViewModel
            {
                Requisitions = _mapper.Map<IEnumerable<RequisitionViewModel>>(
                    requisitions.Where(x => x.Status.Equals(Constants.FirstApprover)))
            };


            return View(model);
        }

        public async Task<IActionResult> DetailRequisition(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("RequisitionList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "La requisicion seleccionado no existe");
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

        public async Task<IActionResult> FillOrder(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("RequisitionList", "Purchases")
                    .WithDanger(Constants.ErrorMessage, "La requisicion seleccionado no existe");
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


        public async Task<IActionResult> AddQuantity(string id)
        {
            var item = await _requisitionDetailService.GetRequisitionDetailByKeyAsync(id);

            var model = new RequisitionDetailItemViewModel
            {
                DetailKey = id,
                Quantity = item.Quantity
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DispatchItem(RequisitionDetailItemViewModel model)
        {
            var item = await _requisitionDetailService.GetRequisitionDetailByKeyAsync(model.DetailKey);

            item.IsDispatched = model.Quantity != 0;
            if (model.Quantity == 0 || model.Quantity < item.Quantity)
            {
                item.IsApproved = true;
                item.QuantityToBuy = item.Quantity - model.Quantity;
                item.QuantityDispatched = model.Quantity;
            }
            else if (model.Quantity == item.Quantity)
            {
                item.QuantityToBuy = item.Quantity - model.Quantity;
                item.QuantityDispatched = model.Quantity;
                item.IsApproved = false;
            }


            var update = await _requisitionDetailService.UpdateRequisitionDetailAsync(item);

            if (!update)
            {

                return RedirectToAction("FillOrder", "Warehouse", new { id = item.Requisition.Key })
                    .WithWarning(Constants.ErrorMessage, "Ocurrio un error intenta de nuevo");
            }

            return RedirectToAction("FillOrder", "Warehouse", new { id = item.Requisition.Key })
                .WithSuccess(Constants.SuccessMessage, "Material Despachado Correctamente");
        }

        public async Task<IActionResult> FinishRequest(string id)
        {
            var item = await _requisitionService.GetRequisitionByKeyAsync(id);

            var filled = string.Empty;

            var totalItems = item.RequisitionDetails.Sum(x => x.Quantity);
            var dispatched = item.RequisitionDetails.Sum(x => x.QuantityDispatched);
            var partial = totalItems - dispatched;

            if (dispatched == totalItems)
            {
                item.Status = Constants.RequestFull;
            }
            else if (dispatched == 0)
            {
                item.Status = Constants.RequestToPurchases;
            }
            else if (dispatched < totalItems)
            {
                item.Status = Constants.RequestPartial;
            }

            var update = await _requisitionService.UpdateRequisitionAsync(item);

            if (!update)
            {
                return RedirectToAction("FillOrder", "Warehouse", new { id = item.Key })
                    .WithWarning(Constants.ErrorMessage, "Ocurrio un error intenta de nuevo");
            }


            var link = Url.Action("Index", "Home", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            var materials = await _requisitionDetailService.GetAllRequisitionDetailAsync(item);
            var lst = _mapper.Map<IEnumerable<RequisitionDetailItemViewModel>>(materials);
            var lstToMail = (from p in lst select p.ToMail).ToList();
            string[] labels =
            {
                "[#UserName]", "[#RequisitionCode]","[#Status]", "[#Area]", "[#OperatingBase]", "[#Classification]",
                "[#Application]", "[#Materials]","[#Link]"
            };
            string[] data =
            {
                item.UserCore.FullName, item.RequisitionCode, item.Status, item.Area.AreaName,
                item.OperatingBase.OperatingBaseName,
                item.Classification, item.Application, string.Join("<br />", lstToMail.ToArray()), link
            };
            string[] to = { item.UserCore.Email };
            string[] cc = { item.UserCore.Report.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/WarehouseFill.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, $"Requisición validada por almacen: {item.RequisitionCode}", view);

            return RedirectToAction("RequisitionListToSend", "Warehouse")
                .WithSuccess(Constants.SuccessMessage, "Solicitud finaliza correctamente");
        }
    }
}
