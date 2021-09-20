using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudEngineMX.Erp.SetinTred.Core.Entities;
using CloudEngineMX.Erp.SetinTred.Core.Interfaces;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Interfaces;
using CloudEngineMX.Erp.SetinTred.Web.Data;
using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using CloudEngineMX.Erp.SetinTred.Web.Extensions.Alerts;
using Microsoft.AspNetCore.Mvc;

namespace CloudEngineMX.Erp.SetinTred.Web.Controllers
{
    public class FinanceController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ICurrencyService _currencyService;
        private readonly ISupplierService _supplierService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FinanceController(
            IPaymentService paymentService,
            ICurrencyService currencyService,
            ISupplierService supplierService,
            IPurchaseOrderService purchaseOrderService,
            IUserService userService,
            IMapper mapper)
        {
            _paymentService = paymentService;
            _currencyService = currencyService;
            _supplierService = supplierService;
            _purchaseOrderService = purchaseOrderService;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PaymentList()
        {

            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var test = User.IsInRole("Administrator");

            if (!user.UserCore.Area.AreaName.Equals("Finanzas") && User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Home")
                    .WithDanger(Constants.ErrorMessage, "NO tienes los permisos requeridos");
            }

            var payments = await _paymentService.GetAllPaymentsAsync();

            var model = new PaymentViewModel
            {
                PayentDate = DateTime.Now,
                Currencies = await _currencyService.GetCurrencyComboAsync(),
                PurcharOrders = await _purchaseOrderService.GetPurchaseOrderForPayment(),
                Payments = payments.Select(x => new PaymentsViewModel
                {
                    CurrencyName = x.Currency.CurrencyName,
                    PaymentDate = x.PaymentDate,
                    PurchaseOrderCode = x.PurchaseOrder.PurchaseOrderCode,
                    SupplierName = x.Supplier.FiscalName,
                    PaymentType = x.PaymentType,
                    Ammount = x.Ammount,
                    PaymentKey = x.Key
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePayment(PaymentViewModel model)
        {
            var po = await _purchaseOrderService.GetPurchaseOrderByKeyAsync(model.PurcharseOrderKey);

            var payment = new Payment
            {
                Currency = await _currencyService.GetCurrencyByKeyAsync(model.CurrencyKey),
                Ammount = model.Ammount,
                CreationDate = DateTime.Now,
                Key = Guid.NewGuid().ToString(),
                PaymentDate = model.PayentDate,
                PaymentType = model.PaymentType,
                PurchaseOrder = po,
                Reference = model.Reference,
                Remarks = model.Remarks,
                Supplier = po.Supplier
            };

            var savePayment = await _paymentService.CreatePaymentAsync(payment);

            if (!savePayment)
            {
                return RedirectToAction("PaymentList", "Finance")
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error al guardar el pago, intenta de nuevo.");
            }

            var poTotal = po.PurchaseOrderDetails.Sum(x => x.Quantity * x.UnitPrice);

            po.Status = poTotal == payment.Ammount ? Constants.TotalPayment : Constants.PartialPayment;

            var updatePo = await _purchaseOrderService.UpdatePurchaseOrderAsync(po);

            if (!updatePo)
            {
                return RedirectToAction("PaymentList", "Finance")
                    .WithDanger(Constants.ErrorMessage, "Ocurrió un error al guardar el pago, intenta de nuevo.");
            }

            return RedirectToAction("PaymentList", "Finance")
                .WithSuccess(Constants.SuccessMessage, "Pago registrado correctamente");
        }

        public async Task<IActionResult> OrderToPayment()
        {
            var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            var test = User.IsInRole("Administrator");

            if (!user.UserCore.Area.AreaName.Equals("Finanzas") && User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Home")
                    .WithDanger(Constants.ErrorMessage, "NO tienes los permisos requeridos");
            }

            var orders = await _purchaseOrderService.GetAllPurchaseOrderByStatusAsync("Aprobada");
            var model = new PurchaseOrdersViewModel
            {
                Orders = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(orders)
            };
            return View(model);
        }
    }
}
