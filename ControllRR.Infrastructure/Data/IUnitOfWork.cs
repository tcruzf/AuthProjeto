using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly ControllRRContext _context;
    private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? _transaction;

    public UnitOfWork(ControllRRContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _transaction?.CommitAsync()!;
    }

    public async Task RollbackAsync()
    {
        await _transaction?.RollbackAsync()!;
    }
}