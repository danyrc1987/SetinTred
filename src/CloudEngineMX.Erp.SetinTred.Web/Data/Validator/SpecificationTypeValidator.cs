namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
    using FluentValidation;

    public class SpecificationTypeValidator : AbstractValidator<SpecificationTypeViewModel>
    {
        public SpecificationTypeValidator()
        {
            RuleFor(x => x.SpecificationTypeName)
                .NotNull().WithName("Tipo de Especificacion").WithMessage("Debes de ingresar el nombre del {PropertyName}");
        }
    }
}
