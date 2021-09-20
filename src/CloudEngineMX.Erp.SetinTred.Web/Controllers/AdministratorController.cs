using CloudEngineMX.Erp.SetinTred.Web.Data;
using Serilog;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using CloudEngineMX.Erp.SetinTred.Infrastructure.Data.Identity;
    using Core.Entities;
    using Core.Interfaces;
    using Data.ViewModels;
    using Extensions.Alerts;
    using Infrastructure.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller to manage system catalogs
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class AdministratorController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly IUserCoreService _userCoreService;
        private readonly ISupplierService _supplierService;
        private readonly IUserService _userService;
        private readonly IOperatingBaseService _operatingBaseService;
        private readonly IConfidenceLevelService _confidenceLevelService;
        private readonly ISpecificationService _specificationService;
        private readonly ISpecificationTypeService _specificationTypeService;
        private readonly ICostCenterService _costCenterService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministratorController"/> class.
        /// </summary>
        /// <param name="areaService">The area service.</param>
        /// <param name="userCoreService">The user core service.</param>
        /// <param name="supplierService">The supplier service.</param>
        /// <param name="userService"></param>
        /// <param name="specificationTypeService"></param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="operatingBaseService"></param>
        /// <param name="confidenceLevelService"></param>
        /// <param name="specificationService"></param>
        /// <param name="logger"></param>
        public AdministratorController(
            IAreaService areaService,
            IUserCoreService userCoreService,
            ISupplierService supplierService,
            IUserService userService,
            IOperatingBaseService operatingBaseService,
            IConfidenceLevelService confidenceLevelService,
            ISpecificationService specificationService,
            ISpecificationTypeService specificationTypeService,
            ICostCenterService costCenterService,
            IMapper mapper,
            ILogger logger)
        {
            _areaService = areaService;
            _userCoreService = userCoreService;
            _supplierService = supplierService;
            _userService = userService;
            _operatingBaseService = operatingBaseService;
            _confidenceLevelService = confidenceLevelService;
            _specificationService = specificationService;
            _specificationTypeService = specificationTypeService;
            _costCenterService = costCenterService;
            _mapper = mapper;
            _logger = logger;
        }


        #region Suppliers
        /// <summary>
        /// Gets all suppliers.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllSupplierAsync();

            var viewModel = new SupplierViewModel
            {
                Action = "Crear",
                NextScheduledEvaluation = DateTime.Now,
                ConfidenceLevels = await _confidenceLevelService.GetAllConfidenceLevelsComboAsync(),
                OperatingBases = await _operatingBaseService.GetOperatingBaseComboAsync(),
                SupplierViewModels = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers).ToList()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Creates the supplier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var suppliers = await _supplierService.GetAllSupplierAsync();

                var viewModel = new SupplierViewModel
                {
                    Action = "Crear",
                    NextScheduledEvaluation = DateTime.Now,
                    ConfidenceLevels = await _confidenceLevelService.GetAllConfidenceLevelsComboAsync(),
                    OperatingBases = await _operatingBaseService.GetOperatingBaseComboAsync(),
                    SupplierViewModels = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers).ToList(),
                    OperatingBaseKey = model.OperatingBaseKey,
                    ConfidenceLevelKey = model.ConfidenceLevelKey
                };

                return View("GetAllSuppliers", viewModel);
            }

            if (model.Action == "Editar")
            {
                var supplier = await _supplierService.GetSupplierByKeyAsync(model.SupplierKey);

                if (supplier == null)
                {
                    return RedirectToAction("GetAllSuppliers", "Administrator")
                        .WithDanger("Operación Incorrecta", "El proveedor seleccionado no existe");
                }

                supplier.Services = model.Services;
                supplier.IsCritical = model.IsCritical;
                supplier.Specification = model.Specification;
                supplier.NextScheduledEvaluation = model.NextScheduledEvaluation;
                supplier.Specification = model.Specification;
                supplier.FiscalName = model.FiscalName;
                supplier.Rfc = model.Rfc;
                supplier.Address = model.Address;
                supplier.Phone = model.Phone;
                supplier.Remarks = model.Remarks;
                supplier.OperatingBase = await _operatingBaseService.GetOperatingBaseByKeyAsync(model.OperatingBaseKey);
                supplier.ConfidenceLevel =
                    await _confidenceLevelService.GetConfidenceLevelByKeyAsync(model.ConfidenceLevelKey);

                var response = await _supplierService.UpdateSupplierAsync(supplier);

                if (!response)
                {
                    return RedirectToAction("GetAllSuppliers", "Administrator")
                        .WithDanger("Operación Incorrecta", "Ocurrio un error al actualizar el proveedor");
                }

                return RedirectToAction("GetAllSuppliers", "Administrator")
                    .WithSuccess("Operación Correcta", "Proveedor actualizado correctamente");
            }

            var newSupplier = new Supplier
            {
                Key = Guid.NewGuid().ToString(),
                OperatingBase = await _operatingBaseService.GetOperatingBaseByKeyAsync(model.OperatingBaseKey),
                ConfidenceLevel = await _confidenceLevelService.GetConfidenceLevelByKeyAsync(model.ConfidenceLevelKey),
                CreationDate = DateTime.Now,
                FiscalName = model.FiscalName,
                IsCritical = model.IsCritical,
                NextScheduledEvaluation = model.NextScheduledEvaluation,
                Remarks = model.Remarks,
                Services = model.Services,
                Specification = model.Specification,
                SpecificationAvailability = "E-File",
                Rfc = model.Rfc,
                Address = model.Address,
                Phone = model.Phone
            };

            var result = await _supplierService.CreateSupplierAsync(newSupplier);

            if (!result)
            {
                return RedirectToAction("GetAllSuppliers", "Administrator")
                    .WithDanger("Operación Incorrecta", "Ocurrio un error al crear el proveedor");
            }

            return RedirectToAction("GetAllSuppliers", "Administrator")
                .WithSuccess("Operación Correcta", "Proveedor creado correctamente");
        }

        /// <summary>
        /// Details the supplier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> DetailSupplier(string id)
        {
            var supplier = await _supplierService.GetSupplierByKeyAsync(id);

            if (supplier == null)
            {
                return RedirectToAction("GetAllSuppliers", "Administrator")
                    .WithDanger("Operación Incorrecta", "El proveedor seleccionado no existe");
            }

            var suppliers = await _supplierService.GetAllSupplierAsync();

            var model = _mapper.Map<SupplierViewModel>(supplier);
            model.Action = "Editar";
            model.SupplierViewModels = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers).ToList();
            model.ConfidenceLevels = await _confidenceLevelService.GetAllConfidenceLevelsComboAsync();
            model.OperatingBases = await _operatingBaseService.GetOperatingBaseComboAsync();

            return View("GetAllSuppliers", model);
        }
        #endregion

        #region Users
        /// <summary>
        /// Users the list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UserList()
        {
            var users = await _userService.GetAllUsersAsync();
            var areas = await _areaService.GetComboAreasAsync();

            var model = new UserViewModel
            {
                Action = "Crear",
                UserViewModels = users.Select(x => new UserViewModel
                {
                    Email = x.Email,
                    FirstName = x.UserCore.FirstName,
                    LastName = x.UserCore.LastName,
                    AreaName = x.UserCore.Area.AreaName,
                    IsActive = x.UserCore.IsActive,
                    UserKey = x.Id
                }).ToList(),
                Areas = areas,
                UserReports = await _userCoreService.GetAllUsersComboAsync()
            };

            return View(model);
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var users = await _userService.GetAllUsersAsync();
                var areas = await _areaService.GetComboAreasAsync();

                var viewModel = new UserViewModel
                {
                    Action = "Crear",
                    UserViewModels = users.Select(x => new UserViewModel
                    {
                        Email = x.Email,
                        FirstName = x.UserCore.FirstName,
                        LastName = x.UserCore.LastName,
                        AreaName = x.UserCore.Area.AreaName,
                        IsActive = x.UserCore.IsActive,
                        UserKey = x.Id
                    }).ToList(),
                    Areas = areas,
                    UserReports = await _userCoreService.GetAllUsersComboAsync()
                };

                return View("UserList", viewModel);
            }

            if (model.Action == "Editar")
            {
                var userActual = await _userService.GetUserByIdAsync(model.UserKey);

                if (userActual == null)
                {
                    var users = await _userService.GetAllUsersAsync();
                    var areas = await _areaService.GetComboAreasAsync();

                    var viewModel = new UserViewModel
                    {
                        Action = "Crear",
                        UserViewModels = users.Select(x => new UserViewModel
                        {
                            Email = x.Email,
                            FirstName = x.UserCore.FirstName,
                            LastName = x.UserCore.LastName,
                            AreaName = x.UserCore.Area.AreaName,
                            IsActive = x.UserCore.IsActive,
                            UserKey = x.Id
                        }).ToList(),
                        Areas = areas
                    };

                    return View("UserList", viewModel);
                }

                var newArea = await _areaService.GetAreaByKeyAsync(model.AreaKey);

                userActual.UserCore.FirstName = model.FirstName;
                userActual.UserCore.LastName = model.LastName;
                userActual.UserCore.Email = model.Email;
                userActual.UserCore.Area = newArea;

                var update = await _userCoreService.UpdateUserCoreAsync(userActual.UserCore);

                if (userActual.Email != model.Email)
                {
                    userActual.Email = model.Email;
                    userActual.UserName = model.Email;
                    await _userService.UpdateUserAsync(userActual);
                }

                if (!update)
                {
                    return RedirectToAction("UserList", "Administrator").WithDanger("Operación Incorrecta",
                        "No se pudo actualizar el usuario seleccionado");
                }

                return RedirectToAction("UserList", "Administrator")
                    .WithSuccess("Operacción Correcta", "Usuario actualizado correctamente");
            }

            var area = await _areaService.GetAreaByKeyAsync(model.AreaKey);
            var report = await _userCoreService.GetUserCoreByKeyAsync(model.UserReportKey);
            var user = new UserCore
            {
                Key = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                Area = area,
                LastName = model.LastName,
                CreationDate = DateTime.Now,
                Email = model.Email,
                IsActive = true,
                Report = report
            };

            var createUserCore = await _userCoreService.CreateUserCoreAsync(user);

            if (createUserCore)
            {
                var newUser = new User
                {
                    Email = model.Email,
                    UserCore = user,
                    UserName = model.Email
                };

                var response = await _userService.AddUserAsync(newUser, "SetinTred2020");

                if (response.Succeeded)
                {
                    var role = model.IsAdministrator == true ? "Administrator" : "User";
                    await _userService.AddUserToRoleAsync(newUser, role);
                    var token = await _userService.GenerateEmailConfirmationTokenAsync(newUser);
                    await _userService.ConfirmEmailAsync(newUser, token);
                }

                return RedirectToAction("UserList", "Administrator")
                    .WithSuccess("Operación Correcta", "Usuario Creado Correctamente");
            }

            return RedirectToAction("UserList", "Administrator")
                .WithDanger("Operación Incorrecta", "No se pudo crear al usuario");
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> EditUser(string id)
        {
            var users = await _userService.GetAllUsersAsync();
            var areas = await _areaService.GetComboAreasAsync();
            var user = await _userService.GetUserByIdAsync(id);

            var model = new UserViewModel
            {
                UserViewModels = users.Select(x => new UserViewModel
                {
                    Email = x.Email,
                    FirstName = x.UserCore.FirstName,
                    LastName = x.UserCore.LastName,
                    AreaName = x.UserCore.Area.AreaName,
                    IsActive = x.UserCore.IsActive,
                    UserKey = x.Id
                }).ToList(),
                Areas = areas,
                Email = user.Email,
                FirstName = user.UserCore.FirstName,
                LastName = user.UserCore.LastName,
                UserKey = user.Id,
                AreaKey = user.UserCore.Area.Key,
                Action = "Editar",
            };

            return View("UserList", model);
        }

        /// <summary>
        /// Desactives the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> DesactiveUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("UserList", "Administrator")
                    .WithDanger("Operación Incorrecta", "El usuario seleccionado no existe");
            }

            if (!user.UserCore.IsActive)
            {
                user.UserCore.IsActive = true;

                var response = await _userCoreService.UpdateUserCoreAsync(user.UserCore);

                if (!response)
                {
                    return RedirectToAction("UserList", "Administrator").WithDanger("Operación Incorrecta",
                        "No fue posible activar al usuario.");
                }

                return RedirectToAction("UserList", "Administrator")
                    .WithSuccess("Operación Correcta", "Usuario activado Correctamente");
            }

            user.UserCore.IsActive = false;

            var data = await _userCoreService.UpdateUserCoreAsync(user.UserCore);

            if (!data)
            {
                return RedirectToAction("UserList", "Administrator").WithDanger("Operación Incorrecta",
                    "No fue posible desactivar al usuario.");
            }

            return RedirectToAction("UserList", "Administrator")
                .WithSuccess("Operación Correcta", "Usuario desactivado Correctamente");
        }

        public IActionResult ChangePassword(string id)
        {
            var model = new ChangePasswordViewModel
            {
                UserKey = id
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangePassword", "Administrator").WithDanger(Constants.ErrorMessage,
                    "No se pudo cambiar la contraseña, intenta de nuevo.");
            }

            var user = await _userService.GetUserByIdAsync(model.UserKey);

            var token = await _userService.GeneratePasswordResetTokenAsync(user);

            var changePassword = await _userService.ResetPasswordAsync(user, token, model.ConfirmPassword);

            if (!changePassword.Succeeded)
            {
                return RedirectToAction("ChangePassword", "Administrator").WithDanger(Constants.ErrorMessage,
                    "No se pudo cambiar la contraseña, intenta de nuevo.");
            }

            _logger.Information(
                $"Se cambio la contraseña del usuario: {user.Email}, el dia: {DateTime.Now.ToShortDateString()}, la nueva contraseña es: {model.ConfirmPassword}");

            return RedirectToAction("UserList", "Administrator")
                .WithSuccess(Constants.SuccessMessage, "Contraseña cambiada correctamente");
        }

        #endregion

        #region Areas
        /// <summary>
        /// Areas the list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AreaList()
        {
            var areas = await _areaService.GetAllAreasAsync();

            var mapAreas = _mapper.Map<IEnumerable<AreaViewModel>>(areas);

            var model = new AreaViewModel
            {
                Action = "Crear",
                AreaViewModels = mapAreas
            };

            return View(model);
        }

        /// <summary>
        /// Creates the area.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateArea(AreaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var areas = await _areaService.GetAllAreasAsync();

                var mapAreas = _mapper.Map<IEnumerable<AreaViewModel>>(areas);

                var viewModel = new AreaViewModel
                {
                    Action = "Crear",
                    AreaViewModels = mapAreas
                };

                return View("AreaList", viewModel);
            }

            var newArea = new Area
            {
                AreaName = model.AreaName,
                CreationDate = DateTime.Now,
                Key = Guid.NewGuid().ToString(),
            };

            var response = await _areaService.CreateAreaAsync(newArea);

            if (!response)
            {
                return RedirectToAction("AreaList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible crear el area");
            }

            return RedirectToAction("AreaList", "Administrator")
                .WithSuccess("Operación Correcta", "Area creada correctamente");
        }
        #endregion

        #region OperatingBase
        /// <summary>
        /// Operatings the base list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OperatingBaseList()
        {
            var bases = await _operatingBaseService.GetAllOperatingBasesAsync();

            var map = _mapper.Map<IEnumerable<OperatingBaseViewModel>>(bases);

            var model = new OperatingBaseViewModel
            {
                Action = "Crear",
                OperatingBaseViewModels = map
            };

            return View(model);
        }

        /// <summary>
        /// Creates the base.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateBase(OperatingBaseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var bases = await _operatingBaseService.GetAllOperatingBasesAsync();

                var map = _mapper.Map<IEnumerable<OperatingBaseViewModel>>(bases);

                var viewModel = new OperatingBaseViewModel()
                {
                    Action = "Crear",
                    OperatingBaseViewModels = map
                };

                return View("OperatingBaseList", viewModel);
            }

            if (model.Action == "Editar")
            {
                var oldBase = await _operatingBaseService.GetOperatingBaseByKeyAsync(model.Key);
                oldBase.OperatingBaseName = model.OperatingBaseName;

                var update = await _operatingBaseService.UpdateOperatingBaseAsync(oldBase);

                if (update)
                {
                    return RedirectToAction("OperatingBaseList", "Administrator")
                        .WithSuccess("Operación Incorrecta", "Base Operativa actualizada correctamente");
                }

                return RedirectToAction("OperatingBaseList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible actualizar la Base Operativa");
            }

            var newBase = new OperatingBase
            {
                OperatingBaseName = model.OperatingBaseName,
                CreationDate = DateTime.Now,
                Key = Guid.NewGuid().ToString(),
                IsActive = true
            };

            var response = await _operatingBaseService.AddOperatingBaseAsync(newBase);

            if (!response)
            {
                return RedirectToAction("OperatingBaseList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible crear el Base");
            }

            return RedirectToAction("OperatingBaseList", "Administrator")
                .WithSuccess("Operación Correcta", "Base creada correctamente");
        }

        /// <summary>
        /// Edits the base.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> EditBase(string id)
        {
            var bases = await _operatingBaseService.GetAllOperatingBasesAsync();
            var operatingBase = await _operatingBaseService.GetOperatingBaseByKeyAsync(id);

            var model = new OperatingBaseViewModel
            {
                Key = operatingBase.Key,
                Action = "Editar",
                OperatingBaseName = operatingBase.OperatingBaseName,
                OperatingBaseViewModels = _mapper.Map<IEnumerable<OperatingBaseViewModel>>(bases)
            };

            return View("OperatingBaseList", model);
        }

        /// <summary>
        /// Desactives the base.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> DesactiveBase(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("OperatingBaseList", "Administrator");
            }

            var operatingBase = await _operatingBaseService.GetOperatingBaseByKeyAsync(id);

            if (operatingBase.IsActive)
            {
                operatingBase.IsActive = false;

                var update = await _operatingBaseService.UpdateOperatingBaseAsync(operatingBase);

                if (update)
                {
                    return RedirectToAction("OperatingBaseList", "Administrator")
                        .WithSuccess("Operación Correcta", "Base desactivada correctamente");
                }

                return RedirectToAction("OperatingBaseList", "Administrator")
                    .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
            }

            operatingBase.IsActive = true;

            var response = await _operatingBaseService.UpdateOperatingBaseAsync(operatingBase);

            if (response)
            {
                return RedirectToAction("OperatingBaseList", "Administrator")
                    .WithSuccess("Operación Correcta", "Base activada correctamente");
            }

            return RedirectToAction("OperatingBaseList", "Administrator")
                .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
        }
        #endregion

        public async Task<IActionResult> CostCenterList()
        {
            var centers = await _costCenterService.GetAllCostCenterAsync();

            var map = _mapper.Map<IEnumerable<CostCenterViewModel>>(centers);

            var model = new CostCenterViewModel
            {
                Action = "Crear",
                CostCenters = map
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCostCenter(CostCenterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var centers = await _costCenterService.GetAllCostCenterAsync();

                var map = _mapper.Map<IEnumerable<CostCenterViewModel>>(centers);

                var viewModel = new CostCenterViewModel
                {
                    Action = "Crear",
                    CostCenters = map
                };

                return View("CostCenterList", viewModel);
            }

            if (model.Action == "Editar")
            {
                var oldCost = await _costCenterService.GetCostCenterByKeyAsync(model.Key);
                oldCost.CostCenterName = model.CostCenterName;

                var update = await _costCenterService.UpdateCostCentarAsync(oldCost);

                if (update)
                {
                    return RedirectToAction("CostCenterList", "Administrator")
                        .WithSuccess("Operación Incorrecta", "Centro de Costos actualizado correctamente");
                }

                return RedirectToAction("CostCenterList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible actualizar el Centro de Costos");
            }

            var newBase = new CostCenter
            {
                CostCenterName = model.CostCenterName,
                CreationDate = DateTime.Now,
                Key = Guid.NewGuid().ToString(),
                IsActive = true
            };

            var response = await _costCenterService.AddCostCenterAsync(newBase);

            if (!response)
            {
                return RedirectToAction("CostCenterList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible crear el Centro de Costos");
            }

            return RedirectToAction("CostCenterList", "Administrator")
                .WithSuccess("Operación Correcta", "Centro de Costos creado correctamente");
        }

        public async Task<IActionResult> EditCostCenter(string id)
        {
            var centers = await _costCenterService.GetAllCostCenterAsync();
            var costCenter = await _costCenterService.GetCostCenterByKeyAsync(id);

            var model = new CostCenterViewModel
            {
                Key = costCenter.Key,
                Action = "Editar",
                CostCenterName = costCenter.CostCenterName,
                CostCenters = _mapper.Map<IEnumerable<CostCenterViewModel>>(centers)
            };

            return View("CostCenterList", model);
        }

        public async Task<IActionResult> DesactiveCostCenter(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("CostCenterList", "Administrator");
            }

            var costCenter = await _costCenterService.GetCostCenterByKeyAsync(id);

            if (costCenter.IsActive)
            {
                costCenter.IsActive = false;

                var update = await _costCenterService.UpdateCostCentarAsync(costCenter);

                if (update)
                {
                    return RedirectToAction("CostCenterList", "Administrator")
                        .WithSuccess("Operación Correcta", "Centro de Costos desactivado correctamente");
                }

                return RedirectToAction("CostCenterList", "Administrator")
                    .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
            }

            costCenter.IsActive = true;

            var response = await _costCenterService.UpdateCostCentarAsync(costCenter);

            if (response)
            {
                return RedirectToAction("CostCenterList", "Administrator")
                    .WithSuccess("Operación Correcta", "Centro de Costos activado correctamente");
            }

            return RedirectToAction("CostCenterList", "Administrator")
                .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
        }

        #region ConfidenceLevel
        /// <summary>
        /// Confidences the level list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ConfidenceLevelList()
        {
            var levels = await _confidenceLevelService.GetAllConfidenceLevelsAsync();

            var map = _mapper.Map<IEnumerable<ConfidenceLevelViewModel>>(levels);

            var model = new ConfidenceLevelViewModel
            {
                Action = "Crear",
                ConfidenceLevelViewModels = map
            };

            return View(model);
        }

        /// <summary>
        /// Creates the confidence level.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateConfidenceLevel(ConfidenceLevelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var levels = await _confidenceLevelService.GetAllConfidenceLevelsAsync();

                var map = _mapper.Map<IEnumerable<ConfidenceLevelViewModel>>(levels);

                var viewModel = new ConfidenceLevelViewModel
                {
                    Action = "Crear",
                    ConfidenceLevelViewModels = map
                };

                return View("ConfidenceLevelList", viewModel);
            }

            if (model.Action == "Editar")
            {
                var oldConfidence = await _confidenceLevelService.GetConfidenceLevelByKeyAsync(model.Key);
                oldConfidence.ConfidenceLevelName = model.ConfidenceLevelName;

                var update = await _confidenceLevelService.UpdateConfidenceLevelAsync(oldConfidence);

                if (update)
                {
                    return RedirectToAction("ConfidenceLevelList", "Administrator")
                        .WithSuccess("Operación Incorrecta", "Nivel de Confianza actualizado correctamente");
                }

                return RedirectToAction("ConfidenceLevelList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible actualizar el Nivel de Confianza");
            }

            var newConfidence = new ConfidenceLevel
            {
                ConfidenceLevelName = model.ConfidenceLevelName,
                CreationDate = DateTime.Now,
                Key = Guid.NewGuid().ToString(),
                IsActive = true
            };

            var response = await _confidenceLevelService.AddConfidenceLevelAsync(newConfidence);

            if (!response)
            {
                return RedirectToAction("ConfidenceLevelList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible crear el Nivel de Confianza");
            }

            return RedirectToAction("ConfidenceLevelList", "Administrator")
                .WithSuccess("Operación Correcta", "Nivel de Confianza creado correctamente");
        }

        /// <summary>
        /// Edits the confidence.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> EditConfidence(string id)
        {
            var levels = await _confidenceLevelService.GetAllConfidenceLevelsAsync();
            var confidenceLevel = await _confidenceLevelService.GetConfidenceLevelByKeyAsync(id);

            var model = new ConfidenceLevelViewModel
            {
                Key = confidenceLevel.Key,
                Action = "Editar",
                ConfidenceLevelName = confidenceLevel.ConfidenceLevelName,
                ConfidenceLevelViewModels = _mapper.Map<IEnumerable<ConfidenceLevelViewModel>>(levels)
            };

            return View("ConfidenceLevelList", model);
        }

        /// <summary>
        /// Desactivates the confidence leve.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> DesactivateConfidenceLeve(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("ConfidenceLevelList", "Administrator");
            }

            var confidenceLevel = await _confidenceLevelService.GetConfidenceLevelByKeyAsync(id);

            if (confidenceLevel.IsActive)
            {
                confidenceLevel.IsActive = false;

                var update = await _confidenceLevelService.UpdateConfidenceLevelAsync(confidenceLevel);

                if (update)
                {
                    return RedirectToAction("ConfidenceLevelList", "Administrator")
                        .WithSuccess("Operación Correcta", "Nivel de Confianza desactivado correctamente");
                }

                return RedirectToAction("ConfidenceLevelList", "Administrator")
                    .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
            }

            confidenceLevel.IsActive = true;

            var response = await _confidenceLevelService.UpdateConfidenceLevelAsync(confidenceLevel);

            if (response)
            {
                return RedirectToAction("ConfidenceLevelList", "Administrator")
                    .WithSuccess("Operación Correcta", "Nivel de Confianza activado correctamente");
            }

            return RedirectToAction("ConfidenceLevelList", "Administrator")
                .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
        }
        #endregion

        /// <summary>
        /// Specifications the types list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SpecificationTypesList()
        {
            var specifications = await _specificationTypeService.GetAllSpecificationTypesAsync();

            var map = _mapper.Map<IEnumerable<SpecificationTypeViewModel>>(specifications);

            var model = new SpecificationTypeViewModel
            {
                Action = "Crear",
                SpecificationTypeViewModels = map
            };

            return View(model);
        }

        /// <summary>
        /// Creates the type of the specification.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateSpecificationType(SpecificationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var specifications = await _specificationTypeService.GetAllSpecificationTypesAsync();

                var map = _mapper.Map<IEnumerable<SpecificationTypeViewModel>>(specifications);

                var viewModel = new SpecificationTypeViewModel
                {
                    Action = "Crear",
                    SpecificationTypeViewModels = map
                };

                return View("SpecificationTypesList", viewModel);
            }

            if (model.Action == "Editar")
            {
                var oldSpecification = await _specificationTypeService.GetSpecificationTypeByKeyAsync(model.Key);
                oldSpecification.SpecificationTypeName = model.SpecificationTypeName;

                var update = await _specificationTypeService.UpdateSpecificationTypeAsync(oldSpecification);

                if (update)
                {
                    return RedirectToAction("SpecificationTypesList", "Administrator")
                        .WithSuccess("Operación Incorrecta", "Tipo de Especificacion actualizado correctamente");
                }

                return RedirectToAction("SpecificationTypesList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible actualizar el Tipo de Especificacion");
            }

            var newSpecification = new SpecificationType
            {
                SpecificationTypeName = model.SpecificationTypeName,
                CreationDate = DateTime.Now,
                Key = Guid.NewGuid().ToString(),
                IsActive = true
            };

            var response = await _specificationTypeService.AddSpecificationTypeAsync(newSpecification);

            if (!response)
            {
                return RedirectToAction("SpecificationTypesList", "Administrator")
                    .WithDanger("Operación Incorrecta", "No fue posible crear el Tipo de Especificacion");
            }

            return RedirectToAction("SpecificationTypesList", "Administrator")
                .WithSuccess("Operación Correcta", "Tipo de Especificacion creado correctamente");
        }

        /// <summary>
        /// Edits the type of the specification.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> EditSpecificationType(string id)
        {
            var specifications = await _specificationTypeService.GetAllSpecificationTypesAsync();
            var specificationType = await _specificationTypeService.GetSpecificationTypeByKeyAsync(id);

            var model = new SpecificationTypeViewModel
            {
                Key = specificationType.Key,
                Action = "Editar",
                SpecificationTypeName = specificationType.SpecificationTypeName,
                SpecificationTypeViewModels = _mapper.Map<IEnumerable<SpecificationTypeViewModel>>(specifications)
            };

            return View("SpecificationTypesList", model);
        }

        /// <summary>
        /// Desactivates the type of the specification.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> DesactivateSpecificationType(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("SpecificationTypesList", "Administrator");
            }

            var specificationType = await _specificationTypeService.GetSpecificationTypeByKeyAsync(id);

            if (specificationType.IsActive)
            {
                specificationType.IsActive = false;

                var update = await _specificationTypeService.UpdateSpecificationTypeAsync(specificationType);

                if (update)
                {
                    return RedirectToAction("SpecificationTypesList", "Administrator")
                        .WithSuccess("Operación Correcta", "Tipo de Especificacion desactivado correctamente");
                }

                return RedirectToAction("SpecificationTypesList", "Administrator")
                    .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
            }

            specificationType.IsActive = true;

            var response = await _specificationTypeService.UpdateSpecificationTypeAsync(specificationType);

            if (response)
            {
                return RedirectToAction("SpecificationTypesList", "Administrator")
                    .WithSuccess("Operación Correcta", "Tipo de Especificacion activado correctamente");
            }

            return RedirectToAction("SpecificationTypesList", "Administrator")
                .WithDanger("Operación Incorrecta", "Ocurrio un error, intenta de nuevo");
        }
    }
}
