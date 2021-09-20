namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    using FluentValidation;
    using ViewModels;

    public class OperatingBaseValidator : AbstractValidator<OperatingBaseViewModel>
    {
        public OperatingBaseValidator()
        {
            RuleFor(x => x.OperatingBaseName)
                .NotNull().WithName("Base Operativa").WithMessage("Debes de ingresar el nombre de la {PropertyName}");
        }
    }
}
