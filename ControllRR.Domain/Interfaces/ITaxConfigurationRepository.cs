using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface ITaxConfigurationRepository
{
    Task<List<TaxConfiguration>> FindAllAsync();
    Task<List<TaxConfiguration>> SearchAsync(string term);
    Task<TaxConfiguration?> GetByIdAsync(int id);
}