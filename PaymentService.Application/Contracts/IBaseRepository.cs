using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Contracts
{
    /// <summary>
    /// A generic repository contract for any class that implements a type of BaseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id);
        void Add(T entity);
        IQueryable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params string[] includedProperties);
        void Delete(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
