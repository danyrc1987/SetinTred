namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    using FluentValidation;
    using ViewModels;

    public class RequisitionValidator : AbstractValidator<RequisitionsListViewModel>
    {
        public RequisitionValidator()
        {
            RuleFor(x => x.Classification)
                .NotEmpty().WithMessage("Seleccionar una clasificación");
            RuleFor(x => x.Application)
                .NotNull().WithMessage("Debes de ingresar una aplicación");
            RuleFor(x => x.AreaKey)
                .NotEmpty().WithMessage("Selecciona una Area");
            RuleFor(x => x.OperatingBaseKey)
                .NotEmpty().WithMessage("Selecciona una Base Operativa");
        }
    }
}
