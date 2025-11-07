using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pacagroup.Ecommerce.Infraestructura.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var existing = await _appDbContext.Customers.FindAsync(id);
        if (existing is null)
            return false;

        _appDbContext.Customers.Remove(existing);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _appDbContext.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
        return await _appDbContext.Customers.FindAsync(id);
    }

    public async Task<Customer?> PostAsync(Customer entity)
    {
        EntityEntry<Customer> entry = await _appDbContext.Customers.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Customer?> UpdateAsync(Customer entity, string id)
    {
        Customer? existing = await _appDbContext.Customers.FindAsync(id);
        if (existing is null) return null;

        _appDbContext.Entry(existing).CurrentValues.SetValues(entity);
        await _appDbContext.SaveChangesAsync();
        return existing;
    }
}
