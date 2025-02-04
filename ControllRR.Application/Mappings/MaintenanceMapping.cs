using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

public class MaintenanceMappingProfile : Profile
{
    public MaintenanceMappingProfile()
    {
        CreateMap<Maintenance, MaintenanceDto>()
            .ForMember(dest => dest.ApplicationUser, opt => opt.MapFrom(src => src.ApplicationUser)) 
            .ForMember(dest => dest.MaintenanceProducts, opt => opt.MapFrom(src => src.MaintenanceProducts))
            .ReverseMap();

        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap(); 
        CreateMap<Device, DeviceDto>().ReverseMap(); 
    }
}