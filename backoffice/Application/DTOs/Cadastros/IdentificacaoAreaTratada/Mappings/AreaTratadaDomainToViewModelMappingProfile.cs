using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.IdentificacaoAreaTratada.Mappings
{
    public class AreaTratadaDomainToViewModelMappingProfile : Profile
    {
        public AreaTratadaDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada, AreaTratadaViewModel>()
                .ForMember(dest => dest.Gravacao, opt => opt.MapFrom(src => src.GravacaoArea));
        }
    }
}
