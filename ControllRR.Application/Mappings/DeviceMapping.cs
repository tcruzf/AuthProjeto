using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;


public class DeviceMappingProfile : Profile
{
    public DeviceMappingProfile()
    {
        CreateMap<Device, DeviceDto>().ReverseMap();
    }
}