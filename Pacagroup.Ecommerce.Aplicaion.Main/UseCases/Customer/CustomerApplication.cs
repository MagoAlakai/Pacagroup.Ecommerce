namespace Pacagroup.Ecommerce.Aplicacion.Main.UseCases.Customer;
public class CustomerApplication(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<CustomerApplication> logger, CustomerDTOValidator customerDTOValidator) : ICustomerApplication
{
    public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
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

    public async Task<Response<CustomerDTO>> GetAsync(string customerId)
    {
        if (customerId is null) throw new ArgumentNullException(nameof(customerId));

        Response<CustomerDTO> response = new();
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

    public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
    {
        Response<bool> response = new();

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
        Domain.Entities.Customer customer = mapper.Map<Domain.Entities.Customer>(customerDTO);
        Domain.Entities.Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customerDTO.CustomerId);

        if (existingCustomer is not null)
        {
            response.Data = false;
            response.IsSuccess = false;
            response.Message = "This Customer already exists!";

            logger.LogError("Attempted to insert a customer that already exists with ID {CustomerId}.", response.Message);

            return response;
        }

        Domain.Entities.Customer? data = await unitOfWork.customerRepository.PostAsync(customer);

        if(data is null)
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

    public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
    {
        if (customerDTO is null) throw new ArgumentNullException(nameof(customerDTO));

        Response<bool> response = new();
        Domain.Entities.Customer customer = mapper.Map<Domain.Entities.Customer>(customerDTO);
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

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {
        Response<bool> response = new();
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
