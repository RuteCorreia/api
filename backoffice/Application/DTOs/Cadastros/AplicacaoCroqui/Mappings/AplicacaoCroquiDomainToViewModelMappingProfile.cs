using Application.DTOs.Cadastros.AplicacaoCroqui.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoCroqui.Mappings;

public class AplicacaoCroquiDomainToViewModelMappingProfile : Profile
{
    public AplicacaoCroquiDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui, AplicacaoCroquiViewModel>();
    }
}
