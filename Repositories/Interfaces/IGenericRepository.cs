using System.Linq.Expressions;

namespace HospitalManagementWebApp.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void SaveChanges();

        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
