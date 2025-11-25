namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Queries.GetAllCustomersQuery;
public class GetAllCustomersHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger logger) : IRequestHandler<GetAllCustomersQuery, Response<IEnumerable<CustomerDTO>>>
{
    public async Task<Response<IEnumerable<CustomerDTO>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        Response<IEnumerable<CustomerDTO>> response = new();
        IEnumerable<Domain.Entities.Customer>? customerList = await unitOfWork.customerRepository.GetAllAsync();

        if (customerList is null)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "There are no costumers";

            logger.LogWarning("No customers found in the database.");

            return response;
        }

        IEnumerable<CustomerDTO> customerDTOList = mapper.Map<IEnumerable<CustomerDTO>>(customerList);

        response.Data = customerDTOList;
        response.IsSuccess = true;
        response.Message = "Get All succesfull";

        return response;
    }
}
