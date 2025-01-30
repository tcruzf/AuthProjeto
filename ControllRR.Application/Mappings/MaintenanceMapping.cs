using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

public class MaintenanceMappingProfile : Profile
{
    public MaintenanceMappingProfile()
    {
        CreateMap<Maintenance, MaintenanceDto>()
            .ForMember(dest => dest.ApplicationUser, opt => opt.MapFrom(src => src.ApplicationUser)) // ✅
            .ReverseMap();

        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap(); // ✅ Adicionar
        CreateMap<Device, DeviceDto>().ReverseMap(); // ✅ Adicionar
    }
}