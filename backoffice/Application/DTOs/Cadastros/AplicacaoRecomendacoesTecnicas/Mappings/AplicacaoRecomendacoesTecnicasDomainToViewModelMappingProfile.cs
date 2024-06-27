using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Mappings;

public class AplicacaoRecomendacoesTecnicasDomainToViewModelMappingProfile : Profile
{
    public AplicacaoRecomendacoesTecnicasDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas, AplicacaoRecomendacoesTecnicasViewModel>()
            .ForMember(dest => dest.QtdVeiculante, opt => opt.MapFrom(src => src.QtdeVeiculante))
            .ForMember(dest => dest.Aeronave, opt => opt.MapFrom(src => src.NomeAeronave))
            .ForMember(dest => dest.AlturaVoo, opt => opt.MapFrom(src => src.AlturaVooCustom))
            //.ForMember(dest => dest.UmidadeRelativaAr, opt => opt.MapFrom(src => src.UrDoAR.ToString()))
            .ForMember(dest => dest.TipoProduto, opt => opt.MapFrom(src => src.TipoDeProduto))
            .ForMember(dest => dest.Equipamento, opt => opt.MapFrom(src => src.NomeEquipamento));

    }
}
