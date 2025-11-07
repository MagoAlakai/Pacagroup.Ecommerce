namespace Pacagroup.Ecommerce.Infraestructura.Interface;

public interface IRepository<T> where T : BaseEntity
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(string id);
    public Task<T?> PostAsync(T entity);
    public Task<T?> UpdateAsync(T entity, string id);
    public Task<bool> DeleteAsync(string Id);
}
