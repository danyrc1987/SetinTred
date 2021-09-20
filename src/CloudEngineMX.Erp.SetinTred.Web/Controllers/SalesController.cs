using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces;
using CloudEngineMX.Erp.SetinTred.Web.Data;
using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using CloudEngineMX.Erp.SetinTred.Web.Extensions.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly IUserCoreService _userCoreService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMailService _mailService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerDocumentService _customerDocumentService;
        private readonly ICustomerQuoteService _customerQuoteService;
        private readonly ICustomerQuoteDetailService _customerQuoteDetailService;
        private readonly ICurrencyService _currencyService;

        public SalesController(
            IUserCoreService userCoreService,
            IUserService userService,
            IMapper mapper,
            ILogger logger,
            IWebHostEnvironment hostEnvironment,
            IMailService mailService,
            ICustomerService customerService,
            ICustomerDocumentService customerDocumentService,
            ICustomerQuoteService customerQuoteService,
            ICustomerQuoteDetailService customerQuoteDetailService,
            ICurrencyService currencyService)
        {
            _userCoreService = userCoreService;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _mailService = mailService;
            _customerService = customerService;
            _customerDocumentService = customerDocumentService;
            _customerQuoteService = customerQuoteService;
            _customerQuoteDetailService = customerQuoteDetailService;
            _currencyService = currencyService;
        }

        public async Task<IActionResult> CustomerList()
        {
            var data = await _customerService.GetAllCustomersAsync();
            var model = new NewCustomerViewModel
            {
                Customers = data.Select(x => new NewCustomerViewModel
                {
                    SocialName = x.SocialName,
                    FiscalName = x.FiscalName,
                    Rfc = x.Rfc,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    PersonType = x.PersonType,
                    Id = x.Id
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(NewCustomerViewModel model)
        {
            var customer = new Customer
            {
                Address = model.Address,
                SocialName = model.SocialName,
                PersonType = model.PersonType,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                AdministrativeContactName = model.AdministrativeContactName,
                AdministrativeContactPhone = model.AdministrativeContactPhone,
                AdministrativeContactEmail = model.AdministrativeContactEmail,
                FinancialContactName = model.FinancialContactName,
                FinancialContactPhone = model.FinancialContactPhone,
                CreationDate = DateTime.Now,
                FinancialContactEmail = model.FinancialContactEmail,
                FiscalName = model.FiscalName,
                Key = Guid.NewGuid().ToString(),
                Rfc = model.Rfc
            };

            var response = await _customerService.CreateCustomerAsync(customer);

            if (!response)
            {
                return RedirectToAction("CustomerList", "Sales")
                    .WithDanger(Constants.ErrorMessage, "No fue posible crear al cliente, intenta de nuevo.");
            }

            return RedirectToAction("CustomerList", "Sales")
                .WithSuccess(Constants.SuccessMessage, "Cliente creado correctamente");
        }

        public async Task<IActionResult> CustomerEdit(int id)
        {
            var data = await _customerService.GetCustomerByIdAsync(id);

            var model = new NewCustomerViewModel
            {
                Address = data.Address,
                SocialName = data.SocialName,
                PersonType = data.PersonType,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                AdministrativeContactName = data.AdministrativeContactName,
                AdministrativeContactPhone = data.AdministrativeContactPhone,
                AdministrativeContactEmail = data.AdministrativeContactEmail,
                FinancialContactName = data.FinancialContactName,
                FinancialContactPhone = data.FinancialContactPhone,
                FinancialContactEmail = data.FinancialContactEmail,
                Id = data.Id,
                FiscalName = data.FiscalName,
                Rfc = data.Rfc
            };

            return View(model);
        }

        public async Task<IActionResult> UpdateCustomer(NewCustomerViewModel model)
        {
            var data = await _customerService.GetCustomerByIdAsync(model.Id);

            data.AdministrativeContactName = model.AdministrativeContactName;
            data.AdministrativeContactEmail = model.AdministrativeContactEmail;
            data.AdministrativeContactPhone = model.AdministrativeContactPhone;
            data.FinancialContactEmail = model.FinancialContactEmail;
            data.FinancialContactName = model.FinancialContactName;
            data.FinancialContactPhone = model.FinancialContactPhone;
            data.FiscalName = model.FiscalName;
            data.Rfc = model.Rfc;
            data.Address = model.Address;
            data.Email = model.Email;
            data.PhoneNumber = model.PhoneNumber;
            data.PersonType = model.PersonType;
            data.SocialName = model.SocialName;

            var response = await _customerService.UpdateCustomerAsync(data);

            if (!response)
            {
                return RedirectToAction("CustomerEdit", "Sales", new { id = model.Id })
                    .WithDanger(Constants.ErrorMessage, "No fue posible actualizar el cliente, intenta de nuevo.");
            }

            return RedirectToAction("CustomerList", "Sales")
                .WithSuccess(Constants.SuccessMessage, "Cliente actualizado correctamente");
        }

        public async Task<IActionResult> CustomerQuotesList()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
            var data = await _customerQuoteService.GetAllCustomerQuotesByUserAsync(user.UserCore);
            var model = new CustomerQuotesViewModel
            {
                Quotes = data.Select(x => new CustomerQuotesViewModel
                {
                    Status = x.Status,
                    PaymentType = x.PaymentType,
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
                }).ToList(),
            };
            return View(model);
        }

        public async Task<IActionResult> NewCustomerQuote()
        {
            var model = new NewCustomerQuoteViewModel
            {
                Customers = await _customerService.GetAllCustomerComboAsync(),
                Currencies = await _currencyService.GetCurrencyComboAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomerQuote(NewCustomerQuoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Currencies = await _currencyService.GetCurrencyComboAsync();
                model.Customers = await _customerService.GetAllCustomerComboAsync();
                return View("NewCustomerQuote", model);
            }

            var total = await _customerQuoteService.GetCountAllByNormativeAsync(model.NormativeData) + 1;
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var quote = new CustomerQuote
            {
                CreationDate = DateTime.Now,
                Currency = await _currencyService.GetCurrencyByKeyAsync(model.CurrencyKey),
                Customer = await _customerService.GetCustomerByIdAsync(Convert.ToInt32(model.CustomerId)),
                DeliveryTime = model.DeliveryTime,
                Key = Guid.NewGuid().ToString(),
                LegalDocumentation = model.LegalDocumentation,
                ManufacturingStandard = model.ManufacturingStandard,
                NormativeData = model.NormativeData,
                PaymentType = model.PaymentType,
                QualityProcess = model.QualityProcess,
                QuoteCode = $"STR-RTP-{total.ToString().PadLeft(4, '0')}-{QuoteSerie.GetQuoteSeries(model.NormativeData)}",
                QuoteType = model.QuoteType,
                Status = "Creación",
                TechnicalData = model.TechnicalData,
                Validity = "7 Días",
                Warranty = model.Warranty,
                User = user.UserCore,
                Lab = model.Lab
            };

            var response = await _customerQuoteService.CreateCustomerQuoteAsync(quote);

            if (!response)
            {
                model.Currencies = await _currencyService.GetCurrencyComboAsync();
                model.Customers = await _customerService.GetAllCustomerComboAsync();
                return View("NewCustomerQuote", model);
            }

            return RedirectToAction("AddItemsQuote", "Sales", new { id = quote.Key })
                .WithSuccess(Constants.SuccessMessage,
                    "Cotizacion creada correctamente correcta, favor de configurar los items.");
        }

        public async Task<IActionResult> AddItemsQuote(string id)
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

        public async Task<IActionResult> CreateItemQuote(string id)
        {
            var quote = await _customerQuoteService.GetCustomerQuoteByKeyAsync(id);

            var model = new CustomerQuoteItemViewModel
            {
                QuoteKey = quote.Key
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItemQuote(CustomerQuoteItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddItemsQuote", "Sales", new { id = model.QuoteKey })
                    .WithDanger(Constants.ErrorMessage,
                        "Ocurrio un error, intenta de nuevo.");
            }

            var quote = await _customerQuoteService.GetCustomerQuoteByKeyAsync(model.QuoteKey);
            var consecutive = await _customerQuoteDetailService.GetCountAllByCustomerQuote(quote) + 1;

            var item = new CustomerQuoteDetail
            {
                Availability = model.Availability,
                CreationDate = DateTime.Now,
                CustomerQuoteId = quote.Id,
                Description = model.Description,
                Quantity = model.Quantity,
                TypeItem = model.TypeItem,
                UnitPrice = model.UnitPrice,
                Unit = model.Unit,
                Key = Guid.NewGuid().ToString(),
                Consecutive = consecutive,
                CustomerQuote = quote,
                Scope = model.Scope,
                Remarks = model.Remarks
            };

            var response = await _customerQuoteDetailService.CreateDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("AddItemsQuote", "Sales", new { id = model.QuoteKey })
                    .WithDanger(Constants.ErrorMessage,
                        "Ocurrio un error, intenta de nuevo.");
            }

            return RedirectToAction("AddItemsQuote", "Sales", new { id = model.QuoteKey })
                .WithSuccess(Constants.SuccessMessage,
                    "Item agregado correctamente.");
        }

        public async Task<IActionResult> DeleteItemQuote(string id)
        {
            var item = await _customerQuoteDetailService.GetDetailByKeyAsync(id);

            var response = await _customerQuoteDetailService.DeleteDetailAsync(item);

            if (!response)
            {
                return RedirectToAction("AddItemsQuote", "Sales", new { id = item.CustomerQuote.Key })
                    .WithDanger(Constants.ErrorMessage,
                        "Ocurrio un error, intenta de nuevo.");
            }

            return RedirectToAction("AddItemsQuote", "Sales", new { id = item.CustomerQuote.Key })
                .WithSuccess(Constants.SuccessMessage,
                    "Item eliminado correctamente.");
        }

        [HttpPost]
        public async Task<IActionResult> SendToValidateQuote(ApproveCustomerQuoteViewModel model)
        {
            var quote = await _customerQuoteService.GetCustomerQuoteByKeyAsync(model.Id);

            quote.Status = Constants.PendingG1;

            var response = await _customerQuoteService.UpdateCustomerQuoteAsync(quote);

            #region SendEmail

            UserCore director;
            director = await _userCoreService.GetUserGeneralDirectionAsync("kingdavid_5@hotmail.com");

            var link = Url.Action("Index", "Home", HttpContext.Request.Scheme);
            var webRoot = _hostEnvironment.WebRootPath;
            string[] labels =
            {
                "[#DirectorName]", "[#QuoteCode]", "[#SaleManager]", "[#QuoteCode]", "[#SalesManager]",
                "[#CustomerName]", "[#Link]"
            };
            string[] data =
            {
                director.FullName, quote.QuoteCode, quote.User.FullName, quote.QuoteCode, quote.User.FullName,
                quote.Customer.SocialName, link
            };
            string[] to = { director.Email };
            string[] cc = { quote.User.Email };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Templates/ApproveCustomerQuote.html");

            var view = _mailService.GetHtml(path, webRoot, labels, data);

            var mail = await _mailService.SendEmail(to, cc, $"Solicitud de aprobación de cotización folio: {quote.QuoteCode}", view);

            #endregion

            return !response ? StatusCode(StatusCodes.Status400BadRequest) : Ok();
        }

        public async Task<IActionResult> DetailQuote(string id)
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

        public async Task<IActionResult> EditQuote(string id)
        {
            var data = await _customerQuoteService.GetCustomerQuoteByKeyAsync(id);
            var model = new EditCustomerQuoteDetailViewModel
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
                Customers = await _customerService.GetAllCustomerComboAsync(),
                Currencies = await _currencyService.GetCurrencyComboAsync(),
                CustomerId = data.Customer.Id.ToString(),
                CurrencyKey = data.Currency.Key,
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
                    Unit = x.Unit
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditQuote(EditCustomerQuoteDetailViewModel model)
        {
            var quote = await _customerQuoteService.GetCustomerQuoteByKeyAsync(model.QuoteKey);

            if (!quote.NormativeData.Equals(model.NormativeData))
            {
                var total = await _customerQuoteService.GetCountAllByNormativeAsync(model.NormativeData) + 1;
                quote.QuoteCode =
                    $"STR-RTP-{total.ToString().PadLeft(4, '0')}-{QuoteSerie.GetQuoteSeries(model.NormativeData)}";
            }

            quote.CreationDate = DateTime.Now;
            quote.Currency = await _currencyService.GetCurrencyByKeyAsync(model.CurrencyKey);
            quote.Customer = await _customerService.GetCustomerByIdAsync(Convert.ToInt32(model.CustomerId));
            quote.DeliveryTime = model.DeliveryTime;
            quote.LegalDocumentation = model.LegalDocumentation;
            quote.ManufacturingStandard = model.ManufacturingStandard;
            quote.NormativeData = model.NormativeData;
            quote.PaymentType = model.PaymentType;
            quote.QualityProcess = model.QualityProcess;
            quote.QuoteType = model.QuoteType;
            quote.TechnicalData = model.TechnicalData;
            quote.Warranty = model.Warranty;
            quote.Lab = model.Lab;

            var response = await _customerQuoteService.UpdateCustomerQuoteAsync(quote);

            if (!response)
            {
                return RedirectToAction("EditQuote", "Sales", new { id = model.QuoteKey })
                    .WithDanger(Constants.ErrorMessage, "Ocurrio un error al actualizar la cotización, intenta de nuevo.");
            }

            return RedirectToAction("EditQuote", "Sales", new { id = model.QuoteKey })
                .WithSuccess(Constants.SuccessMessage, "Cotización actualizada correctamente.");
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
                ItemKey = item.Key,
                Scope = item.Scope,
                Remarks = item.Remarks
            };
            return View(model);
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
            item.Scope = model.Scope;
            item.Remarks = model.Remarks;

            var update = await _customerQuoteDetailService.UpdateDetailAsync(item);

            if (!update)
            {
                return RedirectToAction("AddItemsQuote", "Sales", new { id = model.QuoteKey })
                    .WithDanger(Constants.ErrorMessage, "No fue posible editar el elemento, intenta de nuevo.");
            }

            return RedirectToAction("AddItemsQuote", "Sales", new { id = model.QuoteKey })
                .WithSuccess(Constants.SuccessMessage, "Elemento actualizado correctamente.");
        }

        public async Task<IActionResult> PrintQuote(string id)
        {
            var data = await _customerQuoteService.GetCustomerQuoteByKeyAsync(id);
            var model = new PrintCustomerQuoteDetailViewModel
            {
                LegalDocumentation = data.LegalDocumentation,
                CreationDate = data.CreationDate,
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
                    Unit = x.Unit
                }).ToList()
            };
            return View(model);
        }
    }
}
