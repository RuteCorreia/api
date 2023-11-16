using Application.DTOs.Cadastros.Aplicacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Aplicacao.Mappings
{
    public class AplicacaoViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoViewModel, Domain.Entidades.Cadastros.Aplicacao.Aplicacao>();
        }
    }
}
