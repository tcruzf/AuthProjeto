using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IDocumentRepository
{
    Task<IEnumerable<Document>> GetAllAsync();
    Task AddAsync(Document document);
    Task SaveChangesAsync();
}