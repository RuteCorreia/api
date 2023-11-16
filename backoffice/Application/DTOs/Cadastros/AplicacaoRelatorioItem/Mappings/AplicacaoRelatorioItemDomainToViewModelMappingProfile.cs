using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.Mappings;

public class AplicacaoRelatorioItemDomainToViewModelMappingProfile : Profile
{
    public AplicacaoRelatorioItemDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem, AplicacaoRelatorioItemViewModel>();
    }
}
