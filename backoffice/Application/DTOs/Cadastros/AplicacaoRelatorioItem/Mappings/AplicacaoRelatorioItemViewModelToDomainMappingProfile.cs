using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.Mappings
{
    public class AplicacaoRelatorioItemViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoRelatorioItemViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoRelatorioItemViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>();
        }
    }
}
