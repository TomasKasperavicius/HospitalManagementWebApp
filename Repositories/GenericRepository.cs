using HospitalManagementWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagementWebApp.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }


        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList(); 
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);  
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList(); 
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges(); 
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();  
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);  
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();  
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  
        }

    }
}
