using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
public class StockMappingProfile : Profile
{
    public StockMappingProfile()
    {
        CreateMap<Stock, StockDto>();
        CreateMap<StockManagement, StockManagementDto>()
                    .ForMember(dest => dest.MovementType, opt => opt.MapFrom(src => (int)src.MovementType));

    }
}
