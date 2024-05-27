using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Mappings
{
    public class AplicacaoRecomendacoesTecnicasViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoRecomendacoesTecnicasViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoRecomendacoesTecnicasViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>();
        }
    }
}
