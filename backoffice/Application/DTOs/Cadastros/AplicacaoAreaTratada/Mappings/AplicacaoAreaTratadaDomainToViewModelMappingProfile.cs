using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoAreaTratada.Mappings;

public class AplicacaoAreaTratadaDomainToViewModelMappingProfile : Profile
{
    public AplicacaoAreaTratadaDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada, AplicacaoAreaTratadaViewModel>();
    }
}
