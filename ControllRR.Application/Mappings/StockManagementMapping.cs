using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
// StockManagementMappingProfile.cs

public class StockManagementMappingProfile : Profile
{
    public StockManagementMappingProfile()
    {
        CreateMap<StockManagement, StockManagementDto>()
       .ForMember(dest => dest.MaintenanceNumber, 
                         opt => opt.MapFrom(src => src.Maintenance != null ? src.Maintenance.MaintenanceNumber.ToString() : "N/A"));
    }
}