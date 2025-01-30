using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>()
            .ForMember(dest => dest.Maintenances, opt => opt.MapFrom(src => src.Maintenances))
            .ReverseMap();

      //  CreateMap<Maintenance, MaintenanceDto>().ReverseMap();
    }
}