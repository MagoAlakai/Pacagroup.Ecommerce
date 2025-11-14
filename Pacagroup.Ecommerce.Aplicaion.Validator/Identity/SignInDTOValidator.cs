namespace Pacagroup.Ecommerce.Aplicacion.Validator.Identity;
public class SignInDTOValidator : AbstractValidator<SignInDTO>
{
    public SignInDTOValidator() 
    {
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage("Email is required.")
            .MaximumLength(50).WithMessage("Email must not exceed 50 characters.");
        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password is required.");
    }
}
