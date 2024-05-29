using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoCroquiImportacao.Mappings;

public class AplicacaoCroquiImportacaoDomainToViewModelMappingProfile : Profile
{
    public AplicacaoCroquiImportacaoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao, AplicacaoCroquiImportacaoViewModel>();
    }
}
