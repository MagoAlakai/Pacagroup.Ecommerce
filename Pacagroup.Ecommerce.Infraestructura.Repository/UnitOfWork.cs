namespace Pacagroup.Ecommerce.Infraestructura.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly IRepository<Customer>? _customerRepository;
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IRepository<Customer> customerRepository => _customerRepository ?? new BaseRepository<Customer>(_appDbContext);

    void IDisposable.Dispose()
    {
        if (_appDbContext is not null) _appDbContext.Dispose();
    }

    void IUnitOfWork.SaveChanges()
    {
        _appDbContext.SaveChanges();
    }

    async Task IUnitOfWork.SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}
