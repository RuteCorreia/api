using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.Mappings
{
    public class AplicacaoRelatorioViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoRelatorioViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoRelatorioViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>();
        }
    }
}
