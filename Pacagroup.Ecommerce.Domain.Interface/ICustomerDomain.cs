namespace Pacagroup.Ecommerce.Domain.Interface
{
    public interface ICustomerDomain
    {
        Task<IEnumerable<Customer>> GetAllAsyncAsync();
        Task<Customer?> GetAsync(string customerId);
        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
    }
}
