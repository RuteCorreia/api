using Application.DTOs.Cadastros.Precificacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Precificacao.Mappings
{
    public class PrecificacaoViewModelToDomainMappingProfile : Profile
    {
        public PrecificacaoViewModelToDomainMappingProfile()
        {
            CreateMap<PrecificacaoViewModel, Domain.Entidades.Cadastros.Precificacao.Precificacao>();
        }
    }
}
