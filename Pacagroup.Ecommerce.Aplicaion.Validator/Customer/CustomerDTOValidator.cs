namespace Pacagroup.Ecommerce.Aplicacion.Validator.Customer;
public class CustomerDTOValidator : AbstractValidator<CustomerDTO>
{
    public CustomerDTOValidator()
    {
        RuleFor(RuleFor => RuleFor.CustomerId)
            .NotEmpty().NotNull().WithMessage("CustomerId is required.")
            .MaximumLength(5).WithMessage("CustomerId must not exceed 5 characters.");
        RuleFor(RuleFor => RuleFor.CompanyName)
            .NotEmpty().NotNull().WithMessage("CompanyName is required.")
            .MaximumLength(40).WithMessage("CompanyName must not exceed 40 characters.");
        RuleFor(RuleFor => RuleFor.ContactName)
            .MaximumLength(30).WithMessage("ContactName must not exceed 30 characters.");
        RuleFor(RuleFor => RuleFor.ContactTitle)
            .MaximumLength(30).WithMessage("ContactTitle must not exceed 30 characters.");
        RuleFor(RuleFor => RuleFor.Address)
            .NotEmpty().NotNull().WithMessage("Address is required.")
            .MaximumLength(60).WithMessage("Address must not exceed 60 characters.");
        RuleFor(RuleFor => RuleFor.City)
            .NotEmpty().NotNull().WithMessage("City is required.")
            .MaximumLength(15).WithMessage("City must not exceed 15 characters.");
        RuleFor(RuleFor => RuleFor.Region)
            .NotEmpty().NotNull().WithMessage("Region is required.")
            .MaximumLength(15).WithMessage("Region must not exceed 15 characters.");
        RuleFor(RuleFor => RuleFor.PostalCode)
            .NotEmpty().NotNull().WithMessage("PostalCode is required.")
            .MaximumLength(10).WithMessage("PostalCode must not exceed 10 characters.");
        RuleFor(RuleFor => RuleFor.Country)
            .NotEmpty().NotNull().WithMessage("Country is required.")
            .MaximumLength(15).WithMessage("Country must not exceed 15 characters.");
        RuleFor(RuleFor => RuleFor.Phone)
            .NotEmpty().NotNull().WithMessage("Phone is required.")
            .MaximumLength(24).WithMessage("Phone must not exceed 24 characters.");
        RuleFor(RuleFor => RuleFor.Fax)
            .MaximumLength(24).WithMessage("Fax must not exceed 24 characters.");
    }
}
