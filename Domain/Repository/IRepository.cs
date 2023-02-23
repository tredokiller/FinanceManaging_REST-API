using Domain.Entities;

namespace Domain.Repository;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int type);
    Task<T> Create(T type);
    Task<T> Update(T type);
    void Remove(T type);
}