using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.Mappings;

public class AplicacaoRelatorioItemDomainToViewModelMappingProfile : Profile
{
    public AplicacaoRelatorioItemDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem, AplicacaoRelatorioItemViewModel>()
            .ForMember(dest => dest.HoraFinal, opt => opt.MapFrom(src => src.HoraTermino))
            .ForMember(dest => dest.HorimetroFinal, opt => opt.MapFrom(src => src.HorimetroTermino))
            .ForMember(dest => dest.UmidadeRelativaArInicial, opt => opt.MapFrom(src => src.UrInicial))
            .ForMember(dest => dest.UmidadeRelativaArFinal, opt => opt.MapFrom(src => src.UrFinal))
            .ForMember(dest => dest.ImagemCondicaoClimatica, opt => opt.MapFrom(src => src.ImagemDadosClimaticos));
    }
}
