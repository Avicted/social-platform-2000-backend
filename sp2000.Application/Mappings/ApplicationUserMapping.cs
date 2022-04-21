using AutoMapper;
using sp2000.Application.DTO;
using sp2000.Application.Models;

namespace sp2000.Application.Mappings;

public class ApplicationUserMapping : Profile
{
    public ApplicationUserMapping()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>();
        CreateMap<ApplicationUserDto, ApplicationUser>();
    }
}