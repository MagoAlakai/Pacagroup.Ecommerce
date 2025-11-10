namespace Pacagroup.Ecommerce.Domain.Core;

public class CustomerServices(IUnitOfWork unitOfWork) : ICustomerServices
{
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        IEnumerable<Customer>? customerList = await unitOfWork.customerRepository.GetAllAsync();

        return customerList;
    }

    public async Task<Customer?> GetAsync(string customerId)
    {
        Customer? customer = await unitOfWork.customerRepository.GetByIdAsync(customerId);

        return customer;
    }

    public async Task<bool> InsertAsync(Customer customer)
    {
        Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customer.Id.ToString());
        if (existingCustomer is not null) return false;

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customer.Id.ToString());
        if (existingCustomer is not null) return false;

        await unitOfWork.customerRepository.UpdateAsync(customer, customer.Id.ToString());
        await unitOfWork.SaveChangesAsync();

        return true;
    }
    public async Task<bool> DeleteAsync(string customerId)
    {
        Customer? existingCustomer = await unitOfWork.customerRepository.GetByIdAsync(customerId);
        if (existingCustomer is not null) return false;

        await unitOfWork.customerRepository.DeleteAsync(customerId);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
