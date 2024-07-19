using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.TipoDeFormulacao.Mappings
{
    public class TipoDeFormulacaoViewModelToDomainMappingProfile : Profile
    {
        public TipoDeFormulacaoViewModelToDomainMappingProfile()
        {
            CreateMap<TipoDeFormulacaoViewModel, Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao>();
        }
    }
}
