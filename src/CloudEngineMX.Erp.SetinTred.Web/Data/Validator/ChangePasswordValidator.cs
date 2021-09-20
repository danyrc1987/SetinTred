using CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels;
using FluentValidation;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.Validator
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Debes de ingresar la nueva contraseña")
                .NotEmpty().WithMessage("Debes de ingresar la contraseña")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres");
            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage("Debes de ingresar la confirmación de la contraseña")
                .NotEmpty().WithMessage("Debes de ingresar la confirmación de la contraseña")
                .Equal(x => x.Password)
                .WithMessage("La confirmación de la contraseña no coincide, favor de verificar");
        }
    }
}
