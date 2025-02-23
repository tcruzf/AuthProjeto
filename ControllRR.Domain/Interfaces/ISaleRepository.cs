using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface ISaleRepository
{

    Task<List<Sale>> FindAllAsync();
    Task<List<Sale>> SearchAsync(string term);
    Task<Sale?> GetByIdAsync(int id);

}
