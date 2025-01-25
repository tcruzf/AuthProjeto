using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;


public class SectorMappingProfile : Profile
{
    public SectorMappingProfile()
    {
        CreateMap<Sector, SectorDto>().ReverseMap();
    }
}