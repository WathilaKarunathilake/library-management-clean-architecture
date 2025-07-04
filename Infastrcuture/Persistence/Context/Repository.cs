using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Infastrucute.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Context
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> AddAsync(T entity)
        {
            var entry = await _dbSet.AddAsync(entity);
            return entry.Entity; 
        }
        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

    }
}
