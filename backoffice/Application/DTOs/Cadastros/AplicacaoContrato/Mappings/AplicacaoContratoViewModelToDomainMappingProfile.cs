using Application.DTOs.Cadastros.AplicacaoContrato.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoContrato.Mappings
{
    public class AplicacaoContratoViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoContratoViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoContratoViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato>();
        }
    }
}
