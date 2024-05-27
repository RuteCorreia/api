using Application.DTOs.Cadastros.Piloto.ViewModel;
using AutoMapper;
using Domain.Entidades.User;

namespace Application.DTOs.Cadastros.Piloto.Mappings;

public class PilotoDomainToViewModelMappingProfile : Profile
{
    public PilotoDomainToViewModelMappingProfile()
    {
        CreateMap<Usuario, PilotoViewModel>();
    }
}
