using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
// StockManagementMappingProfile.cs

public class StockManagementMappingProfile : Profile
{
    public StockManagementMappingProfile()
    {
        CreateMap<StockManagement, StockManagementDto>().ReverseMap();
    }
}