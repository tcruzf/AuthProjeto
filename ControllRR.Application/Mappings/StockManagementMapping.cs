using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
// StockManagementMappingProfile.cs

public class StockManagementMappingProfile : Profile
{
    public StockManagementMappingProfile()
    {
      CreateMap<Stock, StockDto>()
            .ReverseMap()
            .ForMember(dest => dest.Supplier, opt => opt.Ignore()); // Ignorar na conversão reversa

        CreateMap<StockManagement, StockManagementDto>()
            .ForMember(dest => dest.MaintenanceNumber, 
                opt => opt.MapFrom(src => src.Maintenance != null ? 
                    src.Maintenance.MaintenanceNumber.ToString() : "N/A"))
            .ReverseMap()
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.PurchaseOrder, opt => opt.Ignore())
            .ForMember(dest => dest.Sale, opt => opt.Ignore());
    }
}