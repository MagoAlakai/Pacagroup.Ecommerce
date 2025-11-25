namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Queries.GetAllCustomersQuery;
public sealed record GetAllCustomersQuery : IRequest<Response<IEnumerable<CustomerDTO>>>
{
}
