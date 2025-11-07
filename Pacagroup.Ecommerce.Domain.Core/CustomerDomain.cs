
namespace Pacagroup.Ecommerce.Domain.Core;

public class CustomerDomain : ICustomerDomain
{
    public Task<bool> DeleteAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllAsyncAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
