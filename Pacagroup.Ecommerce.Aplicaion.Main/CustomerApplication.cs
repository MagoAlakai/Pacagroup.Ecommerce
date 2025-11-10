using Pacagroup.Ecommerce.Aplicacion.DTO;
using Pacagroup.Ecommerce.Domain.Entity;
using System.Collections.Generic;

namespace Pacagroup.Ecommerce.Aplicacion.Main;

public class CustomerApplication : ICustomerApplication
{
    private readonly ICustomerServices _customerServices;
    private readonly IMapper _mapper;

    public CustomerApplication(ICustomerServices customerServices, IMapper mapper)
    {
        _customerServices = customerServices;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
    {
        Response<IEnumerable<CustomerDTO>> response = new();
        try
        {
            IEnumerable<Customer>? customerList = await _customerServices.GetAllAsync();
            IEnumerable<CustomerDTO> customerDTOList = _mapper.Map<IEnumerable<CustomerDTO>>(customerList);
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
            Customer? customer = await _customerServices.GetAsync(customerId);
            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
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
            bool data = await _customerServices.DeleteAsync(customerId);
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
            Customer customer = _mapper.Map<Customer>(customerDTO);
            bool data = await _customerServices.InsertAsync(customer);
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
            Customer customer = _mapper.Map<Customer>(customerDTO);
            bool data = await _customerServices.UpdateAsync(customer);
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
