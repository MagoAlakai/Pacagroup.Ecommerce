using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Transversal.Common.Interfaces;
public interface IJwtService
{
    string GenerateToken(User user);
}
