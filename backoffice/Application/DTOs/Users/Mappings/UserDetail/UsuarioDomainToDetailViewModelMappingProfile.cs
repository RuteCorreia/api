using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.User;

namespace Application.DTOs.Users.Mappings.UserDetail;

public class UsuarioDomainToDetailViewModelMappingProfile : Profile
{
    public UsuarioDomainToDetailViewModelMappingProfile()
    {
        CreateMap<Usuario, UserDetailViewModel>()
            .ForMember(d => d.FlagTermoResp, opt => opt.MapFrom(s => s.FlagTermoResp ?? false));
    }
}
