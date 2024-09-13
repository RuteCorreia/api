using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Controle_De_Frota.Mappings;

public class ControleDeFrotaDomainToViewModelMappingProfile : Profile
{
    public ControleDeFrotaDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota, ControleDeFrotaViewModel>()
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.StatusEnvio));
    }
}
