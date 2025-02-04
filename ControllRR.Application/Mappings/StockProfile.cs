using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        // Mapeamento de StockDto para Stock
        CreateMap<StockDto, Stock>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.ProductQuantity))
            .ForMember(dest => dest.ProductApplication, opt => opt.MapFrom(src => src.ProductApplication))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
            .ForMember(dest => dest.ProductReference, opt => opt.MapFrom(src => src.ProductReference));

        // Mapeamento inverso (opcional)
        CreateMap<Stock, StockDto>();
    }
}