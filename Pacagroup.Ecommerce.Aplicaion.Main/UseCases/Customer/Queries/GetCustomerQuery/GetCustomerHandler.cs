namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Queries.GetCustomerQuery;
public class GetCustomerHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetCustomerHandler> logger) : IRequestHandler<GetCustomerQuery, Response<CustomerDTO>>
{
    public async Task<Response<CustomerDTO>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        Response<CustomerDTO> response = new();
        string customerId = request.CustomerId ?? throw new ArgumentNullException(nameof(request.CustomerId));
        Domain.Entities.Customer? customer = await unitOfWork.customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            response.Data = null;
            response.IsSuccess = false;
            response.Message = "This customer doesn't exist";

            logger.LogError("Customer with ID {CustomerId} not found.", response.Message);

            return response;
        }

        CustomerDTO customerDTO = mapper.Map<CustomerDTO>(customer);

        response.Data = customerDTO;
        response.IsSuccess = true;
        response.Message = "Get succesfull";

        return response;
    }
}
