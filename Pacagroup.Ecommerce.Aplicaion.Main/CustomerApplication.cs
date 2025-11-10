namespace Pacagroup.Ecommerce.Aplicacion.Main;

public class CustomerApplication(ICustomerServices customerServices, IMapper mapper) : ICustomerApplication
{
    public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
    {
        Response<IEnumerable<CustomerDTO>> response = new();
        try
        {
            IEnumerable<Customer>? customerList = await customerServices.GetAllAsync();
            IEnumerable<CustomerDTO> customerDTOList = mapper.Map<IEnumerable<CustomerDTO>>(customerList);
            if (customerDTOList is not null)
            {
                response.Data = customerDTOList;
                response.IsSuccess = true;
                response.Message = "Get All succesfull";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<Response<CustomerDTO>> GetAsync(string customerId)
    {
        Response<CustomerDTO> response = new();
        try
        {
            Customer? customer = await customerServices.GetAsync(customerId);
            CustomerDTO customerDTO = mapper.Map<CustomerDTO>(customer);
            if (customerDTO is not null)
            {
                response.Data = customerDTO;
                response.IsSuccess = true;
                response.Message = "Get succesfull";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {
        Response<bool> response = new();
        try
        {
            bool data = await customerServices.DeleteAsync(customerId);
            if (data is true)
            {
                response.IsSuccess = true;
                response.Message = "Delete succesfull";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
    {
        Response<bool> response = new();
        try
        {
            Customer customer = mapper.Map<Customer>(customerDTO);
            bool data = await customerServices.InsertAsync(customer);
            if(data is true)
            {
                response.IsSuccess = true;
                response.Message = "Insert succesfull";
            }
        }
        catch(Exception e)
        {
            response.Message = e.Message; 
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
    {
        Response<bool> response = new();
        try
        {
            Customer customer = mapper.Map<Customer>(customerDTO);
            bool data = await customerServices.UpdateAsync(customer);
            if (data is true)
            {
                response.IsSuccess = true;
                response.Message = "Update succesfull";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.IsSuccess = false;
        }

        return response;
    }
}
