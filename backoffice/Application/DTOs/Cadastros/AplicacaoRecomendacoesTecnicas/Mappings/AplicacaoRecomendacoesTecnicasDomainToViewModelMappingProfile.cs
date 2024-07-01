using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Mappings;

public class AplicacaoRecomendacoesTecnicasDomainToViewModelMappingProfile : Profile
{
    public AplicacaoRecomendacoesTecnicasDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas, AplicacaoRecomendacoesTecnicasViewModel>()
            .ForMember(dest => dest.QtdVeiculante, opt => opt.MapFrom(src => src.QtdeVeiculante))
            .ForMember(dest => dest.NomeAeronave, opt => opt.MapFrom(src => src.NomeAeronave))
            .ForMember(dest => dest.QtdAlturaVoo, opt => opt.MapFrom(src => src.AlturaVooCustom))
            .ForMember(dest => dest.UmidadeRelativaAr, opt => opt.MapFrom(src => src.UrDoAR))
            .ForMember(dest => dest.NomeVeiculante, opt => opt.MapFrom(src => src.Veinculante))
            .ForMember(dest => dest.TipoDeProduto, opt => opt.MapFrom(src => src.TipoDeProduto))
            .ForMember(dest => dest.NomeEquipamento, opt => opt.MapFrom(src => src.NomeEquipamento));

    }
}
