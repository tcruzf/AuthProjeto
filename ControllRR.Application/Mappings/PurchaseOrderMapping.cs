using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
public class PurchaseOrderMappingProfile : Profile
{
    public PurchaseOrderMappingProfile()
    {
        CreateMap<PurchaseOrderDto, PurchaseOrder>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();

        CreateMap<PurchaseItemDto, PurchaseItem>()
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.StockId))
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ReverseMap();
    }
}
