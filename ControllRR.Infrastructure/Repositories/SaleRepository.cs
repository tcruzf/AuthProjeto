using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;

namespace ControllRR.Infrastructure.Repositories;


public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(ControllRRContext context) : base(context)
    {

    }

    public Task<List<Sale>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Sale>> SearchAsync(string term)
    {
        throw new NotImplementedException();
    }
}
