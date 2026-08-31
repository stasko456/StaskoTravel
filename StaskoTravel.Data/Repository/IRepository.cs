using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllAttached();

        Task<T?> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T entity);

        Task SaveChangesAsync();
    }
}
