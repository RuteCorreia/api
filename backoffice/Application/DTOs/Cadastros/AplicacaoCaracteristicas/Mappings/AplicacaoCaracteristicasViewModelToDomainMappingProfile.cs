using Application.DTOs.Cadastros.AplicacaoCaracteristicas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoCaracteristicas.Mappings
{
    public class AplicacaoCaracteristicasViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoCaracteristicasViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoCaracteristicasViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>();
        }
    }
}
