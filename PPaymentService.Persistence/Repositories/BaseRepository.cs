using PaymentService.Application.Contracts;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentService.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly PaymentDbContext _dbContext;
        private readonly DbSet<T> _entity;

        public BaseRepository(PaymentDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _entity = _dbContext.Set<T>();

        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _entity.FirstOrDefaultAsync(row=> row.Id ==id);
        }

        public void Add(T entity)
        {
            _entity.Add(entity);
            return;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.FirstOrDefaultAsync(predicate);
        }
        public void Delete(T entity)
        {
            _entity.Remove(entity);
        }

        public virtual IQueryable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params string[] includedProperties)
        {
            IQueryable<T> query = _entity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includedProperties != null)
            {
                foreach (string includeProperty in includedProperties)
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

    }
}
