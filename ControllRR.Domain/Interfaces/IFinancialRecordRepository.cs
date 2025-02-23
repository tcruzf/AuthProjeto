using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;


public interface IFinancialRecordRepository
{

    Task<List<FinancialRecord>> FindAllAsync();
    Task<List<FinancialRecord>> SearchAsync(string term);
    Task<FinancialRecord?> GetByIdAsync(int id);
}