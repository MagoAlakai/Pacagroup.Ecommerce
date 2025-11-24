using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Aplicacion.Interface.Persistence;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> CreateUserAsync(User user, string password);
    Task<bool> IsValidUserAsync(User user, string password);
}
