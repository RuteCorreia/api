using Application.DTOs.Cadastros.AplicacaoContrato.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoContrato.Mappings;

public class AplicacaoContratoDomainToViewModelMappingProfile : Profile
{
    public AplicacaoContratoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato, AplicacaoContratoViewModel>();
    }
}
