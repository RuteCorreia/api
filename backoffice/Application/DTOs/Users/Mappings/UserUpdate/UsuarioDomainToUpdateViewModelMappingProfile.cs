using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.User;

namespace Application.DTOs.Users.Mappings.UserUpdate;

public class UsuarioDomainToUpdateViewModelMappingProfile : Profile
{
    public UsuarioDomainToUpdateViewModelMappingProfile()
    {
        CreateMap<Usuario, UserUpdateViewModel>()
            .ForMember(d => d.FlagTermoResp, opt => opt.MapFrom(s => s.FlagTermoResp ?? false));
    }
}
