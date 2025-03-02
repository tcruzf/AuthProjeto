using System.Linq.Expressions;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ControllRR.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly ControllRRContext _context;

        protected BaseRepository(ControllRRContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }
        public virtual async Task<List<T>> SearchAsync(string term,
                Expression<Func<T, bool>> additionalFilter = null,
                    Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
                        params Expression<Func<T, string>>[] propertySelectors)
        {
            var query = _context.Set<T>().AsQueryable();

            // Aplica os includes primeiro
            if (includes != null)
            {
                query = includes(query);
            }

            // Constrói a expressão OR dinamicamente
            // Vou testar, porque no service eu tive muitos problemas. 
            // Na verdade não quero ter que repetir esse metodo para todo mundo, acho que ainda é a forma mais inteligente.
            // Está apresentando erro com as movimentações. Acredito que seja alguma coisa com o dotnet no debian. Vou testar mais tarde
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression body = null;

            foreach (var selector in propertySelectors)
            {
                var memberExpression = (MemberExpression)selector.Body;
                var propertyAccess = Expression.Property(parameter, memberExpression.Member.Name);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsCall = Expression.Call(propertyAccess, containsMethod, Expression.Constant(term));

                body = body == null
                    ? containsCall
                    : Expression.OrElse(body, containsCall);
            }

            // Combina com filtro adicional se existir
            if (additionalFilter != null)
            {
                var additionalExpression = additionalFilter.Body;
                body = body != null
                    ? Expression.AndAlso(body, additionalExpression)
                    : additionalExpression;
            }

            if (body == null) return await query.ToListAsync();

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return await query.Where(lambda).ToListAsync();
        }
    }
}