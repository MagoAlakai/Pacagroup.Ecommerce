namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Commands.UdpateCustomerCommand;

public class UpdateCustomerHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<CustomerApplication> logger) : IRequestHandler<UpdateCustomerCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Response<bool> response = new();
        Domain.Entities.Customer customer = mapper.Map<Domain.Entities.Customer>(request);
        Domain.Entities.Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customer.Id.ToString());

        if (existingCustomer is null)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "This Customer doesn't exist!";

            logger.LogError("Attempted to update a customer that does not exist with ID {CustomerId}.", response.Message);

            return response;
        }

        Domain.Entities.Customer? customerDTOUpdated = await unitOfWork.customerRepository.UpdateAsync(customer, customer.Id.ToString());
        if (customerDTOUpdated is null)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "This Customer couldn't be updated!";

            logger.LogError("An error occurred while updating the customer with ID {CustomerId}.", response.Message);

            return response;
        }

        await unitOfWork.SaveChangesAsync();

        response.Data = true;
        response.IsSuccess = true;
        response.Message = "Update succesfull";

        return response;
    }
}
