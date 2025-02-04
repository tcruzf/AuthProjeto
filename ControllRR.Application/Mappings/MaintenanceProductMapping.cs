using AutoMapper;
using ControllRR.Domain.Entities;
using ControllRR.Application.Dto;

public class MaintenanceProductProfile : Profile
{
    public MaintenanceProductProfile()
    {
        CreateMap<MaintenanceProduct, MaintenanceProductDto>()
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
            .ReverseMap();

        CreateMap<Stock, StockDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ReverseMap();
    }
}