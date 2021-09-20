namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    using FluentValidation;
    using ViewModels;

    public class ConfidenceLevelValidator : AbstractValidator<ConfidenceLevelViewModel>
    {
        public ConfidenceLevelValidator()
        {
            RuleFor(x => x.ConfidenceLevelName)
                .NotNull().WithName("Nivel de Confianza").WithMessage("Debes de ingresar el nombre del {PropertyName}");
        }
    }
}
