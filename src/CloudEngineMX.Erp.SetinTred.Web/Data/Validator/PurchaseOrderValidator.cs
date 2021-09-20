using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using FluentValidation;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    public class PurchaseOrderValidator : AbstractValidator<NewPurchaseOrderViewModel>
    {
        public PurchaseOrderValidator()
        {
            RuleFor(x => x.SupplierName)
                .NotEmpty().WithMessage("Debes de seleccionar un proveedor");
        }
    }
}
