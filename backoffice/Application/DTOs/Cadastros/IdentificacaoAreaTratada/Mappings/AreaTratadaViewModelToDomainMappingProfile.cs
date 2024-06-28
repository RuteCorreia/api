using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.IdentificacaoAreaTratada.Mappings
{
    public class AreaTratadaViewModelToDomainMappingProfile : Profile
    {
        public AreaTratadaViewModelToDomainMappingProfile()
        {
            CreateMap<AreaTratadaViewModel, Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>()
                .ForMember(dest => dest.GravacaoArea, opt => opt.MapFrom(src => src.Gravacao));
        }
    }
}
