using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace ControllRR.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<List<T>> SearchAsync(string term, 
                Expression<Func<T, bool>> additionalFilter = null,
                    Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
                        params Expression<Func<T, string>>[] propertySelectors);
    }
}