namespace Pacagroup.Ecommerce.Infraestructura.Interface;

public interface IUnitOfWork : IDisposable
{
    IRepository<Customer> customerRepository { get; }
    IUserRepository userRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}
