using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using ProjekatRVA.Core.IRepositories;
using ProjekatRVA.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected PlannerDbContext _context;

        public GenericRepository(PlannerDbContext _context) { 
            this._context = _context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public bool UpdateLoggedIn(T entity)
        {
            var res = _context.Set<T>().Update(entity);
            if(res != null) {
                return true;    
            }
            return false;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> FindById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
