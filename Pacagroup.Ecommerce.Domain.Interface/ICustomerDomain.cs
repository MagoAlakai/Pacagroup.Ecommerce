namespace Pacagroup.Ecommerce.Domain.Interface
{
    public interface ICustomerDomain
    {
        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
        Task<Customer> GetAsync(string customerId);
        Task<IEnumerable<Customer>> GetAllAsyncAsync();
    }
}
