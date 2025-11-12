namespace Pacagroup.Ecommerce.Infraestructura.Repository;

public class UnitOfWork(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher) : IUnitOfWork
{
    private readonly IRepository<Customer>? _customerRepository;
    private readonly IUserRepository? _userRepository;

    public IRepository<Customer> customerRepository => _customerRepository ?? new BaseRepository<Customer>(appDbContext);
    public IUserRepository userRepository => _userRepository ?? new UserRepository(appDbContext, passwordHasher);

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
