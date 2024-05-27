using Application.DTOs.Cadastros.AplicacaoCaracteristicas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoCaracteristicas.Mappings;

public class AplicacaoCaracteristicasDomainToViewModelMappingProfile : Profile
{
    public AplicacaoCaracteristicasDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas, AplicacaoCaracteristicasViewModel>();
    }
}
