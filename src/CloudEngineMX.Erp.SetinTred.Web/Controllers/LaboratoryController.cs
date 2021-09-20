using System;
using System.Linq;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces;
using CloudEngineMX.Erp.SetinTred.Web.Data;
using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using CloudEngineMX.Erp.SetinTred.Web.Extensions.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    [Authorize]
    public class LaboratoryController : Controller
    {
        private readonly IMeasurementMaterialService _materialService;
        private readonly IMaterialInventoryService _inventoryService;
        private readonly IMaterialRequestService _materialRequestService;
        private readonly IMaterialRequestDetailService _materialRequestDetailService;
        private readonly IUserService _userService;

        public LaboratoryController(
            IMeasurementMaterialService materialService,
            IMaterialInventoryService inventoryService,
            IMaterialRequestService materialRequestService,
            IMaterialRequestDetailService materialRequestDetailService,
            IUserService userService)
        {
            _materialService = materialService;
            _inventoryService = inventoryService;
            _materialRequestService = materialRequestService;
            _materialRequestDetailService = materialRequestDetailService;
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MeasurementMaterials()
        {
            var materials = await _materialService.GetAllMaterialsAsync();

            var lst = materials.Select(x => new MaterialViewModel
            {
                IsActive = x.IsActive,
                ManufacturerName = x.Manufacturer.ManufacturerName,
                MaterialName = x.MaterialName,
                MaterialKey = x.Key,
                Inventories = x.MaterialInventories.Select(i => new MaterialInventoryViewModel
                {
                    CalibrationFrequency = i.CalibrationFrequency,
                    ActiveFrom = i.ActiveFrom,
                    Remarks = i.Remarks,
                    Serial = i.Serial,
                    Resolution = i.Resolution,
                    DateOfCalibration = i.DateOfCalibration,
                    DueDateCalibration = i.DueDateCalibration,
                    State = i.State,
                    Location = i.Location,
                    InactiveSince = i.InactiveSince,
                    CertificationNumber = i.CertificationNumber,
                    HeadOfVerification = i.HeadOfVerification,
                    Code = i.Code
                }).ToList()
            }).ToList();

            var model = new MeasurementMaterialViewModel
            {
                Materials = lst
            };

            return View(model);
        }

        public async Task<IActionResult> InventoryToMaterial(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("MeasurementMaterials", "Laboratory")
                    .WithDanger(Constants.ErrorMessage, "El material seleccionado no existe");
            }

            var material = await _materialService.GetMaterialByKeyAsync(id);

            var inventories = await _inventoryService.GetAllInventoriesByMaterialAsync(material);

            var lst = inventories.Select(i => new MaterialInventoryViewModel
            {
                CalibrationFrequency = i.CalibrationFrequency,
                ActiveFrom = i.ActiveFrom,
                Remarks = i.Remarks,
                Serial = i.Serial,
                Resolution = i.Resolution,
                DateOfCalibration = i.DateOfCalibration,
                DueDateCalibration = i.DueDateCalibration,
                State = i.State,
                Location = i.Location,
                InactiveSince = i.InactiveSince,
                CertificationNumber = i.CertificationNumber,
                HeadOfVerification = i.HeadOfVerification,
                Code = i.Code
            }).ToList();

            var model = new DetailInventoryViewModel
            {
                Inventories = lst
            };

            return PartialView(model);
        }

        public async Task<IActionResult> RequestMaterial()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var request = await _materialRequestService.GetAllMaterialRequestByUserAsync(user.UserCore);

            var lst = request.Select(x => new RequestDetailViewModel
            {
                UserName = x.UserCore.FullName,
                AreaName = x.UserCore.Area.AreaName,
                IsDispatched = x.IsDispatched,
                DispatchedDate = x.DispatchedDate,
                EntryDate = x.EntryDate,
                IsEntry = x.IsFinished,
                RequestCode = x.RequestCode,
                RequestDate = x.CreationDate,
                RequestKey = x.Key,
                RequisitionDetailItemViewModels = x.MaterialRequestDetails.Select(o => new RequestDetailItemViewModel
                {
                    Quantity = o.Quantity,
                    MaterialName = o.Material.MaterialName

                }).ToList(),
            }).ToList();

            var model = new RequestMaterialViewModel
            {
                Requests = lst
            };

            return View(model);

        }

        public async Task<IActionResult> NewRequestMaterial()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var total = await _materialRequestService.GetTotalRequestAsync() + 1;


            var newRequest = new MaterialRequest
            {
                UserCore = user.UserCore,
                CreationDate = DateTime.Now,
                IsDispatched = false,
                IsFinished = false,
                Key = Guid.NewGuid().ToString(),
                RequestCode = $"REQ-ML-{total.ToString().PadLeft(5, '0')}",
            };

            var response = await _materialRequestService.AddMaterialRequestAsync(newRequest);

            if (!response)
            {
                return RedirectToAction("RequestMaterial", "Laboratory")
                    .WithDanger(Constants.ErrorMessage, "Ocurrio un error intenta de nuevo.");
            }

            return RedirectToAction("ConfigureRequest", "Laboratory", new { id = newRequest.Key });
        }

        public async Task<IActionResult> ConfigureRequest(string id)
        {
            var request = await _materialRequestService.GetMaterialRequestByKeyAsync(id);
            var model = new ItemRequestViewModel
            {
                Materials = await _materialService.GetAllMaterialsComboAsync(),
                RequestKey = request.Key,
                Items = request.MaterialRequestDetails.Select(x => new RequestDetailItemViewModel
                {
                    Quantity = x.Quantity,
                    MaterialName = x.Material.MaterialName
                }).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> CreateItem(ItemRequestViewModel model)
        {
            var request = await _materialRequestService.GetMaterialRequestByKeyAsync(model.RequestKey);
            var material = await _materialService.GetMaterialByKeyAsync(model.MaterialKey);

            var item = new MaterialRequestDetail
            {
                Key = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                IsDispatched = false,
                IsEntry = false,
                Material = material,
                MaterialRequest = request,
                Quantity = model.Quantity
            };

            var response = await _materialRequestDetailService.AddRequestDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("ConfigureRequest", "Laboratory", new { id = request.Key })
                    .WithDanger(Constants.ErrorMessage, "No se pudo agregar el material");
            }

            return RedirectToAction("ConfigureRequest", "Laboratory", new { id = request.Key })
                .WithSuccess(Constants.SuccessMessage, "Material agregado correctamente");
        }

        public async Task<IActionResult> DetailRequest(string id)
        {
            var request = await _materialRequestService.GetMaterialRequestByKeyAsync(id);

            var model = new ItemRequestViewModel
            {
                Items = request.MaterialRequestDetails.Select(x => new RequestDetailItemViewModel
                {
                    Quantity = x.Quantity,
                    MaterialName = x.Material.MaterialName
                }).ToList()
            };
            return PartialView(model);
        }
    }
}
