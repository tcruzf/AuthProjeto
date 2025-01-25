using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;


public class MaintenanceMappingProfile : Profile
{
    public MaintenanceMappingProfile()
    {
        CreateMap<Maintenance, MaintenanceDto>().ReverseMap();
    }
}