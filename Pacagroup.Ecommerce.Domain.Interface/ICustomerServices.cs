namespace Pacagroup.Ecommerce.Domain.Interface
{
    public interface ICustomerServices
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetAsync(string customerId);
        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
    }
}
