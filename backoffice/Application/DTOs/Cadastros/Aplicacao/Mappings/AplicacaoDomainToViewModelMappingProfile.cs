using Application.DTOs.Cadastros.Aplicacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Aplicacao.Mappings;

public class AplicacaoDomainToViewModelMappingProfile : Profile
{
    public AplicacaoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.Aplicacao, AplicacaoViewModel>();
    }
}
