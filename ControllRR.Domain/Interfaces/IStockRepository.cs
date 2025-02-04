using ControllRR.Domain.Entities;
namespace ControllRR.Domain.Interfaces;

public interface IStockRepository
{
     Task<List<Stock>> FindAllAsync();
     Task<List<Stock>> SearchAsync(string term);
     Task InsertAsync(Stock stock);

     Task<Stock?> GetByIdAsync(int id);
     Task UpdateAsync(Stock stock);

}