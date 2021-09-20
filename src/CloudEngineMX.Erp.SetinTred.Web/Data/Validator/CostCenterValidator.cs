using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using FluentValidation;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    public class CostCenterValidator : AbstractValidator<CostCenterViewModel>
    {
        public CostCenterValidator()
        {
            RuleFor(x => x.CostCenterName)
                .NotNull().WithName("Centro de Costos").WithMessage("Debes de ingresar el nombre de la {PropertyName}");
        }
    }
}
