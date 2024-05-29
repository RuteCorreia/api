using Application.DTOs.Cadastros.AplicacaoLog.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoLog.Mappings;

public class AplicacaoLogDomainToViewModelMappingProfile : Profile
{
    public AplicacaoLogDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog, AplicacaoLogViewModel>();
    }
}
