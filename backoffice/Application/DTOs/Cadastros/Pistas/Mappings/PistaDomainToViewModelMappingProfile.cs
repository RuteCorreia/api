using Application.DTOs.Cadastros.Pistas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Pistas.Mappings;

public class PistaDomainToViewModelMappingProfile : Profile
{
    public PistaDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Pistas.Pista, PistaViewModel>();
    }
}
