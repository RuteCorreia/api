using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.User;

namespace Application.DTOs.Users.Mappings.UserList;

public class UsuarioDomainToListViewModelMappingProfile : Profile
{
    public UsuarioDomainToListViewModelMappingProfile()
    {
        CreateMap<Usuario, UserListViewModel>();
    }
}
