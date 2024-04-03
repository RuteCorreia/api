using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using AutoMapper;
using Domain.Entidades.User;

namespace Application.DTOs.Cadastros.Engenheiro.Mappings;

public class EngenheiroDomainToViewModelMappingProfile : Profile
{
    public EngenheiroDomainToViewModelMappingProfile()
    {
        CreateMap<Usuario, EngenheiroViewModel>();
    }
}
