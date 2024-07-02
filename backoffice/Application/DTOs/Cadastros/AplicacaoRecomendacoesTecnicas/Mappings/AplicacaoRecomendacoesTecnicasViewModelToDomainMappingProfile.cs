using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Mappings
{
    public class AplicacaoRecomendacoesTecnicasViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoRecomendacoesTecnicasViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoRecomendacoesTecnicasViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>()
                .ForMember(dest => dest.QtdeVeiculante, opt => opt.MapFrom(src => src.QtdVeiculante))
                .ForMember(dest => dest.NomeAeronave, opt => opt.MapFrom(src => src.NomeAeronave))
                .ForMember(dest => dest.AlturaVooCustom, opt => opt.MapFrom(src => src.QtdAlturaVoo))
                .ForMember(dest => dest.UrDoAR, opt => opt.MapFrom(src => src.UmidadeRelativaAr))
                .ForMember(dest => dest.Veinculante, opt => opt.MapFrom(src => src.NomeVeiculante))
                .ForMember(dest => dest.TipoDeProduto, opt => opt.MapFrom(src => src.TipoDeProduto))
                .ForMember(dest => dest.NomeEquipamento, opt => opt.MapFrom(src => src.NomeEquipamento));
        }
    }
}
