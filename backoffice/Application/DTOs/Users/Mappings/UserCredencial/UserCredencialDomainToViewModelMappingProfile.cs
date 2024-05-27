using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.User;

namespace Application.DTOs.Users.Mappings.UserCredencial;

public class UserCredencialDomainToViewModelMappingProfile : Profile
{
    public UserCredencialDomainToViewModelMappingProfile()
    {
        CreateMap<UsuarioCredencial, RoleObject>();
    }
}
