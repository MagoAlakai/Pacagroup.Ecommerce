namespace Pacagroup.Ecommerce.Aplicacion.Validator.Identity;
public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().NotNull().WithMessage("FirstName is required.")
            .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");
        RuleFor(x => x.LastName)
            .NotEmpty().NotNull().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
        RuleFor(x => x.UserName)
            .NotEmpty().NotNull().WithMessage("UserName is required.")
            .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");
        RuleFor(x => x.PasswordHash)
            .NotEmpty().NotNull().WithMessage("Password is required.");
    }
}
