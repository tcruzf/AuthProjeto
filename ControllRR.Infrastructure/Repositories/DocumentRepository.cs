/*
  Classe DocumentRepositoy
  Responsavel por persistir as informações sobre os documentos enviados para o sistema.

*/
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

  //Retorna uma lista de documentos contidos na base de dados
  public async Task<IEnumerable<Document>> GetAllAsync()
  {
    var obj = await _controllRRContext.Documents.ToListAsync();
    return obj;
  }

  // Adiciona as informações de um determinado documento enviado para o sistema ao banco de dados
  public async Task AddAsync(Document document)
  {

    _controllRRContext.Documents.Add(document);
    await _controllRRContext.SaveChangesAsync();

  }

  // Persiste as informações de documentos no banco de dados
  public async Task SaveChangesAsync()
  {
    await _controllRRContext.SaveChangesAsync();
  }


}