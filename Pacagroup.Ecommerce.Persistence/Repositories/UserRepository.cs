using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Persistence.Repositories;

public class UserRepository(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher) : IUserRepository
{
    private readonly DbSet<User> _entities = appDbContext.Set<User>();
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher ?? new PasswordHasher<User>();
    public async Task<bool> CreateUserAsync(User user, string password)
    {
        if (user is null || password is null)
            throw new InvalidOperationException("User or password cannot be null or empty for insert.");

        bool entity_exist = await _entities.AnyAsync(x => x.Email == user.Email);
        if (entity_exist is true) return false;

        user.Id = Guid.NewGuid().ToString();
        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        EntityEntry<User> entry = await _entities.AddAsync(user);

        return entry.State is EntityState.Added ? true
                                                : false;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _entities.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<bool> IsValidUserAsync(User user, string password)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, password);
        return await Task.FromResult(result is PasswordVerificationResult.Success);
    }
}
