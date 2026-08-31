using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StaskoTravelDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(StaskoTravelDbContext _context)
        {
            this.context = _context;
            this.dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public IQueryable<T> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
