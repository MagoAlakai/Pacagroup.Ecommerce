namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Commands.CreateCustomerCommand;
public class CreateCustomerHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<CustomerApplication> logger) : IRequestHandler<CreateCustomerCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Response<bool> response = new();
        Domain.Entities.Customer customer = mapper.Map<Domain.Entities.Customer>(request);
        Domain.Entities.Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(request.CustomerId);

        if (existingCustomer is not null)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "This Customer already exists!";

            logger.LogError("Attempted to insert a customer that already exists with ID {CustomerId}.", response.Message);

            return response;
        }

        Domain.Entities.Customer? data = await unitOfWork.customerRepository.PostAsync(customer);

        if (data is null)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "Error inserting customer";

            logger.LogError("An error occurred while inserting a new customer.", response.Message);

            return response;
        }

        await unitOfWork.SaveChangesAsync();
        CustomerDTO customerDTOInserted = mapper.Map<CustomerDTO>(data);

        response.IsSuccess = true;
        response.Message = "Insert successful";
        response.Data = true;

        return response;
    }
}
