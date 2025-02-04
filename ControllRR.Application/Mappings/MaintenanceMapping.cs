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
            //.ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != null && src.Id > 0))
            .ReverseMap();

        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap(); 
        CreateMap<Device, DeviceDto>().ReverseMap(); 
    }
}