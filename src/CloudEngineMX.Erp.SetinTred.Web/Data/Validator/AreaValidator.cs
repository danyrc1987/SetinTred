namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
    using FluentValidation;

    public class AreaValidator : AbstractValidator<AreaViewModel>
    {
        public AreaValidator()
        {
            RuleFor(x => x.AreaName)
                .NotNull().WithName("Area").WithMessage("Debes ingresar el nombre del {PropertyName}");
        }
    }
}
