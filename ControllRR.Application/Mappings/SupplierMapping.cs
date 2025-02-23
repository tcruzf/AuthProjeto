using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Profiles;

public class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        CreateMap<Supplier, SupplierDto>().ReverseMap();
    }
}