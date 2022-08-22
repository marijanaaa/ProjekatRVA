using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        T Add(T entity);
        void Delete(T entity);
        bool UpdateLoggedIn(T entity);
        Task<List<T>> GetAllAsync();
        List<T> GetAll();
        Task<T> FindById(int id);
        void Update(T entity);
    }
}
