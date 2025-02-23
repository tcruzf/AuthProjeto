using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;

namespace ControllRR.Infrastructure.Repositories;


public class TaxConfigurationRepository : BaseRepository<TaxConfiguration>, ITaxConfigurationRepository
{
    public TaxConfigurationRepository(ControllRRContext context) : base (context)
    {

    }

    public Task<List<TaxConfiguration>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaxConfiguration?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TaxConfiguration>> SearchAsync(string term)
    {
        throw new NotImplementedException();
    }
}