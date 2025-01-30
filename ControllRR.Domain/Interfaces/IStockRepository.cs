using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IStockRepository
{
     Task<List<Stock>> FindAllAsync();
     Task<List<Stock>> SearchAsync(string term);
}