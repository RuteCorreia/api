using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.Mappings;

public class AplicacaoRelatorioDomainToViewModelMappingProfile : Profile
{
    public AplicacaoRelatorioDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio, AplicacaoRelatorioViewModel>();
    }
}
