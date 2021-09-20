namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    using CloudEngineMX.Erp.SetinTred.Infrastructure.Data.ViewModels;
    using FluentValidation;

    /// <summary>
    /// Validator to View Model Login
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{CloudEngineMX.Erp.SetinTred.Infrastructure.Data.ViewModels.LoginViewModel}" />
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Debes de ingresar un email")
                .EmailAddress().WithMessage("Debes de ingresar una dirección de email valida");
        }
    }
}
