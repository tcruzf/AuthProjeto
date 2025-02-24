using ControllRR.Domain.Entities;
namespace ControllRR.Domain.Interfaces;

public interface IStockRepository : IRepository<Stock>
{
  Task<List<Stock>> FindAllAsync();
  Task<List<Stock>> SearchAsync(string term);
  Task<Stock?> GetByIdAsync(int id);


}