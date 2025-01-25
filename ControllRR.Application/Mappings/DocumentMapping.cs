using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;


public class DocumentMappingProfile : Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<Document, DocumentDto>().ReverseMap();
    }
}