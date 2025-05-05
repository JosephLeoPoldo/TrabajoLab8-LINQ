using Lab8_BryanCama.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab8_BryanCama.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly LinqexampleContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(LinqexampleContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
    public async Task<T?> GetById(int id) => await _dbSet.FindAsync(id);
    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
