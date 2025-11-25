namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Commands.DeleteCustomerCommand;
public sealed record DeleteCustomerCommand : IRequest<Response<bool>>
{
    public string? CustomerId { get; set; }
}
