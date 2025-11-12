using Pacagroup.Ecommerce.Aplicacion.DTO.Customer;

namespace Pacagroup.Ecommerce.Aplicacion.Interface;

public interface ICustomerApplication
{
    Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();
    Task<Response<CustomerDTO>> GetAsync(string customerId);
    Task<Response<CustomerDTO>> InsertAsync(CustomerDTO customerDTO);
    Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO);
    Task<Response<bool>> DeleteAsync(string customerId);
}
