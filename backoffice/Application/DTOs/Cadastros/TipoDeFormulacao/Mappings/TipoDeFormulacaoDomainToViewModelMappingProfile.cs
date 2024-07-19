using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.TipoDeFormulacao.Mappings
{
    public class TipoDeFormulacaoDomainToViewModelMappingProfile : Profile
    {
        public TipoDeFormulacaoDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.TipoDeFormulacao.TipoDeFormulacao, TipoDeFormulacaoViewModel>();
        }
    }
}
