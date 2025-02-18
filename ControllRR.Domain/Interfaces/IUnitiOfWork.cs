using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ControllRR.Domain.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    DbContext Context { get; }
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}