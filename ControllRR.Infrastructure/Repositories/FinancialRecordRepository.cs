using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;

namespace ControllRR.Infrastructure.Repositories;

public class FinancialRecordRepository : BaseRepository<FinancialRecord>, IFinancialRecordRepository
{
    public FinancialRecordRepository(ControllRRContext context) : base(context)
    {

    }

    public Task<List<FinancialRecord>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FinancialRecord?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<FinancialRecord>> SearchAsync(string term)
    {
        throw new NotImplementedException();
    }
}