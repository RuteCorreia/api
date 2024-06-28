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
                .ForMember(dest => dest.NomeAeronave, opt => opt.MapFrom(src => src.Aeronave))
                .ForMember(dest => dest.AlturaVooCustom, opt => opt.MapFrom(src => src.AlturaVoo))
                .ForMember(dest => dest.UrDoAR, opt => opt.MapFrom(src => src.UmidadeRelativaAr))
                .ForMember(dest => dest.Veinculante, opt => opt.MapFrom(src => src.Veiculante))
                .ForMember(dest => dest.TipoDeProduto, opt => opt.MapFrom(src => src.TipoProduto))
                .ForMember(dest => dest.NomeEquipamento, opt => opt.MapFrom(src => src.Equipamento));
        }
    }
}
