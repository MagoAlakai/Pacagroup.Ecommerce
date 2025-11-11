namespace Pacagroup.Ecommerce.Infraestructura.Repository;

public class BaseRepository<T>(AppDbContext appDbContext) : IRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _entities = appDbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        IEnumerable<T> entities = await _entities.ToListAsync();
        return entities;
    }
    public async Task<T?> GetByIdAsync(string id)
    {
        return await _entities.FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<T?> PostAsync(T entity)
    {
        bool entity_exist = await _entities.AnyAsync(x => x.Id == entity.Id);
        if (entity_exist is true) return null;

        EntityEntry<T> entry = await _entities.AddAsync(entity);

        return entry.Entity;
    }
    public async Task<T?> UpdateAsync(T entity, string id)
    {
        bool entity_exist = await _entities.AnyAsync(x => x.Id == entity.Id);
        if (entity_exist is false) return null;

        T updated_entity = _entities.Update(entity).Entity;

        return updated_entity;
    }
    public async Task<bool> DeleteAsync(string id)
    {
        T? entity_exist = await _entities.FirstOrDefaultAsync(e => e.Id == id);
        if (entity_exist is null) return false;

        _entities.Remove(entity_exist);

        return true;
    }
}
