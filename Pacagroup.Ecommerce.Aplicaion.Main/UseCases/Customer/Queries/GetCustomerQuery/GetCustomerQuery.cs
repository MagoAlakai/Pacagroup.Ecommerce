namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Queries.GetCustomerQuery;

public sealed record class GetCustomerQuery : IRequest<Response<CustomerDTO>>
{
    public string? CustomerId { get; init; }
}
