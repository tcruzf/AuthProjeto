using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;


public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}