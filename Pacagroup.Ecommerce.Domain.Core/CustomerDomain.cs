namespace Pacagroup.Ecommerce.Domain.Core;

public class CustomerDomain : ICustomerDomain
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Customer>> GetAllAsyncAsync()
    {
        IEnumerable<Customer>? customerList = await _unitOfWork.customerRepository.GetAllAsync();

        return customerList;
    }

    public async Task<Customer?> GetAsync(string customerId)
    {
        Customer? customer = await _unitOfWork.customerRepository.GetByIdAsync(customerId);

        return customer;
    }

    public async Task<bool> InsertAsync(Customer customer)
    {
        Customer? existingCustomer = await _unitOfWork.customerRepository.GetByIdAsync(customer.Id.ToString());
        if (existingCustomer is not null) return false;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        Customer? existingCustomer = await _unitOfWork.customerRepository.GetByIdAsync(customer.Id.ToString());
        if (existingCustomer is not null) return false;

        await _unitOfWork.customerRepository.UpdateAsync(customer, customer.Id.ToString());
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
    public async Task<bool> DeleteAsync(string customerId)
    {
        Customer? existingCustomer = await _unitOfWork.customerRepository.GetByIdAsync(customerId);
        if (existingCustomer is not null) return false;

        await _unitOfWork.customerRepository.DeleteAsync(customerId);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
