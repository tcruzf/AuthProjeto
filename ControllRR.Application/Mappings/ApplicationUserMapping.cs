using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;


public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
    }
}