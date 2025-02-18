/*
  Classe DocumentRepositoy
  Responsavel por persistir as informações sobre os documentos enviados para o sistema.

*/
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Repositories;

public class DocumentRepository : BaseRepository<Document> ,IDocumentRepository
{

  public DocumentRepository(ControllRRContext context) : base(context)
  { 
  }

  //Retorna uma lista de documentos contidos na base de dados
  public async Task<IEnumerable<Document>> GetAllAsync()
  {
    var obj = await _context.Documents.ToListAsync();
    return obj;
  }

  // Adiciona as informações de um determinado documento enviado para o sistema ao banco de dados
  public async Task AddAsync(Document document)
  {

    _context.Documents.Add(document);
  

  }

  // Persiste as informações de documentos no banco de dados
  

}