using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;

public interface IDocumentService
{
    Task<IEnumerable<DocumentDto>> GetAllAsync();
    Task AddAsync(DocumentDto documentDto);
    Task<DocumentDto> UploadDocumentAsync(DocumentDto documentDto);
    

}