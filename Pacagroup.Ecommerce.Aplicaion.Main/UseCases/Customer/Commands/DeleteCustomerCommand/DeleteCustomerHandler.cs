namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer.Commands.DeleteCustomerCommand;

public class DeleteCustomerHandler(IUnitOfWork unitOfWork, IAppLogger<DeleteCustomerCommand> logger, IMapper mapper, CustomerDTOValidator customerDTOValidator) : IRequestHandler<DeleteCustomerCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Response<bool> response = new();

        CustomerDTO customerDTO = mapper.Map<CustomerDTO>(request);
        ValidationResult? validation = await customerDTOValidator.ValidateAsync(customerDTO);

        if (validation.IsValid is false)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "Validation errors";
            response.Errors = validation.Errors;

            logger.LogError("Validation errors", response.Errors);

            return response;
        }

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
