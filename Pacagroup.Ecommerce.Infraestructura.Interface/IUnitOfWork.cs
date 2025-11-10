namespace Pacagroup.Ecommerce.Infraestructura.Interface;

public interface IUnitOfWork : IDisposable
{
    IRepository<Customer> customerRepository { get; }

    void SaveChanges();
    Task SaveChangesAsync();
}
