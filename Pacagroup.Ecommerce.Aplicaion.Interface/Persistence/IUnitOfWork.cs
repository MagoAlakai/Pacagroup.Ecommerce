using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Aplicacion.Interface.Persistence;

public interface IUnitOfWork : IDisposable
{
    IRepository<Customer> customerRepository { get; }
    IUserRepository userRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}
