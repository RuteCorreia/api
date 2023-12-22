using Application.DTOs.Cadastros.Precificacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Precificacao.Mappings;

public class PrecificacaoDomainToViewModelMappingProfile : Profile
{
    public PrecificacaoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Precificacao.Precificacao, PrecificacaoViewModel>();
    }
}
