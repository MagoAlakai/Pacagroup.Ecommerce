namespace Pacagroup.Ecommerce.Infraestructura.Repository;

public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    private readonly IRepository<Customer>? _customerRepository;

    public IRepository<Customer> customerRepository => _customerRepository ?? new BaseRepository<Customer>(appDbContext);

    void IDisposable.Dispose()
    {
        appDbContext?.Dispose();
    }

    void IUnitOfWork.SaveChanges()
    {
        appDbContext.SaveChanges();
    }

    async Task IUnitOfWork.SaveChangesAsync()
    {
        await appDbContext.SaveChangesAsync();
    }
}
