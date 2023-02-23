using Domain.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;


    public Repository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _entities = _context.Set<T>();
    }


    public Task<IEnumerable<T>> GetAll()
    {
        return Task.FromResult(_entities.AsEnumerable());
    }

    public Task<T> Get(int id)
    {
        return Task.FromResult(_entities.SingleOrDefault(s => s.Id == id)!);
    }

    public Task<T> Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Update(entity);
        _context.SaveChanges();
        
        return Task.FromResult(entity);
    }

  
    
    public void Remove(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Remove(entity);
        _context.SaveChanges();
    }

    public Task<T> Create(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Add(entity);
        _context.SaveChanges();

        return Task.FromResult(entity);
    }

    public void SaveChanges()
    {
        _context.SaveChangesAsync();
    }
}