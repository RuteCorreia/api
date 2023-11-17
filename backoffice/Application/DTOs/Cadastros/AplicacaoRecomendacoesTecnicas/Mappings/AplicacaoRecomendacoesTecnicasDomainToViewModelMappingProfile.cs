using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Mappings;

public class AplicacaoRecomendacoesTecnicasDomainToViewModelMappingProfile : Profile
{
    public AplicacaoRecomendacoesTecnicasDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas, AplicacaoRecomendacoesTecnicasViewModel>();
    }
}
