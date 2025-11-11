namespace Pacagroup.Ecommerce.Aplicacion.Main;

public class CustomerApplication(IMapper mapper, IUnitOfWork unitOfWork) : ICustomerApplication
{
    public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
    {
        Response<IEnumerable<CustomerDTO>> response = new();
        IEnumerable<Customer>? customerList = await unitOfWork.customerRepository.GetAllAsync();

        if (customerList is null)
        {
            response.IsSuccess = false;
            response.Message = "There are no costumers";

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
        Response<CustomerDTO> response = new();
        Customer? customer = await unitOfWork.customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            response.IsSuccess = false;
            response.Message = "This customer doesn't exist";

            return response;
        }

        CustomerDTO customerDTO = mapper.Map<CustomerDTO>(customer);

        response.Data = customerDTO;
        response.IsSuccess = true;
        response.Message = "Get succesfull";

        return response;
    }

    public async Task<Response<CustomerDTO>> InsertAsync(CustomerDTO customerDTO)
    {
        Response<CustomerDTO> response = new();
        Customer customer = mapper.Map<Customer>(customerDTO);
        Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customer.Id.ToString());

        if (existingCustomer is not null)
        {
            response.IsSuccess = false;
            response.Message = "This Customer already exists!";

            return response;
        }

        Customer? data = await unitOfWork.customerRepository.PostAsync(customer);

        if(data is null)
        {
            response.IsSuccess = false;
            response.Message = "Error inserting customer";

            return response;
        }

        await unitOfWork.SaveChangesAsync();
        CustomerDTO customerDTOInserted = mapper.Map<CustomerDTO>(data);

        response.IsSuccess = true;
        response.Message = "Insert successful";
        response.Data = customerDTOInserted;

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
    {
        Response<bool> response = new();
        Customer customer = mapper.Map<Customer>(customerDTO);
        Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customer.Id.ToString());

        if (existingCustomer is null) 
        {
            response.IsSuccess = false;
            response.Message = "This Customer doesn't exist!";

            return response;
        }

        Customer? customerDTOUpdated = await unitOfWork.customerRepository.UpdateAsync(customer, customer.Id.ToString());
        if (customerDTOUpdated is null)
        {
            response.IsSuccess = false;
            response.Message = "This Customer couldn't be updated!";

            return response;
        }

        await unitOfWork.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Update succesfull";

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {
        Response<bool> response = new();
        Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customerId);

        if (existingCustomer is null)
        {
            response.IsSuccess = false;
            response.Message = "This Customer doesn't exist!";

            return response;
        }

        await unitOfWork.customerRepository.DeleteAsync(customerId);
        await unitOfWork.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Delete succesfull";

        return response;
    }
}
