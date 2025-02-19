using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    private readonly IFileStorageService _fileStorageService;

    public DocumentService(
        IDocumentRepository documentRepository,
        IMapper mapper,
        IFileStorageService fileStorageService,
        IUnitOfWork uow
        )
    {
        _documentRepository = documentRepository;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
        _uow = uow;
    }

    public async Task<IEnumerable<DocumentDto>> GetAllAsync()
    {
        var documents = await _documentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DocumentDto>>(documents);

    }

    public async Task AddAsync(DocumentDto documentDto)
    {
        var document = _mapper.Map<Document>(documentDto);
        await _documentRepository.AddAsync(document);

    }

    public async Task<DocumentDto> UploadDocumentAsync(DocumentDto documentDto)
    {
        if (documentDto.FormFile == null || documentDto.FormFile.Length == 0)
        {
            throw new ArgumentException("Arquivo não fornecido.");

        }
        try
        {
            await _uow.BeginTransactionAsync();
            string uniqueFileName = await _fileStorageService.SaveFileAsync(documentDto.FormFile, "uploads");

            documentDto.DocumentName = uniqueFileName;
            documentDto.UploadedAt = DateTime.Now;

            var document = _mapper.Map<Document>(documentDto);
            await _documentRepository.AddAsync(document);
            await _uow.SaveChangesAsync();
            await _uow.CommitAsync();
            return documentDto;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao salvar arquivo.");
        }

    }

    
}