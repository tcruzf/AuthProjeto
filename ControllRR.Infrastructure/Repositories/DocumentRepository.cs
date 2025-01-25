using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ControllRR.Domain.Interfaces;

public class DocumentRepository : IDocumentRepository
{
    private readonly ControllRRContext _controllRRContext;

    public DocumentRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }
    public async Task<IEnumerable<Document>> GetAllAsync()
    {
      var obj = await _controllRRContext.Documents.ToListAsync();
      return obj;
    }

    public async Task AddAsync(Document document)
    {
      
         _controllRRContext.Documents.Add(document);
         await _controllRRContext.SaveChangesAsync();

    }

      public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }


}