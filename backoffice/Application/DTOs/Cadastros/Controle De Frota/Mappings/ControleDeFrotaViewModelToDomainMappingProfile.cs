using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Controle_De_Frota.Mappings
{
    public class ControleDeFrotaViewModelToDomainMappingProfile : Profile
    {
        public ControleDeFrotaViewModelToDomainMappingProfile()
        {
            CreateMap<ControleDeFrotaViewModel, Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>()
                .ForMember(dest => dest.StatusEnvio, opt => opt.MapFrom(src => src.State));
        }
    }
}
