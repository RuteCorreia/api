using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Application.DTOs.Cadastros.Sincronizacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Sincronizacao.Mappings;

public class SincronizacaoDomainToViewModelMappingProfile : Profile
{
    public SincronizacaoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Sincronizacao.Sincronizacao, SincronizacaoViewModel>();
    }
}
