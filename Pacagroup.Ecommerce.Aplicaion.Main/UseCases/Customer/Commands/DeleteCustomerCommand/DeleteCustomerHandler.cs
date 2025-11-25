namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Commands.DeleteCustomerCommand;

public class DeleteCustomerHandler(IUnitOfWork unitOfWork, IAppLogger<CustomerApplication> logger) : IRequestHandler<DeleteCustomerCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Response<bool> response = new();
        string customerId = request.CustomerId ?? throw new ArgumentNullException(nameof(request.CustomerId));
        Domain.Entities.Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customerId);

        if (existingCustomer is null)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "This Customer doesn't exist!";

            logger.LogError("Attempted to delete a customer that does not exist with ID {CustomerId}.", response.Message);

            return response;
        }

        await unitOfWork.customerRepository.DeleteAsync(customerId);
        await unitOfWork.SaveChangesAsync();

        response.Data = true;
        response.IsSuccess = true;
        response.Message = "Delete succesfull";

        return response;
    }
}
